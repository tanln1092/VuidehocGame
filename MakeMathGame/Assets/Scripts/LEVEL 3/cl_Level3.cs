using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cl_Level3 : MonoBehaviour {

	// Use this create object list need use
	public GameObject[] objList;

	// Use this to turn on sound
	public AudioClip backgroundSound;
	public AudioClip clickedSound;
	public AudioClip getAwardSound;
	public AudioClip playSound;
	public AudioClip winSound;
	public AudioClip loseSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;

	float fTimer = 0f;
	int iNotifResult;
	List<Program.cl_ObjectPosition> ObjectPositionList;

	// Use this for initialization
	void Start () {

		Debug.Log ("run");
		// Use this get data from database
		StartCoroutine( cl_DataManager.getUserInfo (Program.userId));

		// Use this save the all object position
		ObjectPositionList = new List<Program.cl_ObjectPosition> ();
		foreach (var obj in objList) {
			Program.cl_ObjectPosition cl_ObjectPosition = new Program.cl_ObjectPosition(obj.transform.position.x,
									                                                    obj.transform.position.y,
									                                                    obj.transform.position.z);
			ObjectPositionList.Add(cl_ObjectPosition);
		}

		// Use this create play button animation
//		iTween.RotateBy(objList[14],iTween.Hash("x", .15,"time",1, "easeType"
//		                                       , "easeInOutQuad", "loopType", "pingPong", "delay", .5));	
//
//		// Use this create next button animation
//		iTween.RotateBy(objList[27],iTween.Hash("x", .15,"time",1, "easeType"
//	                                        , "easeInOutQuad", "loopType", "pingPong", "delay", .2));
//
//		// Use this create reload button animation
//		iTween.RotateBy(objList[26],iTween.Hash("x", .15,"time",1, "easeType"
//		                                        , "easeInOutQuad", "loopType", "pingPong"));

		// Use this menu notif animation
		iTween.MoveTo(objList[38],iTween.Hash("y", 4,
		                                      "speed", 10,
		                                      "oncomplete", "mPauseGame",
		                                      "oncompletetarget", gameObject));
	}


	public bool completed(){
		return Program.isClicked = true;
	}
	public bool resultNotif(){
		return Program.isResultNotif = true;
	}


	int iSaveGameCount;
	int iUnclockLevelCount;
	void Update(){

		// Use this turn on background sound
		//		if(Time.time % 7.8f > 7.7f)
		//			audio.PlayOneShot(backgroundSound) ;

		if(Program.isPause == true)
			fTimer = Time.time;
		if (fTimer % 7f > 6.9f && Program.isPause == true) {
			Program.isPause = false;		
			fTimer = 0f;
			iTween.MoveTo(objList[38], iTween.Hash("x", ObjectPositionList [38].X,
			                                       "y", ObjectPositionList [38].Y,
			                                       "z", ObjectPositionList [38].Z));
		}


		// Use this stop game
		if (Program.isStop == true) {

			// Use this when game is stopped
			Program.isRun = false;

			// Use this to hide answerButton2 & answerButton1
			iTween.MoveTo(objList[10],iTween.Hash("x", ObjectPositionList[10].X,
			                                      "y", ObjectPositionList[10].Y,
			                                      "z", ObjectPositionList[10].Z, 
			                                      "speed", 1000));
			iTween.MoveTo(objList[24],iTween.Hash("x", ObjectPositionList[24].X,
			                                      "y", ObjectPositionList[24].Y,
			                                      "z", ObjectPositionList[24].Z, 
			                                      "speed", 1000));

			// Use this to hide answerLabel1 & answerLabel2
			iTween.MoveTo(objList[22],iTween.Hash("y", 1000));
			iTween.MoveTo(objList[23],iTween.Hash("y", 1000));

			// Use this to hide question menu
			iTween.MoveTo(objList[11],iTween.Hash("x", ObjectPositionList[11].X,
			                                      "y", ObjectPositionList[11].Y,
			                                      "z", ObjectPositionList[11].Z));

			// Use this to hide question label object
			iTween.MoveTo(objList[28],iTween.Hash("y", 1000));

			iTween.MoveTo (objList [36], iTween.Hash ("x", ObjectPositionList [36].X,
			                                          "y", ObjectPositionList [36].Y,
			                                          "z", ObjectPositionList [36].Z, 
			                                          "speed", 1000));

//			if (!Program.levelName.Equals ("Level 2"))
//				return;
			// Use this move notif board object
			iTween.MoveTo(objList[30],iTween.Hash("y", 3,
			                                      "speed", 10,
			                                      "oncomplete", "NotifMenuSuccess",
			                                      "oncompletetarget", gameObject));


			if(Program.score < 8){
				// Use this to turn on lose game sound
				if(iNotifResult == 0){
					audio.PlayOneShot (loseSound);
					iNotifResult ++;
				}

				iTween.MoveTo(objList[35],iTween.Hash("y", 3,
				                                      "speed", 10));
			}else{
				// Use this to turn on win game sound
				if(iNotifResult == 0){
					audio.PlayOneShot (winSound);
					iNotifResult ++;
				}

				iTween.MoveTo(objList[34],iTween.Hash("y", 3,
				                                      "speed", 10));
			}

			// Use this check correct answer number
			switch(Program.score){
			case 8:
				iTween.MoveTo(objList[31],iTween.Hash("y", 4,
				                                      "speed", 10,
				                                      "delay", 0.2));
				Program.starNumber = 1;
				break;
			case 9:
				iTween.MoveTo(objList[31],iTween.Hash("y", 4,
				                                      "speed", 10,
				                                      "delay", 0.2));
				iTween.MoveTo(objList[32],iTween.Hash("y", 4.3,
				                                      "speed", 10,
				                                      "delay", 0.4));
				Program.starNumber = 2;
				break;
			case 10:
				iTween.MoveTo(objList[31],iTween.Hash("y", 4,
				                                      "speed", 10,
				                                      "delay", 0.2));
				iTween.MoveTo(objList[32],iTween.Hash("y", 4.3,
				                                      "speed", 10,
				                                      "delay", 0.4));
				iTween.MoveTo(objList[33],iTween.Hash("y", 4,
				                                      "speed", 10,
				                                      "delay", 0.6));
				Program.starNumber = 3;
				break;
			}
			if(Program.score > 7){
				if(Program.starNumber >= Program.levelList[0].StarNumber){
					if (iSaveGameCount == 0) {
						
						iSaveGameCount = 1;
						StartCoroutine (cl_DataManager.saveGame (Program.userId, Program.IQTotal + Program.score, Program.MoneyTotal + Program.score,
						                                         Program.gameLevel, (int)Program.playTime, Program.starNumber));
						
					}		
					
					if (iUnclockLevelCount == 0 && cl_DataManager.bRequestSaveGameSuccess) {
						iUnclockLevelCount = 1;
						if (Program.levelList.Count < Program.gameLevel + 1) {
							//	 Unclock next level
							StartCoroutine (cl_DataManager.unclockLevel (Program.userId, Program.gameLevel + 1));
						}
					}
					
					if(cl_DataManager.bRequestUnclockLevelSuccess){
						iTween.MoveTo(objList[26],iTween.Hash("y", -0.35,
						                                      "speed", 10));
						iTween.MoveTo(objList[27],iTween.Hash("y", -0.35,
						                                      "speed", 10));
					}
				}else{
					iTween.MoveTo(objList[26],iTween.Hash("y", -0.35,
					                                      "speed", 10));
					iTween.MoveTo(objList[27],iTween.Hash("y", -0.35,
					                                      "speed", 10));
				}
			}else{
				iTween.MoveTo(objList[26],iTween.Hash("y", -0.35,
				                                      "speed", 10));
			}
	}

		// Use this show map game button
//		if(Program.isRun){
//			iTween.MoveTo(objList[37],iTween.Hash("y", 1000,
//			                                      "speed", 1000));
//		}else{
//			iTween.MoveTo(objList[37],iTween.Hash("y", ObjectPositionList[37].Y,
//			                                      "speed", 1000));
//		}

	}

	// Use this check notif menu success
	public bool NotifMenuSuccess(){
		return (Program.isSuccess = true);
	}
	void mPauseGame(){
		Program.isPause = true;
	}

	public static cl_Level3 Instance3;
	void Awake()
	{
		Instance3 = this;
	}

	private void returnObjectPosition(){
		// Use this to hide go image object
		iTween.MoveTo(objList[15],iTween.Hash("z", 1));

		for(int index = 0; index < objList.Length - 1; index++){
			// Use this move money object
			iTween.MoveTo (objList [index], iTween.Hash ("x", ObjectPositionList [index].X,
												    	 "y", ObjectPositionList [index].Y,
													 	 "z", ObjectPositionList [index].Z,
			                                             "speed", 10000,
			                                             "delay", 0.61));
		}
	}

	#region BUTTON FUNCTION
	public void onPlayButtonFunction(){
		if(!Program.isPause){
			// Use this to turn on play sound
			audio.PlayOneShot(playSound) ;
			
			// Use this check game is running
			Program.isRun = true;
			
			iTween.MoveTo(objList[14],iTween.Hash("x", 1000));
			
			// Use this move money object
			iTween.MoveTo(objList[9],iTween.Hash("x", -3));
			iTween.MoveTo(objList[8],iTween.Hash("x", -3.9));
			iTween.MoveTo(objList[7],iTween.Hash("x", -4.75));
			iTween.MoveTo(objList[6],iTween.Hash("x", -5.6));
			iTween.MoveTo(objList[5],iTween.Hash("x", -6.45));
			iTween.MoveTo(objList[4],iTween.Hash("x", -7.3));
			iTween.MoveTo(objList[3],iTween.Hash("x", -8.15));
			iTween.MoveTo(objList[2],iTween.Hash("x", -9));
			iTween.MoveTo(objList[1],iTween.Hash("x", -9.85));
			iTween.MoveTo(objList[0],iTween.Hash("x", -10.7));
			
			// Use this create animation for award object
			iTween.RotateBy(objList[9],iTween.Hash("x", .15,"time",1, "easeType"
			                                       , "easeInOutQuad", "loopType", "pingPong"));
			// Use this move character and idea object
			iTween.MoveTo(objList[17],iTween.Hash("x", -12));
			iTween.MoveTo(objList[18],iTween.Hash("x", -12));
			iTween.MoveTo(objList[16],iTween.Hash("x", -12));
			
			// Use this move answer button object
			iTween.MoveTo(objList[10],iTween.Hash("x", 0.42));
			iTween.MoveTo(objList[24],iTween.Hash("x", -0.5));
			
			// Use this move go label object
			iTween.MoveTo(objList[15],iTween.Hash("position",transform.position += Vector3.right*22,"easetype",iTween.EaseType.easeInOutSine,"time", 1));
			
			// Use this move question board object
			iTween.MoveTo(objList[11],iTween.Hash("y", 3.5,
			                                      "speed", 5, 
			                                      "oncomplete", "completed",
			                                      "oncompletetarget", gameObject));
			
			// Use this move progress time bar object
			iTween.MoveTo(objList[29],iTween.Hash("z", -0.1));
		}
	}

	public void onAnswerFirstButtonFunction(){
		if(!Program.isPause){
			// Use this to turn on clicked sound
			audio.PlayOneShot(clickedSound) ;
			
			// Use this increase question number
			Program.maxQuest ++;
			
			if(QuestionLabel.questionIndex < 10){
				if(AnswerLabel1.randomNumberList[QuestionLabel.questionIndex] == 0){
					// Use this to turn on right sound
					audio.PlayOneShot(rightSound) ;
					
					iTween.PunchPosition(objList[12],iTween.Hash("z", 2, 
					                                             "time", 1));
					
					if(QuestionLabel.questionIndex < 10){
						
						// Use this increase point
						Program.score ++;
						
						// Use this move character and idea object
						iTween.MoveTo(objList[17],iTween.Hash("x", objList[Program.score - 1].transform.position.x));
						iTween.MoveTo(objList[18],iTween.Hash("x", objList[Program.score - 1].transform.position.x));
						iTween.MoveTo(objList[16],iTween.Hash("position", objList[18].transform.position));
						
						// Use this move IQs object
						iTween.MoveTo(objList[16],iTween.Hash("position", objList[19].transform.position,
						                                      "delay", .1));
						
						
						// Use this move money object
						iTween.MoveTo(objList[Program.score - 1],iTween.Hash("position", objList[20].transform.position, 
						                                                     "z", -1,
						                                                     "oncomplete", "resultNotif",
						                                                     "oncompletetarget", gameObject));
						
						// Use this move money total object
						iTween.PunchPosition(objList[21],iTween.Hash("z", 1,
						                                             "delay", .3, 
						                                             "time", 1));
						
						// Use this move money object
						iTween.MoveTo(objList[Program.score - 1],iTween.Hash("z", 1,
						                                                     "delay", .3,
						                                                     "time", 1));
						
						// Use this move IQs total object
						iTween.PunchPosition(objList[25],iTween.Hash("z", 1,
						                                             "delay", .3,
						                                             "time", 1));
					}
				}else{
					iTween.PunchPosition(objList[13],iTween.Hash("z", 1, 
					                                             "time", 1));	
					// Use this to turn on wrong sound
					audio.PlayOneShot(wrongSound) ;
				}	
				
			}
			if(QuestionLabel.questionIndex < 9){
				QuestionLabel.questionIndex ++;
			}
			
			// Use this stop game
			if(Program.maxQuest > 9){
				Program.isStop = true;
			}
		}

	}

	public void onAnswerSecondButtonFunction(){
		if(!Program.isPause){
			// Use this to turn on clicked sound
			audio.PlayOneShot(clickedSound) ;
			
			// Use this increase question number
			Program.maxQuest ++;
			
			if(QuestionLabel.questionIndex < 10){
				if(AnswerLabel1.randomNumberList[QuestionLabel.questionIndex] == 1){
					iTween.PunchPosition(objList[12],iTween.Hash("z", 2, 
					                                             "time", 1));
					// Use this to turn on right sound
					audio.PlayOneShot(rightSound) ;
					
					if(QuestionLabel.questionIndex < 10){
						
						// Use this increase point
						Program.score ++;
						
						// Use this move character and idea object
						iTween.MoveTo(objList[17],iTween.Hash("x", objList[Program.score - 1].transform.position.x));
						iTween.MoveTo(objList[18],iTween.Hash("x", objList[Program.score - 1].transform.position.x));
						iTween.MoveTo(objList[16],iTween.Hash("position", objList[18].transform.position));
						
						// Use this to turn on get award sound
						audio.PlayOneShot(getAwardSound) ;
						
						// Use this move IQs object
						iTween.MoveTo(objList[16],iTween.Hash("position", objList[19].transform.position,
						                                      "delay", .1));
						
						// Use this move money object
						iTween.MoveTo(objList[Program.score - 1],iTween.Hash("position", objList[20].transform.position, 
						                                                     "z", -1,
						                                                     "oncomplete", "resultNotif",
						                                                     "oncompletetarget", gameObject));
						
						// Use this move total money object
						iTween.PunchPosition(objList[21],iTween.Hash("z", 1,
						                                             "delay", .3,
						                                             "time", 1));
						
						// Use this move money object
						iTween.MoveTo(objList[Program.score - 1],iTween.Hash("z", 1,
						                                                     "delay", .3,
						                                                     "time", 1));
						
						// Use this move IQs total object
						iTween.PunchPosition(objList[25],iTween.Hash("z", 1,
						                                             "delay", .3,
						                                             "time", 1));
						
					}
				}else{
					iTween.PunchPosition(objList[13],iTween.Hash("z", 1, 
					                                             "time", 1));	
					// Use this to turn on wrong sound
					audio.PlayOneShot(wrongSound) ;
				}
			}
			if(QuestionLabel.questionIndex < 9){
				QuestionLabel.questionIndex ++;				
			}
			
			// Use this stop game
			if(Program.maxQuest > 9){
				Program.isStop = true;
			}
		}
	}

	public void onInfomationButtonFunction(){
		if(Program.isPause == false)
			iTween.MoveTo(objList[38],iTween.Hash("y", 4,
			                                      "speed", 10,
			                                      "oncomplete", "mPauseGame",
			                                      "oncompletetarget", gameObject));
		else{
			Program.isPause = false;		
			fTimer = 0f;
			iTween.MoveTo(objList[38], iTween.Hash("x", ObjectPositionList [38].X,
			                                       "y", ObjectPositionList [38].Y,
			                                       "z", ObjectPositionList [38].Z));
		}
	}
	#endregion
}
