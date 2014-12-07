using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class btn_clicked_event : MonoBehaviour {

	private List<string> buttonNameArray;
	
	public GameObject[] buttonObjectList;
	public GameObject[] starObjectList;
	public UISprite[] starSpriteList;

	private static List<Program.cl_ObjectPosition> ObjectPositionList;

	// Use this to turn on sound
	public AudioClip backgroundSound;
	public AudioClip clickedSound;

	// Use this for initialization
	void Start () {
		// Use this save the all button object position
		ObjectPositionList = new List<Program.cl_ObjectPosition> ();
		foreach (var obj in buttonObjectList) {
			Program.cl_ObjectPosition cl_ObjectPosition = new Program.cl_ObjectPosition(obj.transform.position.x,
			                                                                            obj.transform.position.y,
			                                                                            obj.transform.position.z);
			ObjectPositionList.Add(cl_ObjectPosition);
		}

		// Use this save the all star object
		foreach (var obj in starObjectList) {
			Program.cl_ObjectPosition cl_ObjectPosition = new Program.cl_ObjectPosition(obj.transform.position.x,
			                                                                            obj.transform.position.y,
			                                                                            obj.transform.position.z);
			ObjectPositionList.Add(cl_ObjectPosition);
		}

		// Use this hide buttons object
		for(int index = 0; index < buttonObjectList.Length; index++){
			iTween.MoveTo(buttonObjectList[index],iTween.Hash("z", 1));
			iTween.MoveTo(buttonObjectList[index],iTween.Hash("y", 1000));
		}

		// Use this hide stars object
		for(int index = 0; index < starObjectList.Length; index++){
			iTween.MoveTo(starObjectList[index],iTween.Hash("z", 1));
			iTween.MoveTo(starObjectList[index],iTween.Hash("y", 1000));
		}

		// Use this add name button list
		buttonNameArray = new List<string> ();
		for(int i = 0; i < buttonObjectList.Length; i++){
			buttonNameArray.Add("Level " + (i + 1));
		}

		// Use this load data
		StartCoroutine ( cl_DataManager.getGameLevel(Program.userId));
	}

	int iLoadedLevelSuccess;
	void Update(){
		// Use this show success level from database
		if(iLoadedLevelSuccess == 0){
			if (cl_DataManager.bRequestGetUserInfoSuccess) {
				iLoadedLevelSuccess = 1;
				cl_DataManager.bRequestGetUserInfoSuccess = false;
				LoadingSuccessLevel (buttonObjectList, starObjectList);
			}
					
		}
		// Use this turn on background sound
				if(Time.time % 7.8f > 7.7f)
					audio.PlayOneShot(backgroundSound) ;
	}

	public void onButtonClick (string nameButtonClicked){

			// Use this to turn on clicked sound
			audio.PlayOneShot(clickedSound) ;

			iLoadedLevelSuccess = 0;
		Program.gameLevel = int.Parse(nameButtonClicked);

			Application.LoadLevel("Level " + nameButtonClicked);

	}

	public static btn_clicked_event InstanceMap;
	void Awake()
	{

		InstanceMap = this;

	}

	void returnPositionObject(){
		for(int i = 0; i < buttonObjectList.Length; i++){

		}
	}
	public void LoadingSuccessLevel(GameObject[] buttonObj, GameObject[] starObj){

		for (int index = 0; index < Program.levelList.Count; index++) {
			Debug.Log("load level");
					iTween.MoveTo (buttonObj[index], iTween.Hash ("x", ObjectPositionList [index].X,
					                                              "y", ObjectPositionList [index].Y,
					                                              "z", ObjectPositionList [index].Z,
								                                  "speed", 10000,
								                                  "delay", 0.01));

				switch(Program.levelList[index].StarNumber){
					case 1:
				starSpriteList[index*3].fillAmount = 1;
//						iTween.MoveTo (starObj[index*3], iTween.Hash ("x", ObjectPositionList [buttonObj.Length + index*3].X,
//						                                              "y", ObjectPositionList [buttonObj.Length + index*3].Y,
//						                                              "z", ObjectPositionList [buttonObj.Length + index*3].Z,
//								                                          "speed", 10000,
//								                                          "delay", 0.01));
							break;
					case 2:
				starSpriteList[index*3].fillAmount = 1;
				starSpriteList[index*3 + 1].fillAmount = 1;

//						iTween.MoveTo (starObj[index*3], iTween.Hash ("x", ObjectPositionList [buttonObj.Length + index*3].X,
//						                                              "y", ObjectPositionList [buttonObj.Length + index*3].Y,
//						                                              "z", ObjectPositionList [buttonObj.Length + index*3].Z,
//						                                              "speed", 10000,
//						                                              "delay", 0.01));
//						iTween.MoveTo (starObj[index*3 + 1], iTween.Hash ("x", ObjectPositionList [buttonObj.Length + index*3 + 1].X,
//						                                                  "y", ObjectPositionList [buttonObj.Length + index*3 + 1].Y,
//						                                                  "z", ObjectPositionList [buttonObj.Length + index*3 + 1].Z,
//						                                                  "speed", 10000,
//						                                                  "delay", 0.01));
						break;
					case 3:
				starSpriteList[index*3].fillAmount = 1;
				starSpriteList[index*3 + 1].fillAmount = 1;
				starSpriteList[index*3 + 2].fillAmount = 1;


//						iTween.MoveTo (starObj[index*3], iTween.Hash ("x", ObjectPositionList [buttonObj.Length + index*3].X,
//						                                              "y", ObjectPositionList [buttonObj.Length + index*3].Y,
//						                                              "z", ObjectPositionList [buttonObj.Length + index*3].Z,
//						                                              "speed", 10000,
//						                                              "delay", 0.01));
//						iTween.MoveTo (starObj[index*3 + 1], iTween.Hash ("x", ObjectPositionList [buttonObj.Length + index*3 + 1].X,
//						                                                  "y", ObjectPositionList [buttonObj.Length + index*3 + 1].Y,
//						                                                  "z", ObjectPositionList [buttonObj.Length + index*3 + 1].Z,
//						                                                  "speed", 10000,
//						                                                  "delay", 0.01));						
//						iTween.MoveTo (starObj[index*3 + 2], iTween.Hash ("x", ObjectPositionList [buttonObj.Length + index*3 + 2].X,
//						                                                  "y", ObjectPositionList [buttonObj.Length + index*3 + 2].Y,
//						                                                  "z", ObjectPositionList [buttonObj.Length + index*3 + 2].Z,
//						                                                  "speed", 10000,
//						                                                  "delay", 0.01));	
						break;
//				}
			}		
		}
	}
}
