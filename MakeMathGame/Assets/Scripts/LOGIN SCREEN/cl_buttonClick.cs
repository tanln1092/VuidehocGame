using UnityEngine;
using System.Collections;

public class cl_buttonClick : MonoBehaviour {

	void OnClick () {
		string buttonName = gameObject.name;

		switch (buttonName) {
		case "close_notif_button":
			Main.Instance.onCloseNotifMenuButtonClick ();

			break;
		case "close_register_menu_button":
			Main.Instance.onCloseResgisterMenuButtonClick ();
			
			break;
		case "login_button":
			Main.Instance.onLoginButtonClick ();
			break;
		case "register_button":
			Main.Instance.onResisterButtonClick ();
			break;
		}

	}
}
