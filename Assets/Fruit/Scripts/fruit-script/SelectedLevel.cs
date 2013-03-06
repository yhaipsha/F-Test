using UnityEngine;
using System.Collections;

public class SelectedLevel : MonoBehaviour
{
	
	//初始化游戏页面 
	
	

	void OnClick ()
	{
		UILabel lblLevelName = transform.FindChild("LblTitle").GetComponent<UILabel> ();
//		UILabel lblLevelName = GetComponentInChildren<UILabel> ();
		PlayerPrefs.SetInt ("NowPlay", int.Parse (lblLevelName.text.Trim ()));
//		print (int.Parse(lblLevelName.text.Trim ()));
//		PlayerPrefs.Save();
		Application.LoadLevel("Game2");
	}
}
