using UnityEngine;
using System.Collections;
using System;

public class CoroutineManager : MonoBehaviour {

	public static CoroutineManager i;

	// Use this for initialization
	void Start () {
		i = this;
	}
	
	#region FACEBOOK WWW PROFILE PICTURE
	public void LoadProfileImage(string url, Action<Texture2D> completionHandler){
		StartCoroutine (fetchImageAtUrl (url, completionHandler));
	}

	// Fetches an image for the url, completionhandler will fire with the Texture2D or null
	public IEnumerator fetchImageAtUrl(string url, Action<Texture2D> completionHandler){
		var www = new WWW (url);

		yield return www;

		if (!string.IsNullOrEmpty (www.error)) {
			Debug.Log("Error attempting to load profile image: " + www.error);
			if(completionHandler != null)
				completionHandler (null);
		}else{
			if(completionHandler != null)
				completionHandler(www.texture);
		}
	}
	#endregion
}
