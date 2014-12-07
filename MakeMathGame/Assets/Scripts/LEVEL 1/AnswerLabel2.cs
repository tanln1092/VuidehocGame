using UnityEngine;
using System.Collections;

public class AnswerLabel2 : MonoBehaviour {

	// Use this to save answer
	string answerString = null;

	// Set style for text label
	public Texture[] answerLabel;

	public UISprite plusSprite_1;
	public UISprite minusSprite_1;
	public UISprite multiSprite_1;
	public UISprite divideSprite_1;

	void Start(){
		plusSprite_1.fillAmount = 0;
		minusSprite_1.fillAmount = 0;
		multiSprite_1.fillAmount = 0;
		divideSprite_1.fillAmount = 0;
	}
	void OnGUI(){
		if (Program.isClicked == true && QuestionLabel.questionIndex < 10  && !Program.isPause) {

			if(AnswerLabel1.randomNumberList[QuestionLabel.questionIndex] != 0){
				answerString = QuestionLabel.calculatorList [QuestionLabel.questionIndex].getCorrectAnswer ();
			}else{
				answerString = QuestionLabel.calculatorList [QuestionLabel.questionIndex].getInCorrectAnswer ();
			}

			if(answerString.Equals("+")){
				GUI.DrawTexture (new Rect (gameObject.transform.position.x + (float)(Screen.width*0.6030341f), 
				                           gameObject.transform.position.y + (Screen.height*0.7815126f), 50, 50), answerLabel[0]);
//				plusSprite_1.fillAmount = 1;
//				minusSprite_1.fillAmount = 0;
//				multiSprite_1.fillAmount = 0;
//				divideSprite_1.fillAmount = 0;
			}else if(answerString.Equals("-")){
				GUI.DrawTexture (new Rect (gameObject.transform.position.x + (Screen.width*0.6030341f),
				                           gameObject.transform.position.y + Screen.height*0.7815126f, 50, 50), answerLabel[1]);
//				plusSprite_1.fillAmount = 0;
//				minusSprite_1.fillAmount = 1;
//				multiSprite_1.fillAmount = 0;
//				divideSprite_1.fillAmount = 0;
			}else if(answerString.Equals("*")){
				GUI.DrawTexture (new Rect (gameObject.transform.position.x + (Screen.width*0.6030341f),
				                           gameObject.transform.position.y + Screen.height*0.7815126f, 50, 50), answerLabel[2]);
//				plusSprite_1.fillAmount = 0;
//				minusSprite_1.fillAmount = 0;
//				multiSprite_1.fillAmount = 1;
//				divideSprite_1.fillAmount = 0;
			}else{
				GUI.DrawTexture (new Rect (gameObject.transform.position.x + Screen.width*0.6030341f,
				                           gameObject.transform.position.y + Screen.height*0.7815126f, 50, 50), answerLabel[3]);
//				plusSprite_1.fillAmount = 0;
//				minusSprite_1.fillAmount = 0;
//				multiSprite_1.fillAmount = 0;
//				divideSprite_1.fillAmount = 1;
			}
		
		}
	}
}
