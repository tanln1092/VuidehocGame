using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Facebook.MiniJSON;


public class cl_FacebookPlugin : MonoBehaviour{
	private Action<bool> _loginCallback;
	private Action<int> _inviteCallback;
	private PostData _postData;
	Action<string> _friendCallback;
	private Action<List<FBFriend>> _friendListCallBack;

	private Dictionary<string, Texture2D> _photoStorage = new Dictionary<string, Texture2D> ();
	private Dictionary<string, PictureRequest> _pictureRequest = new Dictionary<string, PictureRequest> ();
	private Dictionary<string, FBFriend> _dicFriends = new Dictionary<string, FBFriend> ();
	private List<FBFriend> friendList = new List<FBFriend> ();

	public List<FBFriend> GetFriendList{
		get{
			friendList.Clear ();
			foreach(var item in _dicFriends){
				friendList.Add(item.Value);
			}
			return friendList;
		}
	}

	public class FBFriend{
		public string name;
		public string id;
		public int score;
	}
	private class PostData{
		public string message;
		public byte[] image;
		public string imageUrl;
		public Action<string> postCallback;

		public PostData(string msg, byte[] img, Action<string> callback){
			postCallback = callback;
			image = img;
			message = msg;
		}

		public PostData(string msg, string img, Action<string> callback){
			postCallback = callback;
			imageUrl = img;
			message = msg;
		}

		public PostData(string msg, Action<string> callback){
			postCallback = callback;
			message = msg;
		}

	}

	bool _canpostTofb;
	public bool CanPostToFacebook{

		// return if application can post in user's name
		get{return _canpostTofb;}
		set{_canpostTofb = value;}
	}

	private List<string> usrsIdsFilter = new List<string> ();
	public cl_FacebookPlugin(){

	}
	public void Init(Action<bool> callback){
		_loginCallback = callback;
		FB.Init (InitCallback, null, null);
	}
	private void InitCallback(){
		_loginCallback (true);
	}
	public void LoginWithReadPermission(Action<bool> callback){
		_loginCallback = callback;
		FB.Login ("email", LoginReadPermissions);
	}
	private void LoginReadPermissions(FBResult response){
		if (response.Error != null)
						_loginCallback (false);
		Debug.Log (response.Error);
		Debug.Log (response.Text);

		if (FB.IsLoggedIn) {
			FB.API("/me/permissions", Facebook.HttpMethod.GET, PermissionsCallback);		
		}else{
			FB.Logout();
			FB.Init(InitCompleteCallback, null, null);
		}
	}
	private void InitCompleteCallback(){

	}
	public void LoginWithPostPermissions(Action<bool> callback){
		// Login only with Publish permission
		_loginCallback = callback;
		FB.Login ("publish_actions", LoginWithPostPermissionCall);

	}
	private void LoginWithPostPermissionCall(FBResult response){

	}
	public void LoginWithBothPermissions(Action<bool> callback){
		_loginCallback = callback;
		FB.Login ("email, publish_actions", LoginWithBothPermissionCallback);
	}
	private void LoginWithBothPermissionCallback(FBResult response){
		Debug.Log (response.Error);
		Debug.Log (response.Text);

		if (response.Error != null)
			_loginCallback (false);	
		Dictionary<string, object> a = Json.Deserialize (response.Text) as Dictionary<string, object>;
		bool isLogged = (bool)a["is_logged_in"];

		if(isLogged)
			FB.API ("/me/permissions", Facebook.HttpMethod.GET, PermissionsCallback);
		else{
			FB.Logout();
			FB.Init(InitCompleteCallback, null, null);
		}


	}
	private void PermissionsCallback(FBResult response){
		// If the Kson contains the key "publish_actions" means that user has accepted them
		if (response.Text.Contains ("publish_actions")) {
			CanPostToFacebook = true;		
		}else{
			CanPostToFacebook = false;
		}
		Debug.Log("Publish Permission Aceepted?[" + CanPostToFacebook + "]");
		GetUserProfile ();
	}
	private void GetUserProfile(){
		// Will get the user basic info
		FB.API ("/me?fields=id,name,first_name,last_name,locale", Facebook.HttpMethod.GET, GetUserProfileCall);
	}
	private void GetUserProfileCall(FBResult response){
		if(response.Error != null){
			if(_loginCallback != null)
				_loginCallback(false);
			return;
		}
		Debug.Log (response.Error);
		Debug.Log (response.Text);

		Dictionary<string, object> a = Json.Deserialize (response.Text) as Dictionary<string, object>;

		FacebookGUI.instance.userId = a ["id"].ToString ();
		FacebookGUI.instance.userName = a ["first_name"].ToString () + " " + a ["last_name"].ToString ();

		if(_loginCallback != null){
			_loginCallback(true);
		}
	}

