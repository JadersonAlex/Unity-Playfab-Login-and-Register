using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabPlayerStats : MonoBehaviour {

	
    public void SaveStats()
    {
        // Create new request
        var request = new UpdatePlayerStatisticsRequest();
        // Statistics List
        request.Statistics = new List<StatisticUpdate>();
        // Create new value for statistics
        var stat = new StatisticUpdate { StatisticName = "Coins", Value = 5 };
        // Add entry
        request.Statistics.Add(stat);
        // Send request to PlayfabAPI
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccess, OnPlayFabError);

    }

    private void OnPlayFabError(PlayFabError obj)
    {
        print("Something went wrong");
    }

    private void OnSetStatsSuccess(UpdatePlayerStatisticsResult obj)
    {
        print("Stats has been added");
    }

    public void GetStats()
    {
        // Create new request
        var request = new GetPlayerStatisticsRequest();
        request.StatisticNames = new List<string>() { "Coins" };
         // Send request to PlayfabAPI
        PlayFabClientAPI.GetPlayerStatistics(request, GetStatsSuccess, OnPlayFabError);
    }

    private void GetStatsSuccess(GetPlayerStatisticsResult obj)
    {
        print("Stats wurden erfolgreich empfangen");

        // Output statistics
        foreach(var stat in obj.Statistics)
        {
            print("Statistic: " + stat.StatisticName + " Stats value: " + stat.Value);
        }
    }

    public void GetLeaderBoard()
    {
    var request = new GetLeaderboardRequest();
    request.StartPosition = 0;
    request.StatisticName = "Coins";
    PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, OnPlayFabError);
    }

    private void OnLeaderboardSuccess(GetLeaderboardResult obj){
        foreach(var value in obj.Leaderboard)
        {
            print(value.StatValue + " " + value.DisplayName);
        }
    }
}