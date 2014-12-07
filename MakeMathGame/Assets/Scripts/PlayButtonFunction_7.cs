using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayButtonFunction_7 : MonoBehaviour {

	public GameObject[] mainObjectList;
	public GameObject[] quizzObjectList;
	public GameObject[] answerObjectList;

	public static List<int> checkList;
	public static List<int> AnswerInQuiz;
	public static List<int> positionQuiz;

	// Use this to know start quizz position
	public static int iIndex = 0;
	public static int iPosition = 0;
	int iLength = 0;

	public GUIStyle styleQuizzLabel;
	public GUIStyle styleAnswerLabel;
	public GUIStyle styleNotif;

	public AudioClip backgroundSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	public AudioClip answerButtonClickedSound;
	public AudioClip playSound;
	
	public static bool isSuccessAnimation;
	bool isRight;

	List<Program.cl_ObjectPosition> ObjectPositionList;

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
		iTween.MoveTo(mainObjectList[10],iTween.Hash("y", 1.0,
			                                             "speed", 10,
		                                             	 "oncomplete", "mPauseGame",
			                                             "oncompletetarget", gameObject));		
		
	}
	
	int iLoadedQuizzSuccess;
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
	}

	void onButtonClick (UFTButton button){
		// Use this when play button is clicked
		if (button.name.Equals ("btnPlayGame_7")) {
			if(Program.isPause == false){
				Program.isClicked = true;

				// Use this to turn on play sound
				audio.PlayOneShot (playSound);

				for(int i = 0; i < answerObjectList.Length; i++){
					iTween.MoveTo(answerObjectList[i], iTween.Hash("x", ObjectPositionList [i].X,
					                                               "y", ObjectPositionList [i].Y,
					                                               "z", ObjectPositionList [i].Z,
					                                               "oncomplete", "SuccessAnimation",
					                                               "oncompletetarget", gameObject));
					iTween.MoveTo(mainObjectList[8], iTween.Hash("z", -1));
				}

				iTween.MoveTo(mainObjectList[0],iTween.Hash("z", 1));

				// Use this move question board object
				iTween.MoveTo(mainObjectList[5],iTween.Hash("y", .7,
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
		// Use this when close game button is clicked
		if (button.name.Equals ("btnInfo_7")) {
			if(Program.isPause == false)
				iTween.MoveTo(mainObjectList[10],iTween.Hash("y", 1.0,
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
		// Use this when close game button is clicked
		if (button.name.Equals ("btnClose_7")) {
//			Application.Quit();
			Application.LoadLevel("Map Game");
		}
		// Use this when close game button is clicked
		if (button.name.Equals ("btnMapGame_7")) {
//			if(Program.isPause == false){
			Debug.Log("btnMap");
				Application.LoadLevel("Map Game");
//			}
		}

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
		for(int index = 0; index < answerObjectList.Length; index++){
			if(button.name.Equals("Answer_" + (index + 1) + "_7")){
				if(Program.isPause == false){
					// Use this to turn on answer button clicked
					audio.PlayOneShot(answerButtonClickedSound);

					// Use this to check answer object position
					if(answerObjectList[index].transform.position.y == ObjectPositionList [index].Y){

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

										for(int j = answerObjectList.Length; j < iLength; j++){
											iTween.MoveTo(quizzObjectList[j - 14], iTween.Hash("x", ObjectPositionList [j].X,
											                                                   "y", ObjectPositionList [j].Y,
											                                                   "z", 1));
										}

										for(int position = 0; position < AnswerInQuiz.Count; position++){
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
		}
	}
	int count = 0;
	string answerString = "";
	float fTimer = 0f;
	void OnGUI(){
		if(Program.isClicked == true && isSuccessAnimation == true && Program.isPause == false){

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
			                      mainObjectList[5].transform.position.y + Screen.height * 0.50f,
			                      50, 
			                      50), 
			            Program.quizzList[Program.quizzId].QuizzTitle, 
			            styleQuizzLabel);
			int iStop = 0;
			// Use this to show answer symbols
			for(int index = 0; index < 12; index++){
				if(AnswerInQuiz[index] != -1){
					if(isRight == false){
						GUI.Label (new Rect ( answerObjectList[0].transform.position.x + Screen.width * (0.235f + 0.048f*index), 
						                      answerObjectList[0].transform.position.y + Screen.height * 0.63f,
						                      50, 
						                      50), 
									          answerRandom[AnswerInQuiz[index]].ToString(), 
									          styleAnswerLabel);
						iStop = Program.quizzId;
					}
				
				}

				// Use this notif correct answer
				if(isRight == true && index < Program.quizzList[Program.quizzId].Answer_vi.Length){
					GUI.Label (new Rect ( answerObjectList[0].transform.position.x + Screen.width * (0.235f + 0.048f*(iIndex + index)), 
					                     answerObjectList[0].transform.position.y + Screen.height * 0.63f,
					                     50, 
					                     50), 
								         Program.quizzList[Program.quizzId].Answer_vi[index].ToString(), 
								         styleAnswerLabel);

					// Use this delay 3s
					fTimer = Time.time;
					if(fTimer % 3.5f > 3.4f){
						isRight = false;
						reset ();
						if (Program.quizzId < 9)
							Program.quizzId ++;
						fTimer = 0f;
					}
				}


			}

		}
		// Use this notif game infomation
		if(Program.isPause == true){

//			GUI.Label (new Rect (Screen.width * 0.28f, 
//			                     Screen.height * 0.2f,
//			                     50, 
//			                     50), 
//			           "Bạn hãy hoàn thành những câu đố vui sau\n\nđể nhận điểm IQ và tích luỹ xu cho mình.\n\n\t\t\t\tChúc bạn may mắn OOO", 
//			           styleNotif);
//			Debug.Log("Runs");
		}
	}

	private bool SuccessAnimation(){
		return(isSuccessAnimation = true);
	}

	void ResultNotif(){
		isRight = true;
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
				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}		
		} else if (length == 10 || length == 9) {
			for (int i = 1; i < length + 1; i++) {
				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 1;
		} else if(length == 8 || length == 7){
			for (int i = 2; i < length + 2; i++) {
				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 2;
		}else if(length == 6 || length == 5){
			for (int i = 3; i < length + 3; i++) {
				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 3;
		}else if(length == 4 || length == 3){
			for (int i = 4; i < length + 4; i++) {
				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}	
			iIndex = 4;
		}else{
			for (int i = 5; i < length + 5; i++) {
				iTween.MoveTo (quizzObjectList [i], iTween.Hash ("z", -1));
			}
			iIndex = 5;
		}
	}

	private void reset(){

		checkList = new List<int> ();
		AnswerInQuiz = new List<int> ();
		positionQuiz = new List<int> ();
		for(int i = 0; i < Program.quizzList[Program.quizzId].Answer_en.Length; i++){
			checkList.Add(0);
			positionQuiz.Add(-1);

		}
		
		for(int i = 0; i < answerObjectList.Length; i++){
			AnswerInQuiz.Add(-1);
			iTween.MoveTo(answerObjectList[i], iTween.Hash("x", ObjectPositionList [i].X,
			                                               "y", ObjectPositionList [i].Y,
			                                               "z", ObjectPositionList [i].Z));
		}
		for(int j = answerObjectList.Length; j < iLength; j++){
			iTween.MoveTo(quizzObjectList[j - 14], iTween.Hash("x", ObjectPositionList [j].X,
			                                               "y", ObjectPositionList [j].Y,
			                                               "z", 1));
		}

		count = 0;
		answerString = "";
		ShowEmptyAnswer (Program.quizzId + 1);
	}

	#region BUTTON FUNCTION
	public void onPlayButtonFunction(){
//		if(Program.isPause == false){
			Program.isClicked = true;
			
			// Use this to turn on play sound
			audio.PlayOneShot (playSound);
			
			for(int i = 0; i < answerObjectList.Length; i++){
				iTween.MoveTo(answerObjectList[i], iTween.Hash("x", ObjectPositionList [i].X,
				                                               "y", ObjectPositionList [i].Y,
				                                               "z", ObjectPositionList [i].Z,
				                                               "oncomplete", "SuccessAnimation",
				                                               "oncompletetarget", gameObject));
				iTween.MoveTo(mainObjectList[8], iTween.Hash("z", -1));
			}
			
			iTween.MoveTo(mainObjectList[0],iTween.Hash("z", 1));
			
			// Use this move question board object
			iTween.MoveTo(mainObjectList[5],iTween.Hash("y", .7,
			                                            "speed", 5));
			// Use this move go label object
			iTween.MoveTo(mainObjectList[1],iTween.Hash("z", -1));
			iTween.MoveTo(mainObjectList[1],iTween.Hash("position",transform.position += Vector3.right*22,
			                                            "easetype",iTween.EaseType.easeInOutSine,
			                                            "time", 1,
			                                            "delay", 0.01));
			ShowEmptyAnswer (Program.quizzId);
//		}
	}
	#endregion
}
