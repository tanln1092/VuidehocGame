using UnityEngine;
using System.Collections;

public class cl_SetProfileFBImage : MonoBehaviour {

//	public string FacebookId = "614363115340002";
//	private string graphUrl = "http://graph.facebook.com/{0}/picture";
	
	void Start () {
		StartCoroutine(GetFacebookProfilePicture());
	}
	
	IEnumerator GetFacebookProfilePicture() {
//		var request = string.Format(graphUrl, FacebookId);

		string request = "http://graph.facebook.com/614363115340002/picture";
		WWW loader = new WWW(request);
		yield return loader;
		
		renderer.material.mainTexture = loader.texture;
		renderer.material.shader = Shader.Find("Unlit/Transparent");
//		renderer.material.mainTexture = new Texture2D(4, 4, TextureFormat.RGB565, false);

	}
}
