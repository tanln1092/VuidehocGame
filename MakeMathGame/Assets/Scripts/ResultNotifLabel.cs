using UnityEngine;
using System.Collections;

public class ResultNotifLabel : MonoBehaviour {

	public UILabel notifTitleLabel;
	public UILabel timeNotifLabel;
	public UILabel correctNotifLabel;

	void OnGUI(){
		if (Program.isSuccess == true && Program.isStop == true) {
			if(Program.score > 7){
				notifTitleLabel.text = "Bạn thắng rồi";
			}else{
				notifTitleLabel.text = "Thử lại nào";

			}
			correctNotifLabel.text = Program.score + "/10 câu";
			timeNotifLabel.text = Program.playTime + " giây";


		}else{
			correctNotifLabel.text = "";
			timeNotifLabel.text = "";
			notifTitleLabel.text = "";
		}
	}

}
