using UnityEngine;
using System.Collections;

public class Answer1 : MonoBehaviour {

	public static bool label = false;
	public UILabel[] answerUILabel;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if(Program.isClicked == true && Program.isStop == false  && !Program.isPause){
			Program.playTime = Time.timeSinceLevelLoad; 
		}

		if (Program.isClicked == true && !cl_Level7.isRight && !Program.isPause){


			for(int i = 0; i < cl_Level7.AnswerInQuiz.Count; i++){
				answerUILabel[i].text = Program.quizzList[Program.quizzId].AnswerRandom[i].ToString ();
			}
		}
		if(cl_Level7.isRight) {
			for(int i = 0; i < Program.quizzList [Program.quizzId].Answer_vi.Length; i++){
				answerUILabel[cl_Level7.positionQuiz[i]].text = Program.quizzList[Program.quizzId].Answer_vi[i].ToString ();
			}
		}

		
	}
}

