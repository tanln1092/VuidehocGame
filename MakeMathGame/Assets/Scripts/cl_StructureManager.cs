using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// For SQLite
//using System.Data;
//using Mono.Data.SqliteClient;

public class cl_StructureManager : MonoBehaviour {

	public static string DBName = "MakeMathGame.db";
//	public static IDbConnection dbcon;
//
//	public static bool Connection(){
//		try{
//			if(dbcon != null){
//				dbcon.Close();
//			}
//			string connectionString = "URI=file:" + DBName;
//
//			dbcon = (IDbConnection) new SqliteConnection(connectionString);
//			dbcon.Open();
//
//			return true;
//		}catch(DataException e){
//			return false;
//		}
//	}
//
//	public static void CloseConnection(){
//		dbcon.Close();
//		dbcon = null;
//	}
//
//	/*------------------SELECT----------------------*/
//	public static void GetUserInf (int Id) {
//
//		// Use this connect db
//		Connection ();
//
//		IDbCommand dbcmd = dbcon.CreateCommand();
//		
//		string sql = "SELECT * " +
//					" FROM user WHERE Id = " + Id;
//
//		dbcmd.CommandText = sql;
//		IDataReader reader = dbcmd.ExecuteReader();
//		
//		while(reader.Read()) {
//			int id = int.Parse(reader.GetString(0));
//			string username = reader.GetString(1);
//			string password = reader.GetString(2);
//			int iqScore = int.Parse(reader.GetString(3));
//			int moneyNumber = int.Parse(reader.GetString(4));
//
//			Program.IQTotal = iqScore;
//			Program.MoneyTotal = moneyNumber;
//			Program.userId = id;
//		}
//
//		// Use this clean up                                                                                                                                 
//		reader.Close();
//		reader = null;
//		dbcmd.Dispose();
//		dbcmd = null;
//
//		// Use this close connect
//		CloseConnection ();
//
//	}
//
//	public static void GetGameLevel (int Id) {
//		// Use this connect db
//		Connection ();
//		
//		IDbCommand dbcmd = dbcon.CreateCommand();
//		
//		string sql = "SELECT * FROM user INNER JOIN level ON user.Id  = level .UserId AND user.Id = " + Id;
//		
//		dbcmd.CommandText = sql;
//		
//		// Init array
//		Program.levelList = new List<cl_User>();
//		
//		IDataReader reader = dbcmd.ExecuteReader();
//		while(reader.Read()) {
//			int id = int.Parse(reader.GetString(0));
//			string username = reader.GetString(1);
//			string password = reader.GetString(2);
//			int iqScore = int.Parse(reader.GetString(3));
//			int moneyNumber = int.Parse(reader.GetString(4));
//
//			int gameTime = int.Parse(reader.GetString(5));
//			int levelId = int.Parse(reader.GetString(6));
//			string levelName = reader.GetString(7);
//			int unclock = int.Parse(reader.GetString(9));
//			float time = float.Parse(reader.GetString(10));
//			int starNumber = int.Parse(reader.GetString(11));
//			
//			cl_User user = new cl_User(id, username, password, iqScore, moneyNumber,
//			                           levelId, levelName, unclock, time, starNumber, gameTime);
//			
//			Program.levelList.Add(user);
////			Debug.Log("id: " + id +
////			          "username: " + username +
////			          "pass: " + password +
////			          "iq: " + iqScore +
////			          "money: " + moneyNumber +
////			          "levelId: " + levelId +
////			          "levelName: " + levelName +
////			          "unclock: " + unclock +
////			          "time: " + time +
////			          "starNumber: " + starNumber );
//		}
//		// Use this clean up                                                                                                                                 
//		reader.Close();
//		reader = null;
//		dbcmd.Dispose();
//		dbcmd = null;
//		
//		// Use this close connect
//		CloseConnection ();
//		
//	}
//
//	public static void GetQuizz () {
//		
//		// Use this connect db
//		Connection ();
//		
//		IDbCommand dbcmd = dbcon.CreateCommand();
//		
//		string sql = "SELECT * FROM quizz ORDER BY RANDOM() LIMIT 10";
//		
//		dbcmd.CommandText = sql;
//		IDataReader reader = dbcmd.ExecuteReader();
//
//		Program.quizzList = new List<cl_Quizz> ();
//		while(reader.Read()) {
//			int id = int.Parse(reader.GetString(1));
//			string quizzTitle = reader.GetString(2);
//			string quizzContent = reader.GetString(3);
//			string answer_vi = reader.GetString(4).ToUpper().Replace(" ", "");
//			string answer_en = reader.GetString(0);
//
//			Debug.Log("answer: " + answer_vi);
//			cl_Quizz quizz = new cl_Quizz(id, answer_en, answer_vi, quizzTitle, quizzContent, GetAnswerRandom(answer_en));
//			Program.quizzList.Add(quizz);
//		}
//		
//		// Use this clean up                                                                                                                                 
//		reader.Close();
//		reader = null;
//		dbcmd.Dispose();
//		dbcmd = null;
//		
//		// Use this close connect
//		CloseConnection ();
//		
//	}
//
//	/*------------------INSERT----------------------*/
//
//
//	/*------------------UPDATE----------------------*/
//	public static void SaveGame (int Id, int iqScore, int moneyNumber,
//	                             int levelId, int unclock, float time, int starNumber) {
//		
//		// Use this connect db
//		Connection ();
//		
//		IDbCommand dbcmd = dbcon.CreateCommand();
//
//		string sql = "UPDATE user SET MoneyNumber = " + moneyNumber + 
//								" , Score = " + iqScore + 
//					 " WHERE Id = " + Id;
//		
//		dbcmd.CommandText = sql;
//		IDataReader reader = dbcmd.ExecuteReader();
//
//		reader.Close();
//		reader = null;
//		dbcmd.Dispose();
//		dbcmd = null;
//
//		IDbCommand dbcmd1 = dbcon.CreateCommand();
//		string sql1 = "UPDATE level SET Unclock = " + unclock + 
//										" , PlayedTime = " + time + 
//										" , StarNumber = " + starNumber + 
//					 " WHERE UserId = " + Id + " AND LevelId = " + levelId;
//		
//		dbcmd1.CommandText = sql1;
//		IDataReader reader1 = dbcmd1.ExecuteReader();
//		// Use this clean up                                                                                                                                 
//		reader1.Close();
//		reader1 = null;
//		dbcmd1.Dispose();
//		dbcmd1 = null;
//		
//		// Use this close connect
//		CloseConnection ();
//		
//	}
//		public static void UnclockLevel (int Id, int LevelId) {
//		
//		// Use this connect db
//		Connection ();
//		
//		IDbCommand dbcmd = dbcon.CreateCommand();
//		
//		string sql = "UPDATE level SET Unclock = 1" + 
//			" WHERE UserId = " + Id + " AND LevelId = " + LevelId;
//		
//		dbcmd.CommandText = sql;
//		IDataReader reader = dbcmd.ExecuteReader();
//		
//		reader.Close();
//		reader = null;
//		dbcmd.Dispose();
//		dbcmd = null;
//			
//		// Use this close connect
//		CloseConnection ();
//		
//	}
//	/*------------------CHECK INFOMATION----------------------*/
//	public static bool CheckLevelId (int levelId) {
//
//		bool isExist = false;
//		// Use this connect db
//		Connection ();
//		
//		IDbCommand dbcmd = dbcon.CreateCommand();
//		
//		string sql = "SELECT COUNT(*) " +
//			" FROM level WHERE LevelId = " + levelId;
//		
//		dbcmd.CommandText = sql;
//		IDataReader reader = dbcmd.ExecuteReader();
//		while (reader.Read()) {
//			int exist = int.Parse(reader.GetString (0));
//			if(exist == 1) 
//				isExist = true;
//			else 
//				isExist = false;
//		}
//
//		// Use this clean up                                                                                                                                 
//		reader.Close();
//		reader = null;
//		dbcmd.Dispose();
//		dbcmd = null;
//		
//		// Use this close connect
//		CloseConnection ();
//
//		return isExist;
//	}