	#region POST
	public void PostImageToFacebook(byte[] image, Action<string> callback){

		_postData = new PostData ("Your Message", image, callback);
		if(!CanPostToFacebook){
			callback("cp");
			return;
		}
		var wwwForm = new WWWForm();
		wwwForm.AddBinaryData("image", _postData.image, "desordenados.png");
		wwwForm.AddField("message", _postData.message);
		FB.API ("me/photos", Facebook.HttpMethod.POST, ImagePostCall, wwwForm);
	}

	private void LoginAndPostImageCallback(bool result){
		if (result) 
			PostImageToFacebook(_postData.image, _postData.postCallback);
		else
			_postData.postCallback("error");
	}
	private void ImagePostCall(FBResult result){
		if(result.Error == null)
			_postData.postCallback("");
		else
			_postData.postCallback(result.Error);
	}

	public void PostMessage(string message, Action<string> callback){
		if (!IsSessionValid ()) {
			LoginWithBothPermissions(LoginAndPostImageCallback);		
		}
		if (!CanPostToFacebook) {
			callback("cp");
			return;
		}

		_postData = new PostData (message, callback);

		Dictionary<string, string> a = new Dictionary<string, string> ();
		a.Add ("message", message);
		FB.API ("/me/feed", Facebook.HttpMethod.POST, PostResponse, a);
	}
	private void LoginAndPostMessage(bool result){
		if(result)
			PostMessage(_postData.message, _postData.postCallback);
		else
			_postData.postCallback("error");
	}

	public void ShareMessageWithGameLink(string message, Action<String> callback){
		_postData = new PostData (message, callback);

		if (!IsSessionValid ()) {
			LoginWithReadPermission(delegate (bool result){
				if(result)
					ShareMessageWithGameLink(_postData.message, _postData.postCallback);
				else
					_postData.postCallback("error");
			});
			return;
		}

		FB.Feed (linkCaption:"linkCaption", picture:"http://imageshack.com/a/image843/2459/44te.png",linkName:message, link:"http://imageshack.com/a/image843/2459/44te.png", callback:PostResponse);


	}

	public void ShareMessageWithLinkAndImage(string message, string imageUrl, Action<string> callback, string gameLink){
		_postData = new PostData (message, imageUrl, callback);
		if (!IsSessionValid ()) {
		
			// If session is not valid, first we need to log in into Facebook
			LoginWithReadPermission(delegate(bool result) {
				if(result)
					ShareMessageWithGameLink(_postData.message, _postData.postCallback);
				else
					_postData.postCallback("error");
			});
			return;
		}
		FB.Feed(linkName: message, picture: imageUrl, link:gameLink, callback:PostResponse);
	}

	private void PostResponse(FBResult response){
		Dictionary<string, object> s = Json.Deserialize (response.Text) as Dictionary<string, object>;
		object a;

		if (response.Error != null) {
			_postData.postCallback(response.Error);
			return;
		}
		s.TryGetValue ("cancalled", out a);

		// If a != null means that the user has cancalled the request
		if(a != null)
			_postData.postCallback("cancelled");
		else
			_postData.postCallback("");
	}

