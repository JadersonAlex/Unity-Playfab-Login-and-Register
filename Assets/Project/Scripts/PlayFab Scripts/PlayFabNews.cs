// using PlayFab;
// using PlayFab.ClientModels;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using UnityEngine.UI;

// public class PlayFabNews : MonoBehaviour {

// public Text titleText, bodyText;

    
// 	void Start(){
// 	GetNews();
// 	}

// 	public void GetNews(){
//     GetTitleNewsRequest request = new GetTitleNewsRequest();
// 	PlayFabClientAPI.GetTitleNews(request, GetNewsSuccess, GetNewsError);
// 	}

// 	private void GetNewsError(PlayFabError obj)
// 	{		
// 		print("News Error");

// 	}

// 	private void GetNewsSuccess(GetTitleNewsResult obj){
	
// 		print("News Success " + obj.News.Count);
// 		print(obj.News[0].Title);
// 		print(obj.News[0].Body);
// 		titleText.text = obj.News[0].Title;
// 		bodyText.text = obj.News[0].Body;

// 	}


// }
