using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public static bool isPause;

	public static bool isHelp;

	public static int iHelp;

	public static int quizzId = 0;

	public static bool isReset;
	// Use this count number question
	public static int maxQuest = 0;

	public static int userId;

	// Use this save score of player
	public static int score = 0;

	// Use this save remain time of player
	public static float playTime = 0;

	// Use this notification stop game
	public static bool isStop = false;

	// Use this play time total
	public static float timeTotal = 0;

	// Use this 
	public static int IQTotal;

	//
	public static int MoneyTotal;

	// Use this check game level
	public static int gameLevel;

	public static string levelName;


	// Use this check star number
	public static int starNumber;
	
	// Use this check user click play button
	public static bool isClicked = false;
	
	// Use this check notif menu
	public static bool isSuccess = false;
	
	// Use this check choose result
	public static bool isResultNotif = false;
	
	public static bool isRun = false;

	// Use this save the all data for map level
	public static List<cl_StructureManager.cl_User> levelList;

	public static List<cl_StructureManager.cl_Level> levelAllList;

	// Use this save the all quizz data for game
	public static List<cl_StructureManager.cl_Quizz> quizzList;

	public static List<Program.cl_ObjectPosition> ObjectPositionList;

	// Use this save object position
	public class cl_ObjectPosition{

		float x;
		float y;
		float z;

		public cl_ObjectPosition(float x, float y, float z){
			this.x = x;
			this.y = y;
			this.z = z;
		}


		public float Y {
			get {
				return y;
			}
		}

		public float X {
			get {
				return x;
			}
		}

		public float Z {
			get {
				return z;
			}
		}
	}
}
