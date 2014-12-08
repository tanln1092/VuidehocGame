using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionLabel : MonoBehaviour {

	public class cl_Questions{
		
		string questionString;
		string correctAnswer;
		string inCorrectAnswer;
		
		public cl_Questions(string quest, 
		                    	string correctAns, string inCorrectAns){
			questionString = quest;
			correctAnswer = correctAns;
			inCorrectAnswer = inCorrectAns;
		}
		
		public string getQuestionString(){
			return questionString;		
		}

		public string getCorrectAnswer(){
			return correctAnswer;		
		}
		
		public string getInCorrectAnswer(){
			return inCorrectAnswer;		
		}

	}

	// Use this for operation list initialization
	List<string> operationList;

	// Use this for operation list initialization
	public static List<cl_Questions> calculatorList;

	// Use this to random answer
	public static bool isAnswer = false;
	public static int questionIndex = 0;

	// Time for quest
	public float timer = 30;

	// Set style for text label
//	public GUIStyle questionLabel;

	// Set style for info text label
	public GUIStyle infoLabel;

	public UILabel questLabel;

	// Resolve operation conflict
	int conflictIndex = -1;
	float fAfterTimer;

	// Use this for initialization
	void Start () {
		calculatorList = new List<cl_Questions>();

		operationList = new List<string>();
		operationList.Add ("+");
		operationList.Add ("-");
		operationList.Add ("*");
		operationList.Add ("/");

		Operation ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Program.isClicked == false) {
			fAfterTimer = Time.timeSinceLevelLoad;	
		}
	}

	void OnGUI(){


		if (Program.isClicked == true && questionIndex < 10 && !Program.isPause) {
			// Use this count remain time
			if(Program.isStop == false){
				questLabel.text = calculatorList[questionIndex].getQuestionString();
				Program.playTime = Time.timeSinceLevelLoad - fAfterTimer; 

			}
			else{
				Program.playTime = Mathf.Round(Program.playTime * 100)/100f;
				questLabel.text = "";

			}

			// Use this load question

//			GUI.Button(new Rect(gameObject.transform.position.x + Screen.width * 0.3982301f, 
//			                    gameObject.transform.position.y + Screen.height * 0.1540616f,
//			                    50,
//			                    50),
//			           			calculatorList[questionIndex].getQuestionString(), questionLabel)	;
		}else
			questLabel.text = "";

		if(Program.isPause){
			questLabel.text = "";
		}
	}


	public void Operation(){
		int count = 0;
		do{
			int index = Random.Range(0, operationList.Count);
			int x, y;
			int kq = 0;

			do{
				x = Random.Range(0, 10);
				y = Random.Range(0, 10);
			}while(x == 0 && y == 0);

			if (operationList[index].Equals("+"))
			{
				kq = x + y;               
			}
			else if (operationList[index].Equals("-"))
			{
				if (x < y) {
					int cons = x;
					x = y;
					y = cons;
				}
				kq = x - y;
			}
			else if (operationList[index].Equals("*"))
			{
				
				kq = x * y;
			}
			else if (operationList[index].Equals("/"))
			{
				
				x = Random.Range(1, 10);
				y = Random.Range(1, 10);
				
				if (x < y)
				{
					int cons = x;
					x = y;
					y = cons;
				}
				
				if (x % y != 0) {
					for (int i = 1; i < 10; i++) {
						if (x % y == 0)
							break;
						else
							x ++;
					}
					
				}
				kq = x / y;

			}

			// Check number conflict
			if(x == 2 && y == 2){
				if(operationList[index].Equals("+")){
					conflictIndex = 2;
				}else if(operationList[index].Equals("*")){
					conflictIndex = 0;
				}
			}
			if(x == 4 && y == 2){
				if(operationList[index].Equals("-")){
					conflictIndex = 3;
				}else if(operationList[index].Equals("/")){
					conflictIndex = 1;
				}
			}
			if(y == 1){
				if(operationList[index].Equals("/")){
					conflictIndex = 2;
				}else if(operationList[index].Equals("*")){
					conflictIndex = 3;
				}
			}

			// Use this to random answer choise 
			int numberChoise = 0; 
		
			if(y == 0){
				if(operationList[index].Equals("/")){
					do{
						index = Random.Range(0, operationList.Count);
					}while(index == 3);
				}
				if(operationList[index].Equals("+")){
					conflictIndex = 1;
				}
				if(operationList[index].Equals("-")){
					conflictIndex = 0;
				}
			}
			do{
				numberChoise = Random.Range(0, operationList.Count);
			}while(numberChoise == index || numberChoise == conflictIndex);

			// Use this to write question infomation
			string operationString = x + " ... " + y + " = " + kq;
			string correctAnswer = operationList[index];
			string inCorrectAnswer = operationList[numberChoise];
			
			cl_Questions questions = new cl_Questions(operationString, correctAnswer, inCorrectAnswer);
			calculatorList.Add(questions);
			count ++;
		}while(count < 10);

	}
}
