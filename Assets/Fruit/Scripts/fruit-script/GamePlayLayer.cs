using UnityEngine;
using System.Collections.Generic;

public class GamePlayLayer : MonoBehaviour
{
	//定义委托，显示关卡和错误次数。	
	public delegate void showLevel (string levelname);
	public event showLevel EventShowLevel;

	private Transform goButtons = null;
	private int _nowPlay=1;
	private int _nowMode=1;
	// Use this for initialization
	void Start ()
	{
		//		Globe.sameSize = new System.Collections.Generic.Dictionary<string, int> ();
		//		createButtons ();
	}
//	void Update()
//	{
//		if (transform.gameObject.activeSelf) {
//			OnLayer();
//		}
//	}
	void Awake() {
		OnLayer();
	}
	public void cleanButtons ()
	{
		if (goButtons != null)
			for (int i = 0; i < goButtons.GetChildCount(); i++) {
				foreach (UIButtonTween item in goButtons.GetComponentsInChildren<UIButtonTween>()) {
					DestroyImmediate (item, true);
				}
				Destroy (goButtons.GetChild (i).gameObject, 1.0f);
			}
	}
	
	void OnLayer ()
	{		
		//-----------当前关卡
		_nowPlay = PlayerPrefs.GetInt ("NowPlay");
		_nowMode = PlayerPrefs.GetInt ("NowMode");
		print ("current Level is " + _nowPlay + "; current Mode is " + _nowMode);
		
//		Transform transFruit = transform.FindChild ("ExampleFruit");
		string time = string.Empty;
		switch (_nowMode) {
		case 1:
			Globe.errorCount=3;
			time = Globe.errorCount.ToString();			
			createAtlases(PlayerPrefs.GetString("first"+(_nowPlay-1)).Split(','));
//			createAtlases (Globe.askbox);						
			print ("current level is " + _nowPlay + " from 1 ,and findCount = " + Globe.findCount);			
			break;
		case 2:
			Globe.errorCount=1;
			time = Globe.errorCount.ToString();	//每一个关卡 允许错误次数
			createAtlases(PlayerPrefs.GetString("second"+(_nowPlay-1)).Split(','));
//			createAtlases (Globe.askbox2);
			break;
		case 3:
			time = "0:30";//30
			createAtlases(PlayerPrefs.GetString("third"+(_nowPlay-1)).Split(','));
//			createAtlases (Globe.askbox3);
			break;
			
		}
		initTitle(time);
		initStar ();		
		initGameWindow ();
//		createButtons ();
	}

	void initTitle (string time)
	{
		Transform transTitle = transform.FindChild ("PanelTitle");
	
		UILabel lbl = transTitle.FindChild ("LabelShow").GetComponent<UILabel> ();
		lbl.text = _nowMode + "-" + _nowPlay;			
		lbl = transTitle.FindChild ("LabelTime").GetComponent<UILabel> ();
		lbl.text = time;
		
		Transform transFruit = transTitle.FindChild ("ExampleFruit");
		if (_nowMode == 1) {
		UISlicedSprite ssp = transFruit.GetComponent<UISlicedSprite> ();
		ssp.enabled=false;
		ssp.spriteName = Globe.askatlases [Globe.askatlases.Count - 1];//only normal mode
		print ("look for sprite =" + ssp.spriteName);
					
		}else
		{
			transFruit.gameObject.SetActive (false);
		}
	}

	public GameObject go;

	void initStar ()
	{
		string tmp = Globe.Compare (_nowMode) + _nowPlay;
		if (go != null) {
			Transform transStar = transform.FindChild ("PanelStar");
			
			for (int i = 0; i < transStar.GetChildCount(); i++) {
				transStar.GetChild (i).gameObject.SetActive (false);
			}
			for (int i = 0; i < PlayerPrefs.GetInt (tmp); i++) {
				transStar.GetChild (i).gameObject.SetActive (true);
			}			
		}
	}
	

	public void initGameWindow ()
	{
		PlayerPrefs.DeleteKey ("cardReady");
		string[] names = Globe.cards.ToArray ();
		ArrayRandom.FillRandomArray (ref names);
		DecorateGamePlay (names);

		/*
        for (int i = 0; i < arrSp.Length; i++) {
            arrSp [i] = "box" + Globe.box [box1_1 [i]];		
			
            string boxfind = "boxfind" + Globe.box [box1_1 [i]];	
            if (Globe.sameSize.ContainsKey (boxfind)) {
                Globe.sameSize [boxfind]++;
            } else
                Globe.sameSize.Add (boxfind, 1);
        }
		
        foreach (KeyValuePair<string, int> kvp in Globe.sameSize) {
            print (kvp.Key + ":" + kvp.Value);
        }*/

	}
	
