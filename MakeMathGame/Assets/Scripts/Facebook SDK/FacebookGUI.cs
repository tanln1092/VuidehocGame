using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Facebook.MiniJSON;


public class FacebookGUI : MonoBehaviour {

	public static FacebookGUI instance;
	public cl_FacebookPlugin FBPlugin;
	public static bool _isInitialized;
	public static bool _isLoggedIn;

	private int SHARE_FB = 1;
	private int INVITE_FB = 2;

	private int FB_FUNCTION = 0;

	string GAME_URL 		= "http://vuidehoc.zz.mu/assets/VuiDeHoc_v1.0.8/VuiDeHoc_v1.0.8/VuiDeHoc_v1.0.8.html";
	string GAME_ICON_URL 	= "http://icons.iconarchive.com/icons/elegantthemes/beautiful-flat/128/calculator-icon.png";
//	public UserDislay userDisplay;
	public string userId;
	public string userName;
	Action<Texture2D> callback;

	string access_token = "CAANkWRz66ygBAGBx2jwLebFZBDJnk9bMhNZBGMDBZBfNtnYdhRZBPAVFVOl3KCI0IMQ3fUHFdIAI81vW8YmCYcZC1LEfW4raOCcFfc4iZCLkzqe6RDZCf3D04XrmljN7xJWFin5TKCOWYebEGBZA5M1LkTiItIH1gwiZBnFczir9GEXs2uVQhFPVcJKxLHb5l1SnoNqU9G9uMjCFZAwZCZBkZCKXycjoOOVK24tkZD";

	void Awake(){
		instance = this;
		FBPlugin = new cl_FacebookPlugin ();

//		if(!_isInitialized){
//			FBPlugin.Init(InitializeCallback);
//		}
	}

	void OnClick(){
		string name = gameObject.name;
		
		switch (name) {
			case "share_button":	

//			StartCoroutine(TakeScreenshot());
			if (!_isLoggedIn) {
				FB_FUNCTION = SHARE_FB;
				FBPlugin.LoginWithBothPermissions(LoginCallback);
			}else{
				FB_FUNCTION = SHARE_FB;

			}

		
				break;
			
			case "invite_button":	
			if (!_isLoggedIn) {
				FB_FUNCTION = INVITE_FB;
				FBPlugin.LoginWithBothPermissions(LoginCallback);
			}else{
				FB_FUNCTION = INVITE_FB;
				
			}
				break;
	
		}
	}
	void Update(){
		if (_isLoggedIn && FB_FUNCTION == SHARE_FB) {
			FB_FUNCTION = 0;
			FBPlugin.ShareMessageWithLinkAndImage (userName + " đã đạt được " + Program.score + " điểm trong màn chơi " + Program.gameLevel + " của trò chơi Vui Để Học.", 
			                                       GAME_ICON_URL, 
			                                       ShareCallback, 
			                                       GAME_URL);
		}
		if (_isLoggedIn && FB_FUNCTION == INVITE_FB) {
			FB_FUNCTION = 0;
			onChallengeClicked();
		}
	}
	private IEnumerator TakeScreenshot()
	{
		yield return new WaitForEndOfFrame();
		
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToPNG();
		
		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
		
		FB.API("me/photos", Facebook.HttpMethod.POST, LogCallback, wwwForm);
	}

	void LogCallback(FBResult response)
	{
		var responseObject = Json.Deserialize(response.Text) as Dictionary<string, object>;
		object cancelled;
		if (responseObject.TryGetValue ("cancelled", out cancelled))
		{
			if( (bool)cancelled == true )
			{
				Util.Log("not publish");                                                                                  

			}
			else
			{
				Util.Log("publish");                                                                                  
			}
		}
		else
		{
			Util.Log("publish");                                                                                  
		}
	}
	

//	void OnGUI(){
//
//		if (!_isLoggedIn) {
//			if(GUI.Button(new Rect(Screen.width/2,0, 100, 100), "Login  Facebook")){
//				FBPlugin.LoginWithBothPermissions(LoginCallback);
//				FBPlugin.GetPhoto (instance.userId, callback);
//
//			}
//		}
//		if(GUI.Button(new Rect(Screen.width/2 + 200,0, 100, 100), "Share on Facebook"))
////			FBPlugin.ShareMessageWithGameLink("Hello world!", ShareCallback);
//		GUI.Label (new Rect (Screen.width / 2 - 100, 0, 100, 100), userName);
//
//		if(GUI.Button(new Rect(Screen.width/2 , 200, 100, 100), "Invite friend"))
//			onChallengeClicked();
//	}

	private void InitializeCallback(bool isSuccess){
		_isInitialized = isSuccess;
	}

	private void LoginCallback(bool isSuccess){
		_isLoggedIn = isSuccess;

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
//			to: null,
//			excludeIds : null,
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
//								AddPopupMessage("Request Sent", ChallengeDisplayTime);
				Util.Log("Request sent" + result.Text);                                                                                       
			}                                                                                                                      
		}                                                                                                                          
	}  



}
