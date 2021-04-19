using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;

public class PlayFabAccount : MonoBehaviour {

    public InputField inputPlayerName;
    public GameObject accountPanel;
    public GameObject LeadPanel;

    public void UpdatePlayerName()
    {
        // This is the input for the player name
        string text = inputPlayerName.text;

        // if text exists then update the name
        if (text != null)
        {
            // Create a request
            var request = new UpdateUserTitleDisplayNameRequest();
            request.DisplayName = text.ToString();
            
            // API 
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnPlayerNameResult, OnPlayFabError);

        }
    }

    public void ChangePanel()
    {
        if (accountPanel.activeSelf)
        {
            accountPanel.SetActive(false);
        }
        else
        {
            accountPanel.SetActive(true);

        }

    }

     public void LeaderboardPanel()
    {
        if (LeadPanel.activeSelf)
        {
            LeadPanel.SetActive(false);
        }
        else
        {
            LeadPanel.SetActive(true);

        }

    }


    private void OnPlayFabError(PlayFabError obj)
    {
        print("Error: " + obj.Error);
    }

    private void OnPlayerNameResult(UpdateUserTitleDisplayNameResult obj)
    {
        print("Your new display name is " + obj.DisplayName);
    }
}