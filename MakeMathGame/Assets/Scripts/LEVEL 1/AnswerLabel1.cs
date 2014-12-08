using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnswerLabel1 : MonoBehaviour {

	// Use this create number random array
	public static List<int> randomNumberList;

	// Set style for text label
	public Texture2D[] answerLabel;

//	public UISprite plusSprite_1;
//	public UISprite minusSprite_1;
//	public UISprite multiSprite_1;
//	public UISprite divideSprite_1;

	// Use this to save answer
	string answerString = null;

	// Use this for initialization
	void Start () {
		randomNumberList = new List<int> ();
		int count = 0;
		do{
			int chooseNumber = Random.Range(0, 2);
			randomNumberList.Add(chooseNumber);
			count ++;
		}while(count < 10);

//		plusSprite_1.fillAmount = 0;
//		minusSprite_1.fillAmount = 0;
//		multiSprite_1.fillAmount = 0;
//		divideSprite_1.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		if (Program.isClicked == true && QuestionLabel.questionIndex < 10 && Program.isResultNotif == false  && !Program.isPause) {

			if(randomNumberList[QuestionLabel.questionIndex] == 0){
				answerString = QuestionLabel.calculatorList [QuestionLabel.questionIndex].getCorrectAnswer ();
			}else{
				answerString = QuestionLabel.calculatorList [QuestionLabel.questionIndex].getInCorrectAnswer ();
			
			}
				if(answerString.Equals("+")){
					GUI.DrawTexture (new Rect (gameObject.transform.position.x + (float)(Screen.width*0.3261694f), 
					                           gameObject.transform.position.y + (Screen.height*0.7815126f), 50, 50), answerLabel[0]);
//					  plusSprite_1.fillAmount = 1;
//					  minusSprite_1.fillAmount = 0;
//					  multiSprite_1.fillAmount = 0;
//					  divideSprite_1.fillAmount = 0;
				}else if(answerString.Equals("-")){
//					plusSprite_1.fillAmount = 0;
//					minusSprite_1.fillAmount = 1;
//					multiSprite_1.fillAmount = 0;
//					divideSprite_1.fillAmount = 0;
					GUI.DrawTexture (new Rect (gameObject.transform.position.x + (Screen.width*0.3261694f),
				                          	   gameObject.transform.position.y + Screen.height*0.7815126f, 50, 50), answerLabel[1]);
				}else if(answerString.Equals("*")){
//					plusSprite_1.fillAmount = 0;
//					minusSprite_1.fillAmount = 0;
//					multiSprite_1.fillAmount = 1;
//					divideSprite_1.fillAmount = 0;
					GUI.DrawTexture (new Rect (gameObject.transform.position.x + (Screen.width*0.3261694f),
				                           	   gameObject.transform.position.y + Screen.height*0.7815126f, 50, 50), answerLabel[2]);
				}else{
//					plusSprite_1.fillAmount = 0;
//					minusSprite_1.fillAmount = 0;
//					multiSprite_1.fillAmount = 0;
//					divideSprite_1.fillAmount = 1;
					GUI.DrawTexture (new Rect (gameObject.transform.position.x + Screen.width*0.3261694f,
				                           	   gameObject.transform.position.y + Screen.height*0.7815126f, 50, 50), answerLabel[3]);
				}
		}
	}
}
