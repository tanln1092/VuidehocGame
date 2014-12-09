using UnityEngine;
using System.Collections;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class cl_DataManager : MonoBehaviour {

	public static string SERVER_URL 				= "http://vuidehoc.zz.mu/index.php/api/";

	public static string GET_USER_INFO_URL  		= "service/user/";
	public static string GET_GAME_LEVEL_URL 		= "service/user_level/";
	public static string GET_QUIZZ 					= "service/quizz/";
	public static string GET_GAME_LEVEL_ALL 		= "service/level";
	public static string LOGIN_URL			 		= "service/user_login/";
	public static string REGISTER_URL				= "service/user_register/";
	public static string WRITE_LOG_TRACKING_URL		= "service/log_tracking/";

	public static string TRACKING_FILE_PATH			= "/Resources/";
	public static string TRACKING_FILE_NAME			= "tracking_log_file.txt";

	public static bool 	 bRequestGetUserInfoSuccess;
	public static bool 	 bRequestSaveGameSuccess;
	public static bool 	 bRequestUnclockLevelSuccess;
	public static bool 	 bRequestGetQuizzSuccess;
	public static bool 	 bRequestWriteLogTrackingSuccess;

	public static int 	iRandomNumber = 1;

	public static List<cl_StructureManager.cl_User> userList;
	public static cl_StructureManager.cl_User user;

	#region MAIN UNITY MONO FUNCTION
	void Start()
	{
		//Load JSON data from a URL
		StartCoroutine(getGameLevelAll());
//		StartCoroutine(saveGame(1,1,1,1,1,1));
	}
	#endregion

	#region GET USER INFOMATION FROM SERVER 
	public static IEnumerator getUserInfo(int iPlayerId){
		string sWebUrl = SERVER_URL + GET_USER_INFO_URL + "?id=" + iPlayerId;
				WWW www = new WWW(sWebUrl);
				
				//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
				yield return www;
				if (www.error == null){
					//Sucessfully loaded the JSON string
//					Debug.Log("Loaded following JSON string" + www.data);
					
					//Process user found in JSON file
					JsonData jsonData = JsonMapper.ToObject(www.data);

					int iqScore = int.Parse(jsonData["score"].ToString());
					int moneyNumber = int.Parse(jsonData["coin"].ToString());
					int id = int.Parse(jsonData["id"].ToString());
					string username 	= jsonData["username"].ToString();
					string password 	= jsonData["password"].ToString();
					
					user = new cl_StructureManager.cl_User(id, username, password, iqScore, moneyNumber);

					Program.IQTotal = iqScore;
					Program.MoneyTotal = moneyNumber;
					Program.userId = id;
				}
				else{
					Debug.Log("ERROR: " + www.error);
				}


	}
	#endregion

	#region GET ALL USERS INFOMATION FROM SERVER 
	public static IEnumerator getAllUser(){

		List<cl_StructureManager.cl_User> userSortList = new List<cl_StructureManager.cl_User> ();
		userList = new List<cl_StructureManager.cl_User> ();

		string sWebUrl = SERVER_URL + GET_USER_INFO_URL;
		WWW www = new WWW(sWebUrl);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null){
			//Sucessfully loaded the JSON string
//			Debug.Log("Loaded following JSON string" + www.data);
			
			//Process user found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);

			for(int i = 0; i < jsonData.Count; i++){
				int id 				= int.Parse(jsonData[i]["id"].ToString());
				string username 	= jsonData[i]["username"].ToString();
				string password 	= jsonData[i]["password"].ToString();
				int iqScore 		= int.Parse(jsonData[i]["score"].ToString());
				int moneyNumber		= int.Parse(jsonData[i]["coin"].ToString());

				cl_StructureManager.cl_User user = new cl_StructureManager.cl_User(id, username, password, iqScore, moneyNumber);
				userSortList.Add(user);
			}


		}
		else{
			Debug.Log("ERROR: " + www.error);
		}

		if (www.isDone) {
			for(int i = 0; i < userSortList.Count - 1; i++)
			for(int j = i + 1; j < userSortList.Count; j++){
				if (userSortList[i].IqScore < userSortList[j].IqScore)
				{
					cl_StructureManager.cl_User temp = userSortList[i];
					userSortList[i] = userSortList[j];
					userSortList[j] = temp;
				}
			}
			userList = userSortList;
		}
		
	}
	#endregion

	#region LOGIN GAME FROM SERVER
	public static IEnumerator loginGame(string username, string password){
		string sWebUrl = SERVER_URL + LOGIN_URL + "?username=" + username + "&password=" + password;
		WWW www = new WWW(sWebUrl);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null){
			//Sucessfully loaded the JSON string

			//Process books found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);
			
			Program.userId = int.Parse(jsonData["id"].ToString());
//			Debug.Log("==>Login: user id: " + Program.userId);
			Main.Instance.loginSuccess();

		}
		else{
			Debug.Log("ERROR: " + www.error);
			Main.Instance.loginFauld();

		}
		
		
	}
	#endregion

	#region GET COMPLETED LEVEL FROM SERVER 
	public static IEnumerator getGameLevel(int iPlayerId){
		bRequestGetUserInfoSuccess = false;

		string sWebUrl = SERVER_URL + GET_GAME_LEVEL_URL + "?id=" + iPlayerId;
		WWW www = new WWW(sWebUrl);
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
			//Sucessfully loaded the JSON string
//								Debug.Log("Loaded following JSON string" + www.data);

			//Process books found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);
			Program.levelList = new List<cl_StructureManager.cl_User> ();

			for(int i = 0; i < jsonData.Count; i++){
				int levelId = int.Parse(jsonData[i]["id"].ToString());
				int id = int.Parse(jsonData[i]["user_id"].ToString());
				float time = float.Parse(jsonData[i]["play_time"].ToString());
				int starNumber = int.Parse(jsonData[i]["star"].ToString());
				string levelName = jsonData[i]["name"].ToString();
				int gameTime = int.Parse(jsonData[i]["game_time"].ToString());
//
//				Debug.Log("Loaded following JSON string:" + levelId);
//
				cl_StructureManager.cl_User user = new cl_StructureManager.cl_User(id,
				                           						levelId, levelName, time, starNumber, gameTime);

				Program.levelList.Add(user);
			}		

		}else{
			Debug.Log("ERROR: " + www.error);
		}
			bRequestGetUserInfoSuccess = true;
	}
	#endregion

	#region GET ALL LEVEL FROM SERVER 
	public static IEnumerator getGameLevelAll(){
		string sWebUrl = SERVER_URL + GET_GAME_LEVEL_ALL;
		WWW www = new WWW(sWebUrl);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
			//Sucessfully loaded the JSON string
//			Debug.Log("Loaded following JSON string" + www.data);
			
			//Process books found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);
			Program.levelAllList = new List<cl_StructureManager.cl_Level> ();

			for(int i = 0; i < jsonData.Count; i++){
				int levelId = int.Parse(jsonData[i]["id"].ToString());
				string levelName = jsonData[i]["name"].ToString();
				int gameTime = int.Parse(jsonData[i]["game_time"].ToString());
		
//								Debug.Log("Loaded following JSON string:" + levelId);

				cl_StructureManager.cl_Level user = new cl_StructureManager.cl_Level(levelId, levelName, gameTime);
				
				Program.levelAllList.Add(user);
			}		
			
		}else{
			Debug.Log("ERROR: " + www.error);
		}
	}
	#endregion

	#region GET QUIZZ FOLLOWING "DO VUI" CATEGORY IN GAME FROM SERVER 
	public static IEnumerator getQuizz(int iRandom){
		bRequestGetQuizzSuccess = false;

		string sWebUrl = SERVER_URL + GET_QUIZZ + "?random=" + iRandom;
		WWW www = new WWW(sWebUrl);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;

		if (www.error == null)
		{
			//Sucessfully loaded the JSON string
//											Debug.Log("Loaded following JSON string" + www.data);
			
			//Process books found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);
			Program.quizzList = new List<cl_StructureManager.cl_Quizz> ();

			for(int i = 0; i < jsonData.Count; i++){
				int id = int.Parse(jsonData[i]["quizz_id"].ToString());
				string quizzTitle = jsonData[i]["title"].ToString();
				string quizzContent = jsonData[i]["content"].ToString().Replace("\\", "\n");
				string answer_vi = jsonData[i]["answer_vi"].ToString().ToUpper().Replace(" ", "");
				string answer_en = jsonData[i]["answer_en"].ToString();
				
//				Debug.Log("answer: " + answer_en);
				cl_StructureManager.cl_Quizz quizz = new cl_StructureManager.cl_Quizz(id, answer_en, answer_vi, quizzTitle, quizzContent, cl_StructureManager.GetAnswerRandom(answer_en));
				Program.quizzList.Add(quizz);
			}		
			
		}else{
			Debug.Log("ERROR: " + www.error);
		}
		bRequestGetQuizzSuccess = true;
		
	}
	#endregion

	#region SAVE GAME AFTER USER COMPLETED A LEVEL 
	public static IEnumerator saveGame(int id, int score, int coin,
	                     int level_id, int time, int star){
		bRequestSaveGameSuccess = false;
		string sWebUrl = SERVER_URL + GET_GAME_LEVEL_URL;
									
		WWWForm wform = new WWWForm();
		wform.AddField ("id", id);
		wform.AddField ("level_id", level_id);
		wform.AddField ("score", score);
		wform.AddField ("coin", coin);
		wform.AddField ("time", time);
		wform.AddField ("star", star);

		WWW www = new WWW(sWebUrl, wform);
//		Debug.Log ("====>Save Game : " + level_id);
//		Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
			bRequestSaveGameSuccess = true;

			//Sucessfully loaded the JSON string
//			Debug.Log("Loaded following JSON string djfhdj:" + www.data);

		}else{
			Debug.Log("ERROR: " + www.error);
		}

	}
	#endregion

	#region REGISTER A NEW USER
	public static IEnumerator registerUser(string username, string password){
		string sWebUrl = SERVER_URL + REGISTER_URL;
		
		WWWForm wform = new WWWForm();
		wform.AddField ("username", username);
		wform.AddField ("password", password);
		
		WWW www = new WWW(sWebUrl, wform);
		//		Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
			//Process register found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);
			
			Program.userId = int.Parse(jsonData["id"].ToString());
//			Debug.Log("==>Register: user_id: " + jsonData["id"].ToString());
			Main.Instance.loginSuccess();

		}else{
			Debug.Log("ERROR: " + www.error);

		}

	}
	#endregion

	#region UNCLOCK NEW LEVEL
	public static IEnumerator unclockLevel(int id, int level_id){
		string sWebUrl = SERVER_URL + GET_GAME_LEVEL_URL;
		bRequestUnclockLevelSuccess = false;

		WWWForm wform = new WWWForm();
		wform.AddField ("id", id);
		wform.AddField ("level_id", level_id);

		WWW www = new WWW (sWebUrl, wform);
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
		
			bRequestUnclockLevelSuccess = true;

			//Sucessfully loaded the JSON string
//			Debug.Log("Unclocked success!: " + www.data);
			
		}else{
			Debug.Log("ERROR: " + www.error);
		}

	}
	#endregion

	#region READ CONTENT TRACKING LOG FILE FROM RESOURCE FILE IN ASSET
	public static List<string> readContentTrackingLogFile(){
		List<string> contentTextList = new List<string> ();
		TextAsset taData = (TextAsset)Resources.Load (TRACKING_FILE_NAME, typeof(TextAsset));
		contentTextList.Add (taData.text);
		return contentTextList;
	}
	#endregion

	#region WRITE CONTENT TRACKING LOG FILE FROM RESOURCE FILE IN ASSET
//	public static void writeContentTrackingLogFile(string contentTextList){
//		File.WriteAllText (Application.dataPath + TRACKING_FILE_PATH + TRACKING_FILE_NAME, contentTextList);
//	}
	#endregion

	#region WRITE LOG TRACKING
	public static IEnumerator writeLogTracking(int userId, int levelId, int quizId,
	                                           int playedTime, int gameTime, string status){
		string sWebUrl = SERVER_URL + WRITE_LOG_TRACKING_URL;
		bRequestWriteLogTrackingSuccess = false;

		WWWForm wform = new WWWForm();
		wform.AddField ("user_id", userId);
		wform.AddField ("level_id", levelId);
		wform.AddField ("quiz_id", quizId);
		wform.AddField ("played_time", playedTime);
		wform.AddField ("game_time", gameTime);
		wform.AddField ("status", status);

		WWW www = new WWW(sWebUrl, wform);
		//		Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
			bRequestWriteLogTrackingSuccess = true;

			//Process register found in JSON file
			JsonData jsonData = JsonMapper.ToObject(www.data);
			
//			Debug.Log("==>write log tracking: id: " + jsonData["id"].ToString());
			
		}else{
			Debug.Log("ERROR: " + www.error);
		}
		
	}
	#endregion
}
