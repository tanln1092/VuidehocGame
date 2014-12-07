using UnityEngine;
using System.Collections;

public class LevelLoading : MonoBehaviour {

	public GameObject[] objList; 
	// Use this for initialization
	void Start () {
		iTween.PunchPosition(objList[0],iTween.Hash("y", .05,	                                
		                                            "time", 1
		                                            , "easeType"
		                                            , "easeInOutQuad", "loopType", "pingPong"));
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time % 3.3f > 3.2f) {
			Application.LoadLevel("Log In");		
		}
	}
}