	void createAtlases (string[] item)
	{
//		print (item[0]+"?"+item[1]);
		//头图片个数  选取数组最后一个Globe.askbox
		int maxCard = item.Length;
		Globe.box = new ArrayRandom (maxCard).NonRepeatArray (1, 16);
		Globe.cards = new List<string> ();
		Globe.askatlases = new List<string> ();

		for (int j = 0; j < maxCard; j++) {
			int __count = int.Parse (item [j]);
			if (j == maxCard - 1) {
				Globe.findCount = __count;
			}
			string[] temp = new string[__count];
			for (int k = 0; k < temp.Length; k++) {
				temp [k] = "box" + Globe.box [j];
				//				print (temp [k]);
				Globe.cards.Add (temp [k]);
			}
			if (_nowMode == 1) 
				Globe.askatlases.Add ("boxfind" + Globe.box [j]);
		}
		print (Globe.askatlases.Count);
	}
	void createAtlases2 (List<string[]> item)
	{
		//头图片个数  选取数组最后一个Globe.askbox
		int maxCard = item [_nowPlay - 1].Length;
		Globe.box = new ArrayRandom (maxCard).NonRepeatArray (1, 16);
		Globe.cards = new List<string> ();
		Globe.askatlases = new List<string> ();

		for (int j = 0; j < maxCard; j++) {
			int __count = int.Parse (item [_nowPlay - 1] [j]);
			if (j == maxCard - 1) {
				Globe.findCount = __count;
			}
			string[] temp = new string[__count];
			for (int k = 0; k < temp.Length; k++) {
				temp [k] = "box" + Globe.box [j];
				//				print (temp [k]);
				Globe.cards.Add (temp [k]);
			}
			if (_nowMode == 1) 
				Globe.askatlases.Add ("boxfind" + Globe.box [j]);
		}
	}
	
	void createButtons ()
	{
		Transform transPause = transform.FindChild ("PanelPause");
		transPause.localPosition = new Vector3 (0f, -680f, -5f);
		goButtons = transPause.FindChild ("Buttons");
		GameObject[] panels = Globe.getPanelObject (transform, new string[] { "Panel - Main", "Panel - Shop", "Panel - Level" });
		
		//动态天加脚本
//		if (goButtons.GetChildCount () <= 0)
//			addButtons (transPause, goButtons, panels);

	}

	/// <summary>
	/// 布置游戏界面
	/// </summary>
	/// <param name='names'>
	/// Names.
	/// </param>
	void DecorateGamePlay (string[] names)
	{
		string str = string.Empty;
		foreach (string item in names) {
			str += item + ",";
		}
		if (_nowMode == 1) 
			print (str.Substring (0, str.Length - 1) + " findName = " + Globe.askatlases [Globe.askatlases.Count - 1]);
		
		Transform transGameWindow = transform.FindChild ("GameWindow");
		UIItemStorageTest ut = transGameWindow.GetComponent<UIItemStorageTest> ();
		
		//		Globe.cards.Count
		if (_nowPlay <= 4) {
			ut.maxColumns = 3;
			ut.maxRows = 4;
			ut.transform.localPosition = Globe.cardPanel1;
		} else if (_nowPlay > 4 && _nowPlay <= 12) {
			ut.maxColumns = 5;
			ut.maxRows = 4;
			ut.transform.localPosition = Globe.cardPanel2;
		} else if (_nowPlay > 12 && _nowPlay <= 19) {
			ut.maxColumns = 6;
			ut.maxRows = 4;
			ut.transform.localPosition = Globe.cardPanel3;
		} else if (_nowPlay > 19 && _nowPlay < 40) {
			ut.maxColumns = 7;
			ut.maxRows = 4;
			ut.spacing = 110;
			ut.padding = 10;
			ut.transform.localPosition = Globe.cardPanel3;
		}
		
		if (_nowMode == 2) {
			ut.childrenAutoReverse = true;
		}
		
		if (ut.transform.GetChildCount () == 0) {
			ut.createTemp (names);
		} else {
			ut.cleaner ();
			ut.createTemp (names);
		}
		
		PlayerPrefs.SetInt ("cardReady", 1);
		transGameWindow.GetComponent<TurnManager>().init(1);

		//		else if (PlayerPrefs.GetInt ("PanelGamePlay") == -1) {
		//			//中途退出
		//			print("exit from  ");
		//			//ut.cleaner();
		//			
		//			ut.maxRows = 4;
		//			ut.maxColumns = 3;
		//			
		//			ut.transform.localPosition = new Vector3 (-256f, 128f, -0.5f);
		//			ut.createTemp (names);
		//			PlayerPrefs.SetInt("PanelGamePlay",0);
		//			
		//		}


	}

