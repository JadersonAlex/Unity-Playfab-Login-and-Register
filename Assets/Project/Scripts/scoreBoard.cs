using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Linq;

public class scoreBoard : MonoBehaviour {

       public GameObject ScoreRecordPrefab;

       public List<ScoreRecord> ScoreRecords = new List<ScoreRecord>();
       private Transform UIParent;



       public void GetLeaderBoard()
       {
        var request = new GetLeaderboardRequest();
        request.StartPosition = 0;
        request.StatisticName = "Coins";
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, OnPlayFabError);
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        print("Something went wrong");
    }

    private void OnLeaderboardSuccess(GetLeaderboardResult obj)
    {
      UIParent = GameObject.FindGameObjectWithTag("pame").transform;
                ScoreRecord temp; // temp variable for a scoreRecord
                for (int i = 0; i < obj.Leaderboard.Count; i++)
                {
                    temp = GetRecord(obj.Leaderboard[i].DisplayName);
                    //create a new entry
                    if(temp == null)
                    {
                        //create a new ScoreRecord
                        temp = Instantiate(ScoreRecordPrefab, transform.position, transform.rotation, UIParent).GetComponent<ScoreRecord>();
                        temp.WriteRecord(obj.Leaderboard[i].DisplayName, obj.Leaderboard[i].StatValue.ToString());
                    }
                    else
                    {
                        //just set the score, it might have updated :shrug:
                        temp.Score.text = obj.Leaderboard[i].StatValue.ToString();
                    }
                    //add it to the score list
                    ScoreRecords.Add(temp);

                    //debug the value
                    Debug.Log(temp.Name.text + " " + temp.Score.text);
                }
            }

            //just loops and checks for a ScoreRecord with a name
            private ScoreRecord GetRecord(string name)
            {
                for (int i = 0; i < ScoreRecords.Count; i++)
                {
                    if (ScoreRecords[i].name == name)
                        return ScoreRecords[i]; // we found it
                    }

                return null; // cant find it
            }
}
