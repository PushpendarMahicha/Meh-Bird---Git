using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using System;
using TMPro;
using UnityEngine.UI;

public class OnlineServiceManager : MonoBehaviour
{

    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Button saveNameButton;
    private async void Start()
    {
        try
        {
            if(UnityServices.State!=ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
                if(!AuthenticationService.Instance.IsSignedIn)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
                Debug.Log("Online services ready");
                Debug.Log("player id:" + AuthenticationService.Instance.PlayerId);
            }
        }
        catch(Exception exception)
        {
            Debug.LogError("Online service initilzation failed: " + exception.Message);
        }

        string nameKey = "HasChosenName_" + AuthenticationService.Instance.PlayerId;
        bool hasChoseName = PlayerPrefs.GetInt(nameKey, 0) == 1;
        PlayerNamePanel.SetActive(!hasChoseName);
       
    }

    public void OnSaveNameButtonClicked()
    {
        _ = SavePlayerName();
    }
    private async Task SavePlayerName()
    {
        string playerName = playerNameInput.text.Trim();
        if(string.IsNullOrEmpty(playerName))
        {
            statusText.text = "Please Enter a Name";
            return;
        }
        if(playerName.Length>50 || playerName.Contains(" "))
        {
            statusText.text = "Name must be under 50 character with no space.";
            return;
        }
        saveNameButton.interactable = false;
        statusText.text = "Saving name...";

        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            string nameKey = "HasChosenName_" + AuthenticationService.Instance.PlayerId;
            PlayerPrefs.SetInt(nameKey, 1);
            PlayerPrefs.Save();
            PlayerNamePanel.SetActive(false);
        }
        catch(Exception exception)
        {
            statusText.text = "Could not save name. Try another name."; Debug.LogError(exception);
        }
        finally
        {
            saveNameButton.interactable = true;
        }
    }
}