	void addGrid (GameObject go, int cellWidth)
	{
		
		UIGrid ug = go.GetComponent<UIGrid> ();
		if (ug != null) {
			foreach (UIGrid item in go.GetComponents<UIGrid>()) {
				Destroy (item);
			}
		}			
		ug = go.AddComponent<UIGrid> ();
		ug.arrangement = UIGrid.Arrangement.Horizontal;
		ug.maxPerLine = 0;
		ug.cellWidth = cellWidth;
		ug.cellHeight = 200;
		ug.sorted = true;
		ug.hideInactive = true;
	}
	
	void addButtons (Transform transPause, Transform goButtons, GameObject[] panels)
	{
		
		/*
		string[] prefabs = new string[] { "BtnHome", "BtnShop", "BtnLevel", "BtnReplay", "BtnCancel" };
		GameObject[] gos = Globe.getPrefabButtons (prefabs);

		for (int i = 0; i < gos.Length; i++) {
			GameObject goh = (GameObject)Instantiate (gos [i]);
			goh.transform.parent = goButtons;
			goh.transform.localScale = new Vector3 (1f, 1f, 0.0025f);
			goh.transform.localPosition = new Vector3 (0f, 0f, 0f);
			goh.name = "p" + i + prefabs [i];
			if (i < 3)
				addUIButtonTween (goh, panels [i], gameObject);
		}

		addGrid (goButtons.gameObject, 110);

		foreach (GamePauseAftermath item in transPause.GetComponentsInChildren<GamePauseAftermath>()) {
			item.transLevelPanel = panels [2].transform;
			item.transPausePanel = transform.FindChild ("PanelPause");
			switch (item.transform.name) {
			case "p2BtnLevel":
//				item.resetLevel = false;
				item.resetPause = true;
				item.resetPlay = false;
				item.removeCard = true;
				break;
			case "p3BtnReplay":
//				item.resetLevel = false;
				item.resetPause = true;
				item.resetPlay = true;
				item.removeCard = false;
				break;
			case "p4BtnCancel":
//				item.resetLevel = false;
				item.resetPause = true;
				item.resetPlay = false;
				item.removeCard = false;
				break;
			}
		}*/
	}

	void addUIButtonTween (GameObject obj3, GameObject targets, GameObject targetSelf)
	{
		UIButtonTween bt = null;

		if (obj3.GetComponent<UIButtonTween> () != null) {
			foreach (UIButtonTween item in obj3.GetComponents<UIButtonTween>()) {
				DestroyImmediate (item, true);
			}
		}

		bt = obj3.AddComponent<UIButtonTween> ();//obj3[i].GetComponent<UIButtonTween>();//
		bt.tweenTarget = targetSelf;
		bt.includeChildren = true;
		bt.resetOnPlay = false;
		bt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		bt.disableWhenFinished = AnimationOrTween.DisableCondition.DisableAfterForward;
		bt.trigger = AnimationOrTween.Trigger.OnClick;
		bt.playDirection = AnimationOrTween.Direction.Forward;

		bt = obj3.AddComponent<UIButtonTween> ();//obj3[i].GetComponent<UIButtonTween>();//
		bt.tweenTarget = targets;
		bt.includeChildren = true;
		bt.resetOnPlay = false;
		bt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
		bt.disableWhenFinished = AnimationOrTween.DisableCondition.DisableAfterReverse;
		bt.trigger = AnimationOrTween.Trigger.OnClick;
		bt.playDirection = AnimationOrTween.Direction.Forward;

	}
	/// <summary>
	/// 重置卡牌
	/// </summary>
	public void resetCards ()
	{
		//清理卡牌
		removeCards ();
		initGameWindow ();
	}
	/// <summary>
	/// 清理卡牌
	/// </summary>
	public void removeCards ()
	{
		//清理卡牌
		transform.FindChild ("GameWindow").GetComponent<UIItemStorageTest> ().cleaner ();//PanelPause		
		
	}


}
