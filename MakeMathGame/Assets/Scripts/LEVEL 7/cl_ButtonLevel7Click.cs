using UnityEngine;
using System.Collections;

public class cl_ButtonLevel7Click : MonoBehaviour {

	void OnClick () {
		string name = gameObject.name;
		
		switch (name) {
		case "play_button":		
			cl_Level7.Instance7.onPlayButtonFunction ();
			break;
		case "next_button":		
			Program.isResultNotif = false;
			Program.isClicked = false;
			
			Program.isStop = false;
			cl_Level7.isRight = false;
			Program.gameLevel++;

			QuestionLabel.questionIndex = 0;
			Program.maxQuest = 0;
			Program.score = 0;
			ProgressTimeBar.timeProgress = 0;
			Application.LoadLevel("Level " + Program.gameLevel);
			
			break;
		case "play_again_button":	
			Program.isResultNotif = false;
			Program.isClicked = false;
			
			Program.isStop = false;
			cl_Level7.isRight = false;

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
			cl_Level7.isRight = false;

			QuestionLabel.questionIndex = 0;
			Program.maxQuest = 0;
			Program.score = 0;
			ProgressTimeBar.timeProgress = 0;
			Application.LoadLevel("Map Game");
			break;
		case "infomation_button":	
			cl_Level7.Instance7.onInfomationButtonFunction ();
			break;
		case "close_button":
			Application.Quit();
			break;
		}
}
}