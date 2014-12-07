using UnityEngine;
using System.Collections;

public class HelpObject : MonoBehaviour {

	public GUIStyle styleAnswerLabel;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI () {
		if (Program.isHelp == true){
			GUI.Label (new Rect (gameObject.transform.position.x + Screen.width*0.1f, 
			                     gameObject.transform.position.y + Screen.height * 0.05f,
			                     50, 
			                     50), 
						         "- 5", 
						         styleAnswerLabel);
		}
		
	}
}
