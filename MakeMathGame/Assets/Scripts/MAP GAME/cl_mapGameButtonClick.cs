using UnityEngine;
using System.Collections;

public class cl_mapGameButtonClick : MonoBehaviour {

	void OnClick () {
		string buttonName = gameObject.name;
		btn_clicked_event.InstanceMap.onButtonClick (buttonName);

	}
}
