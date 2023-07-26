using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;

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
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
