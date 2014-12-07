using UnityEngine;
using System.Collections;

public class ProgressTimeBar : MonoBehaviour {

	public UISprite timeProgressBarSprite;
	public static float timeProgress = 0;

	void OnGUI(){

		if (Program.isClicked == true && timeProgress < Program.levelAllList[Program.gameLevel - 1].GameTime - 1) {
			timeProgressBarSprite.fillAmount = 1 - timeProgress/Program.levelAllList[Program.gameLevel - 1].GameTime;
		}

	}

	void Update(){
		if(Program.isClicked == true && Program.isStop == false && !Program.isPause){
			if(timeProgress < Program.levelAllList[Program.gameLevel - 1].GameTime - 1)
				timeProgress = Time.timeSinceLevelLoad;
			else
				timeProgressBarSprite.fillAmount = 0;
		}
		if(timeProgressBarSprite.fillAmount == 0 && Program.isStop == false){
			timeProgressBarSprite.fillAmount = 1; 
			Program.isStop = true;
		}

	}
}