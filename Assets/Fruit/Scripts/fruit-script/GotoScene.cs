using UnityEngine;
using System.Collections;

public class GotoScene : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnClick ()
	{
		string sceneName = transform.name.Substring (transform.name.IndexOf ('-'));
		print (sceneName);
		switch (sceneName) {
		case "-Shop":
			Application.LoadLevel ("Shop");
			break;
			
		case "-Home":
			if (transform.parent.name == "Panel - Level") {
//				this.SendMessageUpwards ("cleanLevels");				
			}
			Application.LoadLevel ("FruitMain phone");
			break;
			
		case "-Help":
//			this.SendMessageUpwards ("cleanLevels");
			Application.LoadLevel ("Help");
			break;
			
		case "-Set":
//			this.SendMessageUpwards ("cleanLevels");
			Application.LoadLevel ("Set");
			break;
		
		case "-Level":
//			this.SendMessageUpwards ("cleanLevels");
			Application.LoadLevel ("Level");
			break;
		}

	}
}
