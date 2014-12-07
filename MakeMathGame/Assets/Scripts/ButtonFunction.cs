using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UFTButton.onButtonClick += onButtonClick;

	}

	void onButtonClick (UFTButton button){
		// Use this if user choose menu game button
		if (button.name.Equals ("btnMapGame")) {
			Application.LoadLevel("Map Game");
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
