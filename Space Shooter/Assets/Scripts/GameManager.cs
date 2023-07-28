using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    [SerializeField] private GameObject _pauseMenuPanel;
    
    private Animator _pauseAnimator;

    private void Start()
    {
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    { 
        if(_isGameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1); //Game scene
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0); //Main menu scene
            }
        }

        if(!_isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuPanel.SetActive(true);
            _pauseAnimator.SetBool("isPaused", true);
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
