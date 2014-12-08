using UnityEngine;
using System.Collections;

public class ProgressTimeBar : MonoBehaviour {

	public UISprite timeProgressBarSprite;
	public static float timeProgress = 0;
	float fAfterTimer;

	void OnGUI(){

		if (Program.isClicked == true && timeProgress < (Program.levelAllList[Program.gameLevel - 1].GameTime) - 0.001f) {
			timeProgressBarSprite.fillAmount = 1 - timeProgress/(Program.levelAllList[Program.gameLevel - 1].GameTime);
		}

	}

	void Update(){
		if (Program.isClicked == false) {
			fAfterTimer = Time.timeSinceLevelLoad;	
		}
		if(Program.isClicked == true && Program.isStop == false && !Program.isPause){

			if(timeProgress < (Program.levelAllList[Program.gameLevel - 1].GameTime) - 0.001f)
				timeProgress = Time.timeSinceLevelLoad - fAfterTimer;
			else
				timeProgressBarSprite.fillAmount = 0;
		}
		if(timeProgressBarSprite.fillAmount == 0 && Program.isStop == false){
			timeProgressBarSprite.fillAmount = 1; 
			Program.isStop = true;
		}

	}
}