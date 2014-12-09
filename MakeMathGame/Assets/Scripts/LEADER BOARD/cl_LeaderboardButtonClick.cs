using UnityEngine;
using System.Collections;

public class cl_LeaderboardButtonClick : MonoBehaviour {

	void OnClick () {
		string name = gameObject.name;
		
		switch (name) {
		case "refresh_button":		
			cl_LeaderBoard.leaderBoardInstance.getAllUsers ();
			break;
		case "close_button":
			string levelName = cl_AppManager.levelNameCachedArray[cl_AppManager.levelNameCachedArray.Count - 1];
			cl_AppManager.levelNameCachedArray.RemoveAt(cl_AppManager.levelNameCachedArray.Count - 1);
			Application.LoadLevel(levelName);
			break;
		}
	}
}
