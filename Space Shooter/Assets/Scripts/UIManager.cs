using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImg;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartText;
    [SerializeField] private Text _returnText;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private Text _bestScoreText;

    private GameManager _gameManager;
    private int _bestScore;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;

        _bestScore = PlayerPrefs.GetInt("HighScore", 0); // 0 is default value, only used if there is no HighScore txt
        _bestScoreText.text = "Best: " + _bestScore;

        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _returnText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL!");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateBestScore(int playerScore)
    {
        if(playerScore > _bestScore)
        {
            _bestScore = playerScore;
            PlayerPrefs.SetInt("HighScore", _bestScore);
            _bestScoreText.text = "Best: " + _bestScore;
        }
        
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();

        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _returnText.gameObject.SetActive(true);
        StartCoroutine(GameOverTextFlickerRoutine());
    }

    IEnumerator GameOverTextFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        _gameManager.ResumeGame();
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}