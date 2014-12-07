using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cl_Level7 : MonoBehaviour {

	public GameObject[] mainObjectList;
	public UISprite[] quizzObjectList;
	public GameObject[] answerObjectList;

	public static List<int> checkList;
	public static List<int> AnswerInQuiz;
	public static List<int> positionQuiz;

	// Use this to know start quizz position
	public static int iIndex = 0;
	public static int iPosition = 0;
	int iLength = 0;
	float fMenuNotifTime = 0;
	int iSaveGameCount;
	int iUnclockLevelCount;

	public GUIStyle styleQuizzLabel;
	public GUIStyle styleAnswerLabel;
	public GUIStyle styleNotif;

	public UILabel timePlayedLabel;
	public UILabel notifTitleLabel;

	public AudioClip backgroundSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	public AudioClip answerButtonClickedSound;
	public AudioClip playSound;
	public AudioClip winSound;
	public AudioClip loseSound;

	public static bool isSuccessAnimation;
	public static bool isRight;
	public static bool isExistedLevel;

	List<Program.cl_ObjectPosition> ObjectPositionList;

	public static cl_Level7 Instance7;
	void Awake()
	{
		Instance7 = this;
	}

	// Use this for initialization
	void Start () {
		UFTButton.onButtonClick += onButtonClick;

		// Use this get data from database
		StartCoroutine( cl_DataManager.getUserInfo (Program.userId));

		StartCoroutine( cl_DataManager.getQuizz (cl_DataManager.iRandomNumber));

		ObjectPositionList = new List<Program.cl_ObjectPosition> ();
		foreach (var obj in answerObjectList) {
			Program.cl_ObjectPosition cl_ObjectPosition = new Program.cl_ObjectPosition(obj.transform.position.x,
			                                                                            obj.transform.position.y,
			                                                                            obj.transform.position.z);
			ObjectPositionList.Add(cl_ObjectPosition);
		}
		foreach (var obj in quizzObjectList) {
			Program.cl_ObjectPosition cl_ObjectPosition1 = new Program.cl_ObjectPosition(obj.transform.position.x,
			                                                                            obj.transform.position.y,
			                                                                            obj.transform.position.z);
			ObjectPositionList.Add(cl_ObjectPosition1);
		}
		foreach (var obj in mainObjectList) {
			Program.cl_ObjectPosition cl_ObjectPosition2 = new Program.cl_ObjectPosition(obj.transform.position.x,
			                                                                             obj.transform.position.y,
			                                                                             obj.transform.position.z);
			ObjectPositionList.Add(cl_ObjectPosition2);
		}

		// Use this count quizz and answer total
		iLength = quizzObjectList.Length + answerObjectList.Length;

		checkList = new List<int> ();
		AnswerInQuiz = new List<int> ();
		positionQuiz = new List<int> ();

		for(int i = 0; i < answerObjectList.Length; i++){
			iTween.MoveTo(answerObjectList[i], iTween.Hash("y", 1000, 
			                                               "x", 1000,
			                                               "speed", 1000));
		}

		// Use this create play button animation
		iTween.RotateBy(mainObjectList[0],iTween.Hash("x", .15,"time",1, "easeType"
		                                        , "easeInOutQuad", "loopType", "pingPong", "delay", .5));	

		// Use this menu notif animation
		iTween.MoveTo(mainObjectList[10],iTween.Hash("y", 1.3,
			                                             "speed", 10,
		                                             	 "oncomplete", "mPauseGame",
			                                             "oncompletetarget", gameObject));		
		
	}
	
	int iLoadedQuizzSuccess;
	int iNotifResult;

	void Update(){
		// Use this show success level from database
		if(iLoadedQuizzSuccess == 0){
			if (cl_DataManager.bRequestGetQuizzSuccess) {
				iLoadedQuizzSuccess = 1;
				cl_DataManager.bRequestGetQuizzSuccess = false;

				for(int i = 0; i < Program.quizzList[Program.quizzId].Answer_en.Length; i++){
					checkList.Add(0);
					positionQuiz.Add(-1);
					
				}
				
				for(int i = 0; i < 14; i++){
					AnswerInQuiz.Add(-1);
				}

			}
			
		}

		if(Time.time % 7.8f > 7.7f)
			audio.PlayOneShot(backgroundSound) ;

		if(Program.isHelp == true)
			fTimer = Time.time;
		if (fTimer % 2.2f > 2.1f && Program.isHelp == true) {
			Program.isHelp = false;		
			fTimer = 0f;
			iTween.MoveTo(mainObjectList[9], iTween.Hash("x", ObjectPositionList [iLength + 9].X,
			                                             "y", ObjectPositionList [iLength + 9].Y,
			                                             "z", ObjectPositionList [iLength + 9].Z));
		}
		if(Program.isPause == true)
			fTimer = Time.time;
		if (fTimer % 13f > 12.9f && Program.isPause == true) {
			Program.isPause = false;		
			fTimer = 0f;
			iTween.MoveTo(mainObjectList[10], iTween.Hash("x", ObjectPositionList [iLength + 10].X,
			                                              "y", ObjectPositionList [iLength + 10].Y,
			                                              "z", ObjectPositionList [iLength + 10].Z));
		}

		// Use this stop game
		if (Program.isStop) {
			fMenuNotifTime = Time.timeSinceLevelLoad;
			if(fMenuNotifTime > 20){
				// Use this when game is stopped
				
				
				
				if(cl_Level7.isRight){
					Program.isRun = false;
					iTween.MoveTo(mainObjectList[5], iTween.Hash(
						"y", 10
						));
					
					iTween.MoveTo(mainObjectList[11],iTween.Hash("y", 0.6));
					// Use this to turn on win game sound
					if(iNotifResult == 0){
						audio.PlayOneShot (winSound);
						iNotifResult ++;
					}
					notifTitleLabel.text = "Qua màn";
					timePlayedLabel.text = Mathf.Round(Program.playTime * 100)/100f + " s";
					
					iTween.MoveTo(mainObjectList[15], iTween.Hash(
						"y", 0.3
						));
					
					iTween.MoveTo(mainObjectList[12], iTween.Hash(
						"y", 0.48
						));

					Program.starNumber = 1;

					float fTimePercent = (Mathf.Round(Program.playTime * 100)/100f)/Program.levelAllList[Program.gameLevel - 1].GameTime ;
					if(fTimePercent < 0.85){
						iTween.MoveTo(mainObjectList[13], iTween.Hash(
							"y", 0.54
							));
						Program.starNumber = 2;

					}
					if(fTimePercent < 0.65){
						iTween.MoveTo(mainObjectList[14], iTween.Hash(
							"y", 0.48
							));
						Program.starNumber = 3;

					}

					if(Program.starNumber >= Program.levelList[Program.gameLevel - 1].StarNumber){
						if (iSaveGameCount == 0) {
							int playedTime = (int)(Mathf.Round(Program.playTime * 100)/100f);
							iSaveGameCount = 1;

							// Use this save level completed
							StartCoroutine (cl_DataManager.saveGame (Program.userId, Program.IQTotal + Program.score, Program.MoneyTotal + Program.score,
							                                         Program.gameLevel, playedTime, Program.starNumber));

//							// Use this write log tracking
//							StartCoroutine (cl_DataManager.writeLogTracking (Program.userId, Program.gameLevel, Program.quizzList[Program.quizzId].Id,
//							                                                 playedTime,  Program.levelAllList[Program.gameLevel - 1].GameTime, "Completed"));
//
//							// Use this write log tracking file
//							string contentLog = Program.userId + "\t" + Program.gameLevel + "\t" +  Program.quizzList[Program.quizzId].Id + "\t"
//								+ playedTime + "\t" + Program.levelAllList[Program.gameLevel - 1].GameTime + "\t"
//									+ "Completed\t";
//							cl_DataManager.writeContentTrackingLogFile(contentLog);
//						}		
						
//						if (iUnclockLevelCount == 0) {
//							iUnclockLevelCount = 1;
//							if (Program.levelList.Count < Program.gameLevel + 1) {
//								//	 Unclock next level
//								StartCoroutine (cl_DataManager.unclockLevel (Program.userId, Program.gameLevel + 1));
//							}
						}
					}
//					else{
//						if (iSaveGameCount == 0) {
//							iSaveGameCount = 1;
//							int playedTime = (int)(Mathf.Round(Program.playTime * 100)/100f);
//
//							// Use this write log tracking
//							StartCoroutine (cl_DataManager.writeLogTracking (Program.userId, Program.gameLevel, Program.quizzList[Program.quizzId].Id,
//							                                                 playedTime,  Program.levelAllList[Program.gameLevel - 1].GameTime, "Completed"));
//
//							// Use this write log tracking file
//							string contentLog = Program.userId + "\t" + Program.gameLevel + "\t" 
//								+ playedTime + "\t" + Program.levelAllList[Program.gameLevel - 1].GameTime + "\t"
//									+ "Completed";
////							cl_DataManager.writeContentTrackingLogFile(contentLog);
//
//						}
//					}

					if(cl_DataManager.bRequestSaveGameSuccess){
						cl_DataManager.bRequestSaveGameSuccess = false;

						int playedTime = (int)(Mathf.Round(Program.playTime * 100)/100f);
						
						// Use this write log tracking
						StartCoroutine (cl_DataManager.writeLogTracking (Program.userId, Program.gameLevel, Program.quizzList[Program.quizzId].Id,
						                                                 playedTime,  Program.levelAllList[Program.gameLevel - 1].GameTime, "Completed"));
						
						// Use this write log tracking file
						string contentLog = Program.userId + "\t" + Program.gameLevel + "\t" 
							+ playedTime + "\t" + Program.levelAllList[Program.gameLevel - 1].GameTime + "\t"
								+ "Completed";
					}
					if(cl_DataManager.bRequestWriteLogTrackingSuccess){
						cl_DataManager.bRequestWriteLogTrackingSuccess = false;

						if (Program.levelList.Count < Program.gameLevel + 1) {
							//	 Unclock next level
							StartCoroutine (cl_DataManager.unclockLevel (Program.userId, Program.gameLevel + 1));
						}else{
							isExistedLevel = true;
						}

					}
					if(cl_DataManager.bRequestUnclockLevelSuccess || isExistedLevel){

						iTween.MoveTo(mainObjectList[3], iTween.Hash(
							"y", -0.4
							));
						iTween.MoveTo(mainObjectList[4], iTween.Hash(
							"y", -0.4
							));
					}


				}else{
					Program.isRun = false;
					iTween.MoveTo(mainObjectList[5], iTween.Hash(
						"y", 10
						));
					
					iTween.MoveTo(mainObjectList[11],iTween.Hash("y", 0.6));
					// Use this to turn on lose game sound
					if(iNotifResult == 0){
						audio.PlayOneShot (loseSound);
						iNotifResult ++;
						int playedTime = (int)(Mathf.Round(Program.playTime * 100)/100f);

						// Use this write log tracking
						StartCoroutine (cl_DataManager.writeLogTracking (Program.userId, Program.gameLevel, Program.quizzList[Program.quizzId].Id,
						                                                 playedTime,  Program.levelAllList[Program.gameLevel - 1].GameTime, "Incomplete"));

						// Use this write log tracking file
						string contentLog = Program.userId + "\t" + Program.gameLevel + "\t" + Program.quizzList[Program.quizzId].Id + "\t"
							+ playedTime + "\t" + Program.levelAllList[Program.gameLevel - 1].GameTime + "\t"
								+ "Incomplete";
//						cl_DataManager.writeContentTrackingLogFile(contentLog);

					}
					notifTitleLabel.text = "Thử lại nào";
					timePlayedLabel.text = Mathf.Round(Program.playTime * 100)/100f + " s";
					
					iTween.MoveTo(mainObjectList[16], iTween.Hash(
						"y", 0.3
						));
					iTween.MoveTo(mainObjectList[4], iTween.Hash(
						"y", -0.4
						));
				}
				reset();
				
			}
		}


	}

	void onButtonClick (UFTButton button){

			// Use this when play button is clicked
		if (button.name.Equals ("btnHelp_7")) {
			if(Program.isPause == false && Program.isClicked == true){
				audio.PlayOneShot(answerButtonClickedSound);

				Program.isHelp = true;
				Program.iHelp += 5;

				iTween.MoveTo(mainObjectList[9], iTween.Hash("y", -40,
				                                             "x", 30,
				                                             "time", 2));

				mHelp();
			}
		}

	}
	int count = 0;
	string answerString = "";
	float fTimer = 0f;
	void OnGUI(){
		if(Program.isClicked == true && isSuccessAnimation == true && Program.isPause == false && !Program.isStop){

			string answerRandom = Program.quizzList[Program.quizzId].AnswerRandom;

			// Use this to show quizz content
			GUI.Button (new Rect (mainObjectList[5].transform.position.x + Screen.width * 0.28f, 
			                      mainObjectList[5].transform.position.y + Screen.height * 0.16f,
			                      50, 
			                      50), 
						          Program.quizzList[Program.quizzId].QuizzContent, 
						          styleQuizzLabel);

			// Use this to show title quizz
			GUI.Button (new Rect (mainObjectList[5].transform.position.x + Screen.width * 0.25f, 
			                      mainObjectList[5].transform.position.y + Screen.height * 0.45f,
			                      50, 
			                      50), 
			            Program.quizzList[Program.quizzId].QuizzTitle, 
			            styleQuizzLabel);
			int iStop = 0;
			// Use this to show answer symbols
			for(int index = 0; index < 12; index++){
				if(AnswerInQuiz[index] != -1){
					if(isRight == false){

						iStop = Program.quizzId;
					}
				
				}


			}

		}
	}

	private bool SuccessAnimation(){
		return(isSuccessAnimation = true);
	}

	void ResultNotif(){
		Program.isStop = true;

	}

	void mPauseGame(){
		Program.isPause = true;
	}

	void mHelp(){
		bool isBreak = false;
		for (int index = 0; index < Program.quizzList [Program.quizzId].Answer_en.Length; index++) {
			// Use this to check answer object position
			for (int position = 0; position < answerObjectList.Length; position++) {
				if (answerObjectList [position].transform.position.y == ObjectPositionList [position].Y
				    && Program.quizzList[Program.quizzId].AnswerRandom[position].ToString().Equals(Program.quizzList [Program.quizzId].Answer_en[index].ToString())) {
					
					for(int i = 0; i < checkList.Count; i++){
						if(checkList[i] == 0){
							checkList[i] = 1;
							positionQuiz[i] = position;
							
							// Use this to transform answer object to quizz object position
							iTween.MoveTo(answerObjectList[position], iTween.Hash("x", quizzObjectList[iIndex + i].transform.position.x,
							                                                      "y", quizzObjectList[iIndex + i].transform.position.y,
							                                                      "z", -1,
							                                                      "speed", 1000));
							
							AnswerInQuiz[i + iIndex] = position;
							
							// Use this to increase choose number
							count++;
							
							isBreak = true;
							break;
						}	
											if(isBreak == true)
												break;
					}
//					if(isBreak == true)
//						break;
				}
				if(isBreak == true)
					break;
			}
		}
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds(2);	
	}
	public void ShowEmptyAnswer(int index){
		int length = Program.quizzList[index].Answer_en.Length;
		if (length == 12 || length == 11) {
			for (int i = 0; i < length; i++) {
				quizzObjectList[i].fillAmount = 1;
			}		
		} else if (length == 10 || length == 9) {
			for (int i = 1; i < length + 1; i++) {
				quizzObjectList[i].fillAmount = 1;
//				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 1;
		} else if(length == 8 || length == 7){
			for (int i = 2; i < length + 2; i++) {
				quizzObjectList[i].fillAmount = 1;
//				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 2;
		}else if(length == 6 || length == 5){
			for (int i = 3; i < length + 3; i++) {
				quizzObjectList[i].fillAmount = 1;
//				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 3;
		}else if(length == 4 || length == 3){
			for (int i = 4; i < length + 4; i++) {
				quizzObjectList[i].fillAmount = 1;
//				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 4;
		}else{
			for (int i = 5; i < length + 5; i++) {
				quizzObjectList[i].fillAmount = 1;
//				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}
			iIndex = 5;
		}
	}

	private void reset(){

//		checkList = new List<int> ();
//		AnswerInQuiz = new List<int> ();
//		positionQuiz = new List<int> ();
//		for(int i = 0; i < Program.quizzList[Program.quizzId].Answer_en.Length; i++){
//			checkList.Add(0);
//			positionQuiz.Add(-1);
//
//		}
//		
		for(int i = 0; i < answerObjectList.Length; i++){
//			AnswerInQuiz.Add(-1);
			iTween.MoveTo(answerObjectList[i], iTween.Hash("x", ObjectPositionList [i].X,
			                                               "y", ObjectPositionList [i].Y,
			                                               "z", ObjectPositionList [i].Z));
		}
//		for(int j = answerObjectList.Length; j < iLength; j++){
//			iTween.MoveTo(quizzObjectList[j - 14], iTween.Hash("x", ObjectPositionList [j].X,
//			                                               "y", ObjectPositionList [j].Y,
//			                                               "z", 1));
//		}

		count = 0;
		answerString = "";
//		ShowEmptyAnswer (Program.quizzId + 1);
	}
	#region BUTTON FUNCTION
	public void onPlayButtonFunction(){
		if(Program.isPause == false){
			Program.isClicked = true;
			
			// Use this to turn on play sound
			audio.PlayOneShot (playSound);
			
		// Use this move play button
		iTween.MoveTo (mainObjectList [0], iTween.Hash ("y", 1000));

			for(int i = 0; i < answerObjectList.Length; i++){
				iTween.MoveTo(answerObjectList[i], iTween.Hash("x", ObjectPositionList [i].X,
				                                               "y", ObjectPositionList [i].Y,
				                                               "z", ObjectPositionList [i].Z,
				                                               "oncomplete", "SuccessAnimation",
				                                               "oncompletetarget", gameObject));
				iTween.MoveTo(mainObjectList[8], iTween.Hash("z", -1));
			}
			
			
			// Use this move question board object
			iTween.MoveTo(mainObjectList[5],iTween.Hash("y", 1.0,
			                                            "speed", 5));
			// Use this move go label object
			iTween.MoveTo(mainObjectList[1],iTween.Hash("z", -1));
			iTween.MoveTo(mainObjectList[1],iTween.Hash("position",transform.position += Vector3.right*22,
			                                            "easetype",iTween.EaseType.easeInOutSine,
			                                            "time", 1,
			                                            "delay", 0.01));
			ShowEmptyAnswer (Program.quizzId);
		}
	}

	public void onAnswerButtonFunction(int index){
		if(isRight)
			return;

		if(Program.isPause == false){
			// Use this to turn on answer button clicked
			audio.PlayOneShot(answerButtonClickedSound);

			// Use this to check answer object position
		    if(Mathf.Round(answerObjectList[index].transform.position.y * 100)/100f == Mathf.Round(ObjectPositionList [index].Y * 100)/100f){

				for(int i = 0; i < checkList.Count; i++){
					if(checkList[i] == 0){
						checkList[i] = 1;
						positionQuiz[i] = index;
						
						// Use this to transform answer object to quizz object position
						iTween.MoveTo(answerObjectList[index], iTween.Hash("x", quizzObjectList[iIndex + i].transform.position.x,
						                                                   "y", quizzObjectList[iIndex + i].transform.position.y,
						                                                   "z", -1,
						                                                   "speed", 1000));
						
						AnswerInQuiz[i + iIndex] = index;
						
						// Use this to increase choose number
						count++;
						
						if(count == Program.quizzList[Program.quizzId].Answer_en.Length){
							answerString = "";
							for(int j = 0; j < AnswerInQuiz.Count; j++){
								if(AnswerInQuiz[j] != -1){
									answerString = string.Concat(answerString, Program.quizzList[Program.quizzId].AnswerRandom[AnswerInQuiz[j]].ToString());
								}
							}
							
							// Use this show right choose
							if(answerString.Equals(Program.quizzList[Program.quizzId].Answer_en)){

								// Use this turn on right sound 
								audio.PlayOneShot(rightSound);
								
								iTween.PunchPosition(mainObjectList[6],iTween.Hash("z", 1, 
								                                                   "time", 2));	
								
								// Use this increase point
								Program.score ++;
								isRight = true;

//								for(int j = answerObjectList.Length; j < iLength; j++){
//									iTween.MoveTo(quizzObjectList[j - 14], iTween.Hash("x", ObjectPositionList [j].X,
//									                                                   "y", ObjectPositionList [j].Y,
//									                                                   "z", 1));
//								}
								
								for(int position = 0; position < Program.quizzList [Program.quizzId].Answer_en.Length; position++){
									iTween.PunchScale(answerObjectList[positionQuiz[position]],iTween.Hash("x", 0.0009,
									                                                                       "time", 2, 
									                                                                       "oncomplete", "ResultNotif",
									                                                                       "oncompletetarget", gameObject));
								}

							}else{
								// Use this turn on wrong sound 
								audio.PlayOneShot(wrongSound);
								iTween.PunchPosition(mainObjectList[7],iTween.Hash("z", 1, 
								                                                   "time", 2));	
							}
						}
						break;
					}
					
				}
				
				
			}
			else{
				for(int i = 0; i < checkList.Count; i++){
					if(positionQuiz[i] == index){
						checkList[i] = 0;
						positionQuiz[i] = -1;
						
						iPosition = iIndex + i;
						iTween.MoveTo(answerObjectList[index], iTween.Hash("x", ObjectPositionList [index].X,
						                                                   "y", ObjectPositionList [index].Y,
						                                                   "z", ObjectPositionList [index].Z,
						                                                   "speed", 1000));
						AnswerInQuiz[i + iIndex] = -1;
						count--;
						break;
					}
					
				}
				
				
			}
			
		}
	
	}

	public void onInfomationButtonFunction(){
		if(isRight)
			return;

		if(Program.isPause == false)
			iTween.MoveTo(mainObjectList[10],iTween.Hash("y", 1.3,
			                                             "speed", 10,
			                                             "oncomplete", "mPauseGame",
			                                             "oncompletetarget", gameObject));
		else{
			Program.isPause = false;		
			fTimer = 0f;
			iTween.MoveTo(mainObjectList[10], iTween.Hash("x", ObjectPositionList [iLength + 10].X,
			                                              "y", ObjectPositionList [iLength + 10].Y,
			                                              "z", ObjectPositionList [iLength + 10].Z));
		}

	}
	#endregion
}
