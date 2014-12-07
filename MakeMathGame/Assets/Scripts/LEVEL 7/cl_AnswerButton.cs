using UnityEngine;
using System.Collections;

public class cl_AnswerButton : MonoBehaviour {

	void OnClick () {
		string name = gameObject.name;
		if(name[8].ToString().Equals("_"))
			cl_Level7.Instance7.onAnswerButtonFunction (int.Parse(name[7].ToString()) - 1);
		else{
			string number = name[7].ToString() + name[8].ToString();
			cl_Level7.Instance7.onAnswerButtonFunction (int.Parse(number) - 1);

		}

	}
}
