using UnityEngine;
using System.Collections;

public class MoneyNumber_and_IQNumber : MonoBehaviour {

	// Set style for text label
	public UILabel candyNumberLabel;
	public UILabel ideaNumberLabel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		candyNumberLabel.text = Program.MoneyTotal + Program.score - Program.iHelp + "";
		ideaNumberLabel.text =  Program.IQTotal + Program.score - Program.iHelp + "";
	}
}
