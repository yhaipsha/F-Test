using UnityEngine;
using System.Collections;

public delegate void ProcessDelegate (object sender,string e);

public delegate string animatePlayOver (string animateName);

public class TurnManager : MonoBehaviour
{	
	public delegate void diePunish (string lname,string name);
	
	public Transform transExample;
	public Transform transTimes;
	public Transform transLable;
	int initialized = 0;
	int _mode = 0;
	UISlicedSprite spHead;
	ExampleAtlas examObj;
	float backTime;//用于计数的变量
	
	private bool autoReverse = false;
	int clickCount = 0;
	private bool isBegin = false;
	Quaternion qua;
	Quaternion quaBg;
	UISprite sprite;
	UISprite spriteBg;
	bool beginTurn = false;
	bool punish = false;
	TurnAnimate ta;
	
	bool clearance=false;
	void toPanelWin (int result)
	{
		if (PlayerPrefs.GetInt ("turn_go_over") == 1 && clearance) {	
			PlayerPrefs.SetInt("result",result);
			PlayerPrefs.DeleteKey("turn_go_over");
			Application.LoadLevel("GameUpshot");			
		}
	}
	
	void doPunish (string name)
	{
		
		if (name != Globe.tmpString && Globe.punish) {
			Destroy (getTransOfSprite (name).gameObject);	
			Destroy (getTransOfSprite (Globe.tmpString).gameObject);				
			Globe.tmpString = null;
			Globe.punish = false;
		}
		if (transform.GetChildCount () <= 2) {
			toPanelWin (1);
		}
		
//		switch (PlayerPrefs.GetInt("NowMode")) {
//		case 1:
//			getTransOfSprite (name).collider.enabled=false;
//			break;
//		case 2:
//		case 3:
//			
//			break;
//		}		
		
	}
	
	void delayPunish (string name)
	{
		//
//		getTransOfSprite (name).collider.enabled=true;
	}

	void Start ()
	{							
		Globe.sameSize = new System.Collections.Generic.Dictionary<string, int> ();
//		Globe.differentSize = new System.Collections.Generic.Dictionary<string, int> ();
//		Globe.tempGameObject = new System.Collections.Generic.List<UnityEngine.GameObject> ();
		
		if (transExample != null) {
			examObj = transExample.GetComponent<ExampleAtlas> ();
			examObj.EventReplace += new ExampleAtlas.replaceSprite (appriseExam);
			spHead = transExample.GetComponent<UISlicedSprite> ();	
		}
	}

	void Update ()
	{
		_mode = PlayerPrefs.GetInt ("NowMode");
		if (initialized == 2 && _mode == 1) {			
			examPlay ();	
			init (2);
			if (PlayerPrefs.GetInt ("animate_exam_over") == 1) {
				init (3);
				initialized = 0;//reset initialized
				PlayerPrefs.DeleteKey ("animate_exam_over");
				PlayerPrefs.DeleteKey ("cardReady");
			}
		}
		
		if (initialized == 2 && _mode == 2) {			
			initialized = 0;
			examPlay ();	
			init (2);
			if (PlayerPrefs.GetInt ("animate_exam_over") == 1) {
				init (3);
				initialized = 0;
				PlayerPrefs.DeleteKey ("cardReady");
			}
		}
		
		//--------------游戏过程
		if (initialized == 2 && _mode == 3) {
//			print (backTime.ToString ("F0"));//每帧都更新时间变化，F0表示小数点后显示0位，如果你需要可以改变0为相应的位数！		
			backTime = backTime - Time.deltaTime;//总时间i减去每帧消耗的时间，当然就等于当前时间啦，对吧？
			transTimes.GetComponent<UILabel> ().text = "0:" + backTime.ToString ("F0");	
			
			if (backTime > 29.0f) {
				init (2);
				PlayerPrefs.DeleteKey ("cardReady");
			} else if (backTime <= 0.0f) {
//				examObj.toPanelWin (0);
				toPanelWin (0);
			}
		}
		
		//-------------提交提示图片
//		if (examObj != null && PlayerPrefs.GetInt ("turn_go_over") == 1) {
//			examObj.NextSprite (spHead.spriteName);
//			PlayerPrefs.DeleteKey("turn_go_over");
//		}
		
		
//		if (PlayerPrefs.GetInt ("cardReady") == 1 && initialized != 0) {
//			StartCoroutine (show ());
//		}
	}

	void examPlay ()
	{	    
		if (transExample != null) {
			transExample.GetComponent<UISlicedSprite> ().enabled = true;
			transExample.animation.Play ("Center_UpRight");
		}
	}
	
	void mode1 (string name)
	{//"标准模式-看3秒，找出指定水果，限错3次";
		string theNumber = RegexUtil.RemoveNotNumber (getSpriteName (name));//print (spHead.spriteName);
		string head = RegexUtil.RemoveNotNumber (spHead.spriteName);		
//		clickedCount (head, theNumber, name);		
		if (head == theNumber) {
			//answer is right == 相同的点击不计数 
			if (!Globe.sameSize.ContainsKey (name)) {
				Globe.sameSize.Add (name, 1);
			} 
			getTransOfSprite (name).collider.enabled = false;//正确的禁止点击多次
			turnTo (name, "TurnGo");
			appriseExam ();
		} else {
			//answer is wrong == 相同的点击计数
			Globe.errorCount--;//= ++clickCount;
//			print (Globe.errorCount);
//			if (Globe.differentSize.ContainsKey (name)) {
//				Globe.differentSize [name]++;
//			} else
//				Globe.differentSize.Add (name, 1);
			
			turnTo (name, "TurnBack");
			UpdateTime (Globe.errorCount);
				
			if (Globe.errorCount <= 0) {
				
//				examObj.toPanelWin (0);
				toPanelWin (0);
			}
		}
	}

