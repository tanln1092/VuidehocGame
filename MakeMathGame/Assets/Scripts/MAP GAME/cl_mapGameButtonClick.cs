using UnityEngine;
using System.Collections;

public class cl_mapGameButtonClick : MonoBehaviour {
	public cl_FacebookPlugin FBPlugin;

	void OnClick () {
		string buttonName = gameObject.name;
		if (buttonName.Equals ("close_button")) {
			Application.LoadLevel("Log in");
		}else{
			if(!FacebookGUI._isInitialized){
				FBPlugin = new cl_FacebookPlugin ();

				FBPlugin.Init(InitializeCallback);
			}

			btn_clicked_event.InstanceMap.onButtonClick (buttonName);

		}

	}

	private void InitializeCallback(bool isSuccess){
		FacebookGUI._isInitialized = isSuccess;
	}
}