	public class cl_User{
		int id;
		string username;
		string password;
		int iqScore;
		int moneyNumber;
		int levelId;
		string levelName;
		int unclock;
		float time;
		int starNumber;
		int gameTime;

		public cl_User(){
		}

		public cl_User(int id, string username,
		               string password, int iqScore, int moneyNumber){
			this.id = id;
			this.username = username;
			this.password = password;
			this.iqScore = iqScore;
			this.moneyNumber = moneyNumber;
		}

		public cl_User(int id, string username,
		               string password, int iqScore, int moneyNumber,
		               int levelId, string levelName, int unclock, float time, int starNumber, int gameTime){
			this.id = id;
			this.username = username;
			this.password = password;
			this.iqScore = iqScore;
			this.moneyNumber = moneyNumber;
			this.levelId = levelId;
			this.levelName = levelName;
			this.unclock = unclock;
			this.time = time;
			this.starNumber = starNumber;
			this.gameTime = gameTime;
		}

		public cl_User(int id,
		               int levelId, string levelName, float time, int starNumber, int gameTime){
			this.id = id;
			this.username = username;
			this.password = password;
			this.iqScore = iqScore;
			this.moneyNumber = moneyNumber;
			this.levelId = levelId;
			this.levelName = levelName;
			this.unclock = unclock;
			this.time = time;
			this.starNumber = starNumber;
			this.gameTime = gameTime;
		}

