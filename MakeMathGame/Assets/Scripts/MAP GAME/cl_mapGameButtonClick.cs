using UnityEngine;
using System.Collections;

public class cl_mapGameButtonClick : MonoBehaviour {

	void OnClick () {
		string buttonName = gameObject.name;
		if (buttonName.Equals ("close_button")) {
			Application.LoadLevel("Log in");
		}else
			btn_clicked_event.InstanceMap.onButtonClick (buttonName);

	}
}