	private void PostCallback(string error, object parameters){
		if(error == null || error == "")
			_postData.postCallback("");
		else
			_postData.postCallback(error);

	}
	#endregion

	#region FRIENDS
	public void GetFriends(Action<List<FBFriend>> callback){
		_friendListCallBack = callback;
		FB.API ("me?fields=friends.field(id,name)", Facebook.HttpMethod.GET, GetFriendsCallback);
	}
	private void GetFriendsCallback(FBResult response){
		if (response.Error != null || response.Text == "") {
			_friendListCallBack(null);
			return;
		}
		Debug.Log (response.Text);
		List<object> data =  Util.DeserializeJSONFriends (response.Text);

		for (int i = 0; i < data.Count; i++) {
			Dictionary<string, object> a = (Dictionary<string, object>)data[i];
			FBFriend friend = new FBFriend();
			String id = (string) a["id"];
			if(_dicFriends.ContainsKey(id)){
				_dicFriends[id].name = friend.name = (string)a["name"];
			}else{
				friend.id = id;
				friend.name = (string)a["name"];
				_dicFriends.Add(friend.id, friend);
			}
		}
		_friendListCallBack (GetFriendList);
	}

	#endregion

	#region PICTURE
	public void GetPhoto(string fbId, Action<Texture2D> callback){
		if (fbId == null) {
			callback(null);
			return;
		}

		if (CheckIfPhotoExists (fbId, callback))
			return;

		PictureRequest request = GetOrCreatePictureRequest (fbId, callback);
	}
	private PictureRequest GetOrCreatePictureRequest(string facebookId, Action<Texture2D> callback){
		PictureRequest request;

		if (_pictureRequest.TryGetValue (facebookId, out request)) {
			return request;		
		}

		request = new PictureRequest ();
		_pictureRequest.Add (facebookId, request);
		request.AddCallback (callback);
		var url = "http://graph.facebook.com/" + facebookId + "/picture?type=square&height=100@width=100";
		CoroutineManager.i.LoadProfileImage (url, delegate(Texture2D texture) {
						PhotoToStorage (facebookId, texture, request);
				});

		return request;
	}

	private void PhotoToStorage(string fbId, Texture2D texture, PictureRequest request){
		if (!_photoStorage.ContainsKey (fbId)) {
			_photoStorage.Add(fbId, texture);		
		}

		request.RunCallbacks (texture);
	}

	private bool CheckIfPhotoExists(string fbId, Action<Texture2D> callback){
		if (_photoStorage.ContainsKey (fbId)) {
			callback(_photoStorage [fbId]);
			return true;
		}else
			return false;
	}

	private class PictureRequest
		{
		private List<Action<Texture2D>> _callbacks;

		public PictureRequest(){
			_callbacks = new List<Action<Texture2D>> ();
		}

		public void AddCallback(Action<Texture2D> callback){
			_callbacks.Add (callback);
		}

		public void RunCallbacks(Texture2D image){
			foreach(Action<Texture2D> callback in _callbacks){
				callback(image);

			}
			_callbacks = null;
		}
	}


	#endregion

	#region FACEBOOK WWW PROFILE PICTURE
	public void LoadProfileImage(string url, Action<Texture2D> completionHandlet){
		StartCoroutine (fetchImageAtUrl(url, completionHandlet));
	}

	// Fetches an image for the url, completionHandler will fire with the Texture2D or null
	public IEnumerator fetchImageAtUrl(string url, Action<Texture2D> completionHandlet){
		var www = new WWW (url);

		yield return www;

		if (!string.IsNullOrEmpty (www.error)) {
			Debug.Log ("Error attempting to load profile image: "  + www.error);
			if(completionHandlet != null)
				completionHandlet(null);
		}else{
			if(completionHandlet != null)
				completionHandlet(www.texture);
		}
	}
	#endregion

	#region INVITE
	public void InviteRequest(Action<int> result){
		_inviteCallback = result;

		if (!IsSessionValid ()) {
			LoginWithReadPermission(LoginAndInvite);
			return;
		}
//		FB.AppRequest(
//			"InviteMessage", null, "", usrsIdsFilter.ToArray(), 100, "data", "InviteTitle", InviteCallback 
//		);
	}

	  

