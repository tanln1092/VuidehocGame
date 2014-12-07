using UnityEngine;
using System.Collections;

public class cl_ButtonLevel6Click : MonoBehaviour {

	void OnClick () {
		string name = gameObject.name;

		switch (name) {
		case "play_button":		
			cl_Level6.Instance6.onPlayButtonFunction ();
			break;
		case "answer_1_button":		
			cl_Level6.Instance6.onAnswerFirstButtonFunction ();
			break;
		case "answer_2_button":		
			cl_Level6.Instance6.onAnswerSecondButtonFunction ();
			break;
		case "next_button":		
			Program.isResultNotif = false;
			Program.isClicked = false;
			
			Program.isStop = false;
			Program.gameLevel++;

			QuestionLabel.questionIndex = 0;
			Program.maxQuest = 0;
			Program.score = 0;
			ProgressTimeBar.timeProgress = 0;
			Application.LoadLevel("Level 7");

			break;
		case "play_again_button":	
			Program.isResultNotif = false;
			Program.isClicked = false;
			
			Program.isStop = false;
			//Program.isReset = true;
			QuestionLabel.questionIndex = 0;
			Program.maxQuest = 0;
			Program.score = 0;
			ProgressTimeBar.timeProgress = 0;
			Application.LoadLevel(Application.loadedLevel);
			break;
		case "menu_button":	
			Program.isResultNotif = false;
			Program.isClicked = false;
			
			Program.isStop = false;
			//Program.isReset = true;
			QuestionLabel.questionIndex = 0;
			Program.maxQuest = 0;
			Program.score = 0;
			ProgressTimeBar.timeProgress = 0;
			Application.LoadLevel("Map Game");
			break;
		case "infomation_button":	
			cl_Level6.Instance6.onInfomationButtonFunction ();
			break;
		case "close_button":
			Application.Quit();
			break;
		}
	}
}
