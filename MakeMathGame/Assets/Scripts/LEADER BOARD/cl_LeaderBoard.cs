using UnityEngine;
using System.Collections;

public class cl_LeaderBoard : MonoBehaviour {

	public UILabel[] nameArrayLabel;
	public UILabel[] scoreArrayLabel;

	public UILabel 	postionLabel;
	public UILabel nameLabel;
	public UILabel scoreLabel;

	public GameObject refreshButtonObject;

	int pageSize = 10;
	int pageIndex = 0;

	public static cl_LeaderBoard leaderBoardInstance;
	void Awake()
	{
		leaderBoardInstance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (cl_DataManager.getAllUser());
	}
	
	// Update is called once per frame
	void Update () {
		if (cl_DataManager.userList.Count != 0) {
			for(int i = 0; i < cl_DataManager.userList.Count; i++){
				int id = cl_DataManager.userList[i].Id;

				if((id == cl_DataManager.user.Id)){
					if((i + 1) > 10){
						postionLabel.text = "10+";

					}else{
						postionLabel.text = (i + 1) + "";
					}
					nameLabel.text = cl_DataManager.userList[i].Username;
					scoreLabel.text = cl_DataManager.userList[i].IqScore + "";

				}
			}
			for(int i = pageSize*pageIndex; i < pageSize*(pageIndex + 1); i++){
				nameArrayLabel[i].text = cl_DataManager.userList[i].Username;
				scoreArrayLabel[i].text = cl_DataManager.userList[i].IqScore + "";

			}		
		}
	}

	public void getAllUsers(){
		iTween.RotateBy (refreshButtonObject, iTween.Hash (
		                                                   "x", 2,
		                                                 "transition", "easeInOutBack"));
		StartCoroutine (cl_DataManager.getAllUser());

	}
}
