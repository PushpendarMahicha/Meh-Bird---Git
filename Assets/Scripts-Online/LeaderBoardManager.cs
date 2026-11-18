using UnityEngine;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
public class LeaderBoardManager : MonoBehaviour
{
    private const string LeaderboardID = "global_high_score";

    [SerializeField] private GameObject LeaderBoardPanel;
    [SerializeField] private TMP_Text LeaderBoardEntries;
    [SerializeField] private Button RefreshButton;
    public async Task SubmitScore(int score)
    {
        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardID, score);
            Debug.Log("Score submitted online"+score);
        }
        catch(System.Exception exception)
        {
            Debug.LogError("Score submission failed: " + exception.Message);
        }
    }
    public async Task GetTopScores()
    {
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardID);
            foreach(var entry in scores.Results)
            {
                Debug.Log("Player: " + entry.PlayerName.Split('#')[0] + " | Score: " + entry.Score);
            }
        }
        catch(System.Exception exception)
        {
            Debug.LogError("Leaderboard download failed: " + exception.Message);
        }
    }
    public async Task RefreshLeaderBoard()
    {
        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardID);
            LeaderBoardEntries.text = "";
            int rank = 1;
            foreach(var entry in scores.Results)
            {
                LeaderBoardEntries.text += $"{rank}.{entry.PlayerName.Split('#')[0] } - {entry.Score}\n";
                rank++;
            }
            if(scores.Results.Count==0)
            {
                LeaderBoardEntries.text = "no score yet";
            }
        }
        catch(Exception exception)
        {
            Debug.LogError("LeaderBoard download failed: " + exception.Message);
            LeaderBoardEntries.text = "unable to load scores. try again";
        }
    }
    public async void OpenLeaderBoard()
    {
        LeaderBoardPanel.SetActive(true);
        await RefreshLeaderBoard();
    }
    public void CloseLeaderBoard()
    {
        LeaderBoardPanel.SetActive(false);
    }
    public void RefreshLeaderBoardButton()
    {
        _ = RefreshLeaderBoard();
    }
}