	private void LoginAndInvite(bool result){
		if (result) {
			InviteRequest(_inviteCallback);		
		}else{
			_inviteCallback(-1);
		}
	}

	private void InviteCallback(FBResult response){
		Dictionary<string, object> data = Json.Deserialize (response.Text) as Dictionary<string, object>;

		object a;

		// We get thelist of ids who we invited
		if (data.TryGetValue ("to", out a)) {
			Debug.Log(a);

			List<object> requestList = (List<object>) a;
			List<string> usersInvited = new List<string> ();

			foreach(var item in requestList){
				usersInvited.Add((string)item);
			}

			foreach(var item in usersInvited){
				usrsIdsFilter.Add(item);
			}

			if(_inviteCallback != null)
				_inviteCallback(usersInvited.Count);
			return;
		}

		if(_inviteCallback != null)
			_inviteCallback (-1);

	}
	#endregion

	#region SCORE

	public void PostScoreToFB(Action<string> callback, int score){

		_friendCallback = callback;

		if (!CanPostToFacebook) {
			callback("cp");
			return;
		}

		Dictionary<string, string> scoredata = new Dictionary<string, string> ();
		scoredata.Add ("score", score.ToString ());

		FB.API ("/me/scores", Facebook.HttpMethod.POST, PostScoreCallback, scoredata);
	}

	private void PostScoreCallback(FBResult response){

		if (response.Error != null) {
			_friendCallback(response.Error);		
			return;
		}
		if(response.Text == "true"){
			_friendCallback("");
		}
	}

	private Action<List<FBFriend>> friendAction;

	public void GetFBScore(Action<List<FBFriend>> callback){
		friendAction = callback;
		FB.API ("AppId/scores", Facebook.HttpMethod.GET, GetScoreCallback);
	}

	private void GetScoreCallback(FBResult response){
		if (response.Error != null || response.Text == "") {
			friendAction(null);
			return;
		}

		List<object> data = Util.DeserializeScores (response.Text);

		for(int i = 0; i < data.Count; i++){
			Dictionary<string, object> a =  (Dictionary<string, object>) data[i];
			Dictionary<string, object> b =  (Dictionary<string, object>) a["user"];
			FBFriend friend = new FBFriend();
			friend.id = (string) b["id"];
			friend.name = (string) b["name"];
			friend.score = (int) b["score"];

			if(_dicFriends.ContainsKey (friend.id)){
				if(_dicFriends[friend.id].score < (int) a["score"]){
					_dicFriends[friend.id].score = (int)a["score"];
				}
			}else{
				_dicFriends.Add(friend.id, friend);
			}

		}

		friendAction (friendList);
	}

	public void GetInitial(){
		FB.API ("AppId/scores", Facebook.HttpMethod.GET, GetInitialScoreCallback);
	}

	private void GetInitialScoreCallback(FBResult response){
		if (response.Error != null || response.Text == "") {
			_loginCallback(false);
			return;
		}

		List<object> data = Util.DeserializeScores (response.Text);
		
		for(int i = 0; i < data.Count; i++){
			Dictionary<string, object> a =  (Dictionary<string, object>) data[i];
			Dictionary<string, object> b =  (Dictionary<string, object>) a["user"];
			FBFriend friend = new FBFriend();
			friend.id = (string) b["id"];
			friend.name = (string) b["name"];
			friend.score = (int) b["score"];
			
			if(_dicFriends.ContainsKey (friend.id)){
				if(_dicFriends[friend.id].score < (int) a["score"]){
					_dicFriends[friend.id].score = (int) a["score"];
				}
			}else{
				_dicFriends.Add(friend.id, friend);
			}
			
		}
		
		_loginCallback (true);

	}
	#endregion

	public bool IsSessionValid(){
		return FB.IsLoggedIn;
	}
}