	void mode2 (string name)
	{//经典模式-看5秒找相同水果，限错N次";
		if (Globe.askatlases.Count > 0 && Globe.tmpString != null) {
			string spriteName = getSpriteName (name);
			print (getSpriteName (Globe.tmpString) + "?" + spriteName);
			if (getSpriteName (Globe.tmpString) == spriteName) {
				Globe.punish = true;
				turnTo (name, "TurnGo");				
			} else {				
				turnTo (Globe.tmpString, "TurnBack");
				turnTo (name, "TurnBack");	
				
				if (PlayerPrefs.GetInt ("NowMode") == 2) {
					Globe.errorCount--;
					UpdateTime (Globe.errorCount);
					if (Globe.errorCount <= 0) {
						toPanelWin (0);
					}
				}
				
				Globe.tmpString = null;
			}
			Globe.askatlases.Clear ();	
		} else {
			//记录上次精灵
			Globe.askatlases.Add (name);
			Globe.tmpString = name;			
			turnTo (name, "TurnGo");
		}		
	}
	
	void appriseExam ()
	{
		if (examObj != null) {
			examObj.NextSprite (spHead.spriteName);
		}
	}

	public string getSpriteName (string name)
	{
		return transform.FindChild (name).FindChild ("Sprite-box").GetComponent<UISlicedSprite> ().spriteName;
	}

	Transform getTransOfSprite (string childrenName)
	{
		return transform.FindChild (childrenName);
	}
	
	bool turnTo (string spriteName, string animateName)
	{
		return transform.FindChild (spriteName).animation.Play (animateName);
//		animation.PlayQueued("TurnBack",QueueMode.PlayNow);
	}

	void UpdateTime (int score)
	{
		if (transTimes != null) {
			transTimes.GetComponent<UILabel> ().text = score.ToString ();
		}
	}
	
	IEnumerator show ()
	{				
		if (initialized == 1) {
			yield return  new WaitForSeconds(3.5f);
			init (-1);			
		} 
		
	}

	public void init (int state)
	{		
		int _mode = PlayerPrefs.GetInt ("NowMode");
		for (int i = 0; i < transform.GetChildCount(); i++) {
			Transform _trans = transform.GetChild (i);
//			Animation animate = _trans.animation;
			
			if (state == 1) {
				_trans.animation.Play ("TurnGo");
				_trans.collider.enabled = false;
			} else if (state == 2) {
				_trans.animation.Stop ();
				if (_mode != 1) {
					_trans.collider.enabled = true;
				}
			} else if (state == -1) {
				_trans.animation ["TurnGo"].time = 0;
				_trans.animation.Play ("TurnGo");
			} else if (state == 3) {
				_trans.collider.enabled = true;
			}
		}		
		
		if (state == 1) {
			initialized = 1;
		} else if (state == -1) {
			initialized = 2;
			backTime = 30;//假设从100开始倒数，这个数值你可以自行修改呀		
		}		
	}

	bool IsNotCorrect2 (float euler)
	{
//		print (euler);
//		if (euler ==0) {
//			return false;
//		}
		if (270 < euler && euler < 360) {
			spriteBg.enabled = false;
			sprite.enabled = true;
		} else if (195 < euler && euler < 270) {
			spriteBg.enabled = true;
			sprite.enabled = false;
			
		} else if (180 < euler && euler < 195) {
			return true;
		}		
		return false;
		
	}

	bool IsCorrect2 (float euler)
	{
		//做减法运算,默认已经旋转180
//		print (euler);
		if (euler > 300) {
			return true;
		}
		if (90 < euler && euler < 180) {			
			spriteBg.enabled = true;
			sprite.enabled = false;
		} else if (5 < euler && euler < 90) {
			spriteBg.enabled = false;
			sprite.enabled = true;
		} else if (0 < euler && euler < 5) {
			return isBegin = true;
		}
		return false;		
	}
	
	void OnTurn ()
	{
		isBegin = true;
		qua = Quaternion.Euler (0f, 180f, 0f) * sprite.transform.localRotation;
		quaBg = Quaternion.Euler (0f, 180f, 0f) * spriteBg.transform.localRotation;
		
//		spriteBg.transform.rotation = Quaternion.Slerp (spriteBg.transform.rotation, quaBg, Time.deltaTime);   
//		sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation, qua, Time.deltaTime);    
	}
	
	void IsNotCorrect (float euler)
	{
		if (0 < euler && euler < 180) {
			if (0 < euler && euler < 90) {
				//isBegin=false;
				spriteBg.enabled = true;
				sprite.enabled = false;
			}
			if (euler > 90) {					
				if (autoReverse) {
					this.OnTurn ();
					
				} else {
					//spriteBg.alpha=1f;			
					spriteBg.enabled = false;
					sprite.enabled = true;
				}
				isBegin = true;
			}
			
			clickCount = 0;	
			//isCorrect=false;
		}
	}

	void IsCorrect (float euler)
	{
		if (180 < euler && euler < 360) {				
			if (180 < euler && euler < 270) {
				//转过270度角
				//spriteBg.alpha=1f;			
				spriteBg.enabled = false;
				sprite.enabled = true;
			}
			if (euler > 270) {
				if (autoReverse) {
					this.OnTurn ();
					
				} else {
					spriteBg.enabled = true;
					sprite.enabled = false;
				}
				//isBegin=false;
			}

		}
	}
	
}
