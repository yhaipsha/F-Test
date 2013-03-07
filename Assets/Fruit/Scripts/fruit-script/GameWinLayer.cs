using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameWinLayer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		init (PlayerPrefs.GetInt ("result"));
	}
	
	public void init (int result)
	{
		switch (result) {
		case 0:
			showResultSprite (false, "p-failed");
			break;
		case 1:
			showResultSprite (true, "p-win");
			break;
		}
		
		//清除计数器记录内容
		Globe.errorCount = 3;
		Globe.sameSize.Clear ();
		

		
		/*
		GameObject[] panels = Globe.getPanelObject (transform
			, new string[]{"Panel - Main","Panel - Shop","Panel - Help","Panel - Level","Panel - GamePlay","Panel - GamePlay"});
		
		string[] prefabs = new string[]{"BtnHome","BtnShop","BtnHelp","BtnLevel","BtnReplay","BtnNext"};
		createButtons (prefabs.Take(3).ToArray(), transform.FindChild ("UpButtons")	, panels.Take(4).ToArray());
		createButtons (prefabs.Skip(3).Take(3).ToArray(), transform.FindChild ("DownButtons"), panels.Skip(3).Take(3).ToArray());
		*/
	}
	
	void showResultSprite (bool on, string spriteName)
	{
		transform.FindChild ("DownButtons").FindChild ("p2Btn-Next").gameObject.SetActive (on);
		UISprite sp = transform.FindChild ("Show").FindChild ("SlicedSprite").GetComponent<UISlicedSprite> ();
		sp.spriteName = spriteName;
		print (spriteName);
		sp.MakePixelPerfect ();
		
		string lastlevelName = Globe.jsonLableNames [PlayerPrefs.GetInt ("NowMode") - 1] + PlayerPrefs.GetInt ("NowPlay");
		print ("lastLevelName = " + lastlevelName);
		
		
		//保留最好的成绩星
		if (on) {
			foreach (KeyValuePair<string,int> entry in Globe.sameSize) {
				print (
                    string.Format ("key:{0}\nvalues:{1}\n",
                                  entry.Key,
                                  entry.Value)
                );
			}
			
			PlayerPrefs.SetInt (lastlevelName, Globe.errorCount == 0 ? 1 : Globe.errorCount);
			switch (PlayerPrefs.GetInt ("NowMode")) {
			case 1:
				break;
			case 2:
				break;
			}
			
		} else {
			int score = PlayerPrefs.GetInt (lastlevelName);
			PlayerPrefs.SetInt (lastlevelName, score > 0 ? score : 0);
			print ("The current level:"+PlayerPrefs.GetInt (lastlevelName));
		}

//		Globe.sameSize.Clear();
//		Globe.differentSize.Clear();

	}
	
	void createButtons (string[] prefabs, Transform transFather, GameObject[] panels)
	{
//		string[] prefabs = new string[]{"BtnHome","BtnShop","BtnHelp"};	
		
		GameObject[] gos = Globe.getPrefabButtons (prefabs);
		
		for (int i = 0; i < gos.Length; i++) {
			GameObject goh = (GameObject)Instantiate (gos [i]);
			goh.transform.parent = transFather;
			goh.transform.localScale = new Vector3 (1f, 1f, 0.0025f);
			goh.transform.localPosition = new Vector3 (0f, 0f, 0f);
			
			goh.name = "u" + i + prefabs [i];	//print (transFather.name);
			
			
			addUIButtonTween (goh, panels [i], gameObject);
			
////			if(transFather.name == "UpButtons")				
//				addJumpPanel(goh,panels [panels.Length - 1].transform);

		}
								
		UIGrid ug = transFather.gameObject.AddComponent<UIGrid> ();
		ug.arrangement = UIGrid.Arrangement.Horizontal;
		ug.cellWidth = 98;
		ug.cellHeight = 200;
		ug.sorted = true;
		ug.hideInactive = true;
				
//		实例化后添加脚本
//		addUIButtonTween (gos, panels, gameObject);
	}

	void addJumpPanel (GameObject go, Transform transLevelPanel)
	{
//		GamePauseAftermath gp = go.GetComponent<GamePauseAftermath> ();
//		if (gp != null) {
//			gp.transLevelPanel = transLevelPanel;//panels [panels.Length - 1].transform;
////				gp.resetLevel = true;
//			gp.resetPause = false;
//			gp.resetPlay = false;
//			gp.removeCard = true;
//		} 
//			else {
//				gp = go.AddComponent ("GamePauseAftermath") as GamePauseAftermath;
//				gp.transLevelPanel = transLevelPanel;
//			}

	}
//		添加跳转控制脚本
	void addUIButtonTween (GameObject obj3, GameObject targets, GameObject targetSelf)
	{
		//GameObject goHome,GameObject goShop,GameObject goLevel
		UIButtonTween bt = null;
			
		bt = obj3.GetComponents<UIButtonTween> () [0];// obj3 [i].AddComponent<UIButtonTween> ();//
		bt.tweenTarget = targetSelf;
		bt.includeChildren = true;
		bt.resetOnPlay = false;
		bt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		bt.disableWhenFinished = AnimationOrTween.DisableCondition.DoNotDisable;
		bt.trigger = AnimationOrTween.Trigger.OnClick;
		bt.playDirection = AnimationOrTween.Direction.Toggle;
			
		bt = obj3.GetComponents<UIButtonTween> () [1];//obj3 [i].AddComponent<UIButtonTween> ();//
		bt.tweenTarget = targets;
		bt.includeChildren = true;
		bt.resetOnPlay = false;
		bt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		bt.disableWhenFinished = AnimationOrTween.DisableCondition.DisableAfterReverse;
		bt.trigger = AnimationOrTween.Trigger.OnClick;
		bt.playDirection = AnimationOrTween.Direction.Forward;		
		
	}

}
