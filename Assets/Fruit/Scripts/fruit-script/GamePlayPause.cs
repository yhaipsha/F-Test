using UnityEngine;
using System.Collections;

public class GamePlayPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject gamePlay;
	void OnClick()
	{
		// 重置 (中途退出)
		PlayerPrefs.SetInt("PanelGamePlay",0);
		UIItemStorageTest ut = gamePlay.GetComponent<UIItemStorageTest> ();
		ut.cleaner();
		
	}
}
