using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<int> ScoreChanged;
    [SerializeField] private TMP_Text FinalScoreText;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject tapToPlayTextImage;
    [SerializeField] private LeaderBoardManager LeaderBoardManager;
    public bool isGameOver { get; private set; }
    public bool isGameStarted { get; private set; }
    public int Score { get; private set; }
    public int highScore { get; private set; }
    public int previousHighScore { get; private set; }
    public bool isNewHighScore { get; private set; }


    private void Awake()
    {
        if(Instance!=null && Instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }
    public void StartGame()
    {
        if(isGameStarted || isGameOver)
        {
            return;
        }
        isGameStarted = true;
        tapToPlayTextImage.SetActive(false);
        Debug.Log("game started");
    }
    public void GameOver()
    {
        if(isGameOver)
        {
            return;
        }
        isGameOver = true;
        GameOverPanel.SetActive(true);
        if(AudioManager.Instance!=null)
        {
            AudioManager.Instance.PlayGameOverClip();
        }
       if(LeaderBoardManager!=null)
        {
            _ = LeaderBoardManager.SubmitScore(Score);
        }
        Debug.Log("game over");
    }
    public void AddScore(int amount)
    {
        Debug.Log("AddScore");
        if(!isGameStarted || isGameOver || amount<=0)
        {
            return;
        }
        Score += amount;
        ScoreChanged?.Invoke(Score);
        if(Score>highScore)
        {           
            previousHighScore = highScore;
            Debug.Log("previous: "+previousHighScore+"new:"+Score);
            highScore = Score;
            isNewHighScore = true;
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.Save();
        }
        if(AudioManager.Instance!=null)
        {
            AudioManager.Instance.PlayScoreClip();
        }
        Debug.Log("Score: " + Score);
    }
    public void ResstartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Bug Testing
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("highScore");
        highScore = 0;
        previousHighScore = 0;
        isNewHighScore = false;
        Debug.Log("HighScore reset");
    }
}
