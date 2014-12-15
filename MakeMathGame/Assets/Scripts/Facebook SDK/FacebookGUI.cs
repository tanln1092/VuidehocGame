using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Facebook.MiniJSON;


public class FacebookGUI : MonoBehaviour {

	public static FacebookGUI instance;
	public cl_FacebookPlugin FBPlugin;
	private bool _isInitialized;
	private bool _isLoggedIn;
//	public UserDislay userDisplay;
	public string userId;
	public string userName;
	Action<Texture2D> callback;

	string access_token = "CAANkWRz66ygBAGBx2jwLebFZBDJnk9bMhNZBGMDBZBfNtnYdhRZBPAVFVOl3KCI0IMQ3fUHFdIAI81vW8YmCYcZC1LEfW4raOCcFfc4iZCLkzqe6RDZCf3D04XrmljN7xJWFin5TKCOWYebEGBZA5M1LkTiItIH1gwiZBnFczir9GEXs2uVQhFPVcJKxLHb5l1SnoNqU9G9uMjCFZAwZCZBkZCKXycjoOOVK24tkZD";

	void Awake(){
		instance = this;
		FBPlugin = new cl_FacebookPlugin ();


	}

	void OnGUI(){
		if(!_isInitialized){
			if(GUI.Button(new Rect(Screen.width/2-100,0, 100, 100), "Init Facebook")){
				FBPlugin.Init(InitializeCallback);
			
			}
			return;
		}
		if (!_isLoggedIn) {
			if(GUI.Button(new Rect(Screen.width/2,0, 100, 100), "Login  Facebook")){
				FBPlugin.LoginWithBothPermissions(LoginCallback);
				FBPlugin.GetPhoto (instance.userId, callback);

			}
		}
		if(GUI.Button(new Rect(Screen.width/2 + 200,0, 100, 100), "Share on Facebook"))
//			FBPlugin.ShareMessageWithGameLink("Hello world!", ShareCallback);
			FBPlugin.ShareMessageWithLinkAndImage ("Hello world!", "http://icons.iconarchive.com/icons/designbolts/handstitch-social/256/Share-icon.png", ShareCallback, "http://vuidehoc.zz.mu/assets/VuiDeHoc_v1.0.8/VuiDeHoc_v1.0.8.html");
		GUI.Label (new Rect (Screen.width / 2 - 100, 0, 100, 100), userName);

		if(GUI.Button(new Rect(Screen.width/2 , 200, 100, 100), "Invite friend"))
			onChallengeClicked();
	}

	private void InitializeCallback(bool isSuccess){
		Debug.Log ("Initialize complete? :[" + isSuccess + "]");
		_isInitialized = isSuccess;
	}

	private void LoginCallback(bool isSuccess){
		Debug.Log ("Initialize complete? :[" + isSuccess + "]");
		_isInitialized = isSuccess;

	}

	private void ShareCallback(string error){
		if (string.IsNullOrEmpty (error)) {
			Debug.Log("Share Succeed? (TRUE)");		
		}else
			Debug.Log("Share Succeed? (FALSE)");		

	}

	private void onChallengeClicked()                                                                                              
	{ 
//		FB.AppRequest(
//			to: null,
//			filters : "",
//			excludeIds : null,
//			message: "Friend Smash is smashing! Check it out.",
//			title: "Play Friend Smash with me!",
//			callback:appRequestCallback
//			);  
		FB.AppRequest(
						to: null,

			message: "Come play this great game!", 
			callback: appRequestCallback
			);
		
	}                                                                                                                              
	private void appRequestCallback (FBResult result)                                                                              
	{                                                                                                                              
		Util.Log("appRequestCallback");                                                                                         
		if (result != null)                                                                                                        
		{                                                                                                                          
			var responseObject = Json.Deserialize(result.Text) as Dictionary<string, object>;                                      
			object obj = 0;                                                                                                        
			if (responseObject.TryGetValue ("cancelled", out obj))                                                                 
			{                                                                                                                      
				Util.Log("Request cancelled");                                                                                  
			}                                                                                                                      
			else if (responseObject.TryGetValue ("request", out obj))                                                              
			{                
				//				AddPopupMessage("Request Sent", ChallengeDisplayTime);
				Util.Log("Request sent");                                                                                       
			}                                                                                                                      
		}                                                                                                                          
	}  



}
