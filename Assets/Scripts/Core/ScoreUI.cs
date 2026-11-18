using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void OnEnable()
    {
        if (gameManager == null || scoreText == null)
        {
            enabled = false;
            return;
        }
        gameManager.ScoreChanged += UpdateScore;
        UpdateScore(gameManager.Score);
        UpdateHighScore(gameManager.highScore);
    }
    private void OnDisable()
    {
        if(gameManager!=null)
        {
            gameManager.ScoreChanged -= UpdateScore;
        }
    }
    private void UpdateScore(int score)
    {
        scoreText.text = "Score: "+score;
        finalScoreText.text = "Score: " + score;
        if(score>gameManager.highScore)
        {
            UpdateHighScore(score);
            finalScoreText.color = Color.green;
            finalScoreText.text = "New HighScore: " + score;
        }
    }
    private void UpdateHighScore(int score)
    {
        Debug.Log("pre:" + GameManager.Instance.previousHighScore + "new:" +score);
        highScoreText.text = GameManager.Instance.isNewHighScore?"Previous HighScore: "+ GameManager.Instance.previousHighScore:"HighScore: " + score;
    }
}
