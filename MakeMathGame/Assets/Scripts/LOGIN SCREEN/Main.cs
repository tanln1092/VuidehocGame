using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	public static Main Instance;

	// Use this create object list need use
	public GameObject[] objList;

	// Use this save speaking
	List<string> speakingString; 

	// Set style for text label
	public GUIStyle questionLabel;

	public UILabel notifContentLabel;
	public UILabel notifResisterContentLabel;

	public UILabel loginTitleLabel;
	public UILabel usernameLabel;
	public UILabel passwordLabel;

	int index = 0;
	float closePositionY;

	bool isSuccessAnimation;
	bool isPaused;
	bool isLogin;

	private string usernameTextField = "";
	private string passwordTextField = "";


	public AudioClip backgroundSound;
	public AudioClip nextButtonClickedSound;

	// Use this for initialization
	void Start () {
		Instance = this;
		 
		closePositionY = objList [3].transform.position.y;

		speakingString = new List<string>();
		speakingString.Add ("\tChao ban !");
		speakingString.Add ("Hay chung to\nkha nang von co cua ban");
		speakingString.Add ("Để hoàn thành những màn chơi \nmột các tuyệt vời nhất!");
		speakingString.Add ("Bạn đã chuẩn bị \nsẵn sàng rồi chứ? ");
		speakingString.Add ("Nào hãy cho mình thấy \nkhả năng của bạn");
		speakingString.Add ("Bắt đầu nào...");
		speakingString.Add ("");

		// Use this create play button animation
		iTween.MoveTo(objList[0],iTween.Hash("x", -2.2,
		                                     "y", -0.5));
		
		// Use this create play button animation
		iTween.MoveTo(objList[1],iTween.Hash("x", -2.2,
		                                     "y", -0.35));

		// Use this create menu login animation
		iTween.MoveTo(objList[2],iTween.Hash("y", 0.095,
		                                     "oncomplete", "Success",
		                                     "oncompletetarget", gameObject));

	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time % 7.8f > 7.7f)
			audio.PlayOneShot(backgroundSound) ;

	}

	private bool Success(){
		return (isSuccessAnimation = true);
	}
	void OnCollisionEnter(Collision collision){
		/*
		audio.clip = backgroundSound;
		audio.loop = true;
		audio.Play ();
		*/
	}

	public void onLoginButtonClick (){
		if(!isPaused){
			if (usernameTextField.Trim ().Length == 0
			    || passwordTextField.Trim ().Length == 0) {
				isPaused = true;
				notifContentLabel.text = "Vui lòng điền đầy đủ\nthông tin";
				iTween.MoveTo(objList[3], iTween.Hash(
					"y", 0.4
					));
			}else{
				StartCoroutine(cl_DataManager.loginGame(usernameTextField, passwordTextField));
				isLogin = true;
			}
		}

	}

	public void onResisterButtonClick (){
		Debug.Log ("run");
		StartCoroutine(cl_DataManager.registerUser(usernameTextField, passwordTextField));
		isLogin = true;
	}
	public void onCloseNotifMenuButtonClick (){
		iTween.MoveTo(objList[3], iTween.Hash(
			"y", closePositionY
			));
		isPaused = false;
		isLogin = false;
	}

	public void onCloseResgisterMenuButtonClick (){
		iTween.MoveTo(objList[4], iTween.Hash(
			"y", closePositionY
			));
		isPaused = false;
		isLogin = false;
	}

	void OnGUI(){
		if(isSuccessAnimation == true && !isPaused){
			loginTitleLabel.text = "ĐĂNG NHẬP";
			usernameLabel.text = "Tài khoản";
			passwordLabel.text = "Mật khẩu";

			usernameTextField = GUI.TextField (new Rect(Screen.width*0.485f, Screen.height*0.342f, Screen.width*0.151f, Screen.height*0.0556f), usernameTextField, 25);
			passwordTextField = GUI.PasswordField (new Rect(Screen.width*0.485f, Screen.height*0.432f, Screen.width*0.151f, Screen.height*0.0556f), passwordTextField, '*', 25);
		}
		if (isPaused) {
			loginTitleLabel.text = "";
			usernameLabel.text = "";
			passwordLabel.text = "";
		}


	}

	public void loginSuccess(){
		Application.LoadLevel("Map Game");
	}

	public void loginFauld(){
		isPaused = true;
		notifResisterContentLabel.text = "Tài khoản " + usernameTextField +  "\nkhông tồn tại!\nBạn có muốn đăng ký\ntài khoản mới này?";
		iTween.MoveTo(objList[4], iTween.Hash(
			"y", 0.6
			));
	}
}