		public int Id {
			get {
				return id;
			}
		}

		public string Username {
			get {
				return username;
			}
		}

		public string Password {
			get {
				return password;
			}
		}

		public int IqScore {
			get {
				return iqScore;
			}
		}

		public int MoneyNumber {
			get {
				return moneyNumber;
			}
		}

		public int LevelId {
			get {
				return levelId;
			}
		}

		public string LevelName {
			get {
				return levelName;
			}
		}

		public int Unclock {
			get {
				return unclock;
			}
		}

		public float Time {
			get {
				return time;
			}
		}

		public int StarNumber {
			get {
				return starNumber;
			}
		}

		public int GameTime {
			get {
				return gameTime;
			}
		}
	}


	public class cl_Quizz{

		int id;
		string answer_en;
		string answer_vi;
		string quizzTitle;
		string quizzContent;
		string answerRandom;

		public cl_Quizz(int id,
		                string answer_en,
		                string answer_vi,
		                string quizzTitle,
		                string quizzContent,
		                string answerRandom){
			this.id = id;
			this.answer_en = answer_en;
			this.answer_vi = answer_vi;
			this.quizzTitle = quizzTitle;
			this.quizzContent = quizzContent;
			this.answerRandom = answerRandom;
		}

		public int Id {
			get {
				return id;
			}
		}

		public string Answer_en {
			get {
				return answer_en;
			}
		}

		public string Answer_vi {
			get {
				return answer_vi;
			}
		}

		public string QuizzTitle {
			get {
				return quizzTitle;
			}
		}

		public string QuizzContent {
			get {
				return quizzContent;
			}
		}

		public string AnswerRandom {
			get {
				return answerRandom;
			}
		}
	} 

	public class cl_Level{

		int levelId;
		string levelName;
		int gameTime;
		
		public cl_Level(){
		}

		public cl_Level(int levelId, string levelName, int gameTime){
		
			this.levelName = levelName;
			this.levelId = levelId;
			this.gameTime = gameTime;
		}
		
		public int LevelId {
			get {
				return levelId;
			}
		}
		
		public string LevelName {
			get {
				return levelName;
			}
		}

		public int GameTime {
			get {
				return gameTime;
			}
		}
	}

	public static string GetAnswerRandom(string input){
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string random = "";
		List<int> current = new List<int> ();
		int index = 0;
		bool isCheck = false;
		int r = 0;
		int lenght = input.Length;
		do{
			r = Random.Range(0, chars.Length);
			input = string.Concat(input, chars[r]);
			index ++;
		}while(index < (14 - lenght));
		
		for (int i = 0; i < input.Length; i++)
		{
			do
			{
				r = Random.Range(0, input.Length);
			}
			while (current.IndexOf(r) != -1);
			
			current.Add(r);
			random = string.Concat(random, input[r]);
		}
		return random;
	}
}
