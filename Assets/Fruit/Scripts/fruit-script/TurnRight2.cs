using UnityEngine;
using System.Collections;

public class TurnRight2 : MonoBehaviour
{
	/// <summary>
	/// 绕sprite中心左右旋转
	/// </summary>
	
	public UISprite sprite;
	public UISprite spriteBg;
	
	//只有精灵脚本
	private GameObject target;
	private bool isBegin = false;
	public bool autoReverse = false;

	int clickCount = 0;
	Quaternion qua;
	Quaternion quaBg;
	UISlicedSprite spHead;
	// Use this for initialization
	void Start ()
	{		
		sprite.enabled = false;
		target = GameObject.FindWithTag ("Player");
		
		Globe.sameSize = new System.Collections.Generic.Dictionary<string, int> ();
		Globe.differentSize = new System.Collections.Generic.Dictionary<string, int> ();
		Globe.tempGameObject = new System.Collections.Generic.List<UnityEngine.GameObject> ();
		
		if (target != null) {
			ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas> ();
			ra.EventReplace += new ExampleAtlas.replaceSprite (OnClick);
			spHead = target.transform.GetComponent<UISlicedSprite> ();	
		}
	}

	void LateUpdate ()
	{
		if (PlayerPrefs.GetInt ("cardReady") == 2) {		
//			qua = Quaternion.Euler (Vector3.zero) * spriteBg.transform.localRotation;	
//			quaBg = Quaternion.Euler (new Vector3 (0f, 180f, 0f)) * sprite.transform.localRotation;				
//			StartCoroutine (show ());
		}
	}

	void Update ()
	{

		if (PlayerPrefs.GetInt ("cardReady") == 1) {
//			qua = Quaternion.Euler (new Vector3 (0f, 180f, 0f)) * sprite.transform.localRotation;	
//			quaBg = Quaternion.Euler (Vector3.zero) * spriteBg.transform.localRotation;			
//			StartCoroutine (show ());

		}
      
//		if (isBegin) {
//
//			spriteBg.transform.rotation = Quaternion.Slerp (spriteBg.transform.rotation, quaBg, Time.deltaTime * 3);
//			sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation, qua, Time.deltaTime * 3);
//
//			float vEuler = Mathf.Round (spriteBg.transform.rotation.eulerAngles.y);
//			//反面
////			IsNotCorrect (Mathf.Round (spriteBg.transform.rotation.eulerAngles.y));
//			//正面
//			IsCorrect (Mathf.Round (sprite.transform.rotation.eulerAngles.y));
//
//		}
		
	}
	void OnClick ()
	{		
		++clickCount;
		if (sprite != null && spriteBg != null) {//		&& !isBegin
			
			isBegin = true;
			qua = Quaternion.Euler (0f, 180f, 0f) * sprite.transform.localRotation;
			quaBg = Quaternion.Euler (0f, 180f, 0f) * spriteBg.transform.localRotation;
			
			switch (PlayerPrefs.GetInt ("NowMode")) {
			case 1:
				mode1 ();
				break;
			case 2:
				mode2 ();
				break;
			}
		}
	}

	void mode1 ()
	{//"标准模式-看3秒，找出指定水果，限错3次";
		string theNumber = RegexUtil.RemoveNotNumber (sprite.spriteName);
		string head = RegexUtil.RemoveNotNumber (spHead.spriteName);
		if (head == theNumber) {
			//signName = "Right";				
			if (Globe.sameSize.ContainsKey (spHead.spriteName)) {
				Globe.sameSize [spHead.spriteName]++;
			} else
				Globe.sameSize.Add (spHead.spriteName, 1);
		} else {
			//signName = "Wrong";				
			if (Globe.differentSize.ContainsKey (transform.name)) {
				Globe.differentSize [transform.name]++;
			} else
				Globe.differentSize.Add (transform.name, 1);
		}						
			

		ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas> (); 
		if (target != null) {
			if (head == theNumber) {
				StartCoroutine (playReplace ());
				autoReverse = false;
				ra.NextSprite (spHead.spriteName);
			} else {				
					
				SendMessageUpwards("UpdateTime",3-Globe.differentSize.Count);				
				if (Globe.differentSize.Count >= 3) {
					ra.toPanelWin (0);
				}
			}

		}
	}
	void mode2 ()
	{//经典模式-看5秒找相同水果，限错N次";

		if (Globe.askatlases.Count > 0 && Globe.thisPanel != null && Globe.askatlases [0] != transform.name) {
			
			UISprite ltsp = Globe.thisPanel.FindChild ("Sprite-box").GetComponent<UISprite> ();
			UISprite tsp = transform.FindChild ("Sprite-box").GetComponent<UISprite> ();
		
			print (ltsp.spriteName + "?" + tsp.spriteName);
			if (ltsp.spriteName == tsp.spriteName) {
				playReplace ();
				Destroy (Globe.thisPanel.gameObject);
				Destroy (gameObject);
				Globe.askatlases.Clear ();
			} else {
				;
//				StartCoroutine(show());
				Globe.thisPanel.gameObject.GetComponent<TurnRight2> ().Turn ();
//				this.OnClick();
			}
			
			Globe.thisPanel = null;
			Globe.askatlases.Clear ();
			
		} else if (!Globe.askatlases.Contains (transform.name)) {
			//记录上次精灵
			Globe.askatlases.Add (transform.name);
			Globe.thisPanel = transform;
			print (transform.name);
			autoReverse = false;
		}
		
		if (transform.parent.GetChildCount () <= 0) {
			print ("----------");
			GameWinLayer gw = Globe.getPanelOfParent (transform.parent.parent, 1, "Panel - GameWin").GetComponent<GameWinLayer> ();
			gw.init (1);
			
		}
			
	}

	IEnumerator waitSecond3 ()
	{
		yield return new WaitForSeconds(3);
	}

	IEnumerator show ()
	{				
		sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation, qua, Time.deltaTime);     		
		spriteBg.transform.rotation = Quaternion.Slerp (spriteBg.transform.rotation, quaBg, Time.deltaTime);   
		
		if (PlayerPrefs.GetInt ("cardReady") == 1) {
			bool correct = IsCorrect (Mathf.Round (sprite.transform.rotation.eulerAngles.y));
			if (correct) {				
				PlayerPrefs.SetInt ("cardReady", 2);
//				print ("in the correct =if");
//				yield return null;  					
				yield return new WaitForSeconds(5.0F);
//				yield return new  WaitForSeconds(Time.deltaTime * 30f);	
			}
			
		} else if (PlayerPrefs.GetInt ("cardReady") == 2) {
						
			//反面
			bool correct = IsNotCorrect (Mathf.Round (spriteBg.transform.rotation.eulerAngles.y));
			if (correct) {				
				SendMessageUpwards("examPlay");
				PlayerPrefs.DeleteKey ("cardReady");
//				print ("in the correct =else");
//				yield return null;  
				yield return new WaitForSeconds(5.0F);
				
			}		
		}
			
		/*
//		//正面
		float timeD = Time.deltaTime * 57f;
//		print (timeD);return false;
		

//		print (font+"<<>>"+Correct);
//		print(timeD+"|WaitForSeconds" + Time.time);
//		
//		if (Time.time - timeD >= 3 && PlayerPrefs.GetInt ("cardReady") == 1) {
//			print ("in the Time=if");
//			PlayerPrefs.SetInt ("cardReady", 2);
//		} 
		else if (Time.time - timeD >= 3 && PlayerPrefs.GetInt ("cardReady") == 2) {
			print ("in the Time=else");
			PlayerPrefs.DeleteKey ("cardReady");
		}*/
		
		yield return  new WaitForSeconds(57.0f);		
	}

	bool IsNotCorrect (float euler)
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
	
	bool IsCorrect (float euler)
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

	IEnumerator showWait3 ()
	{
		yield return new  WaitForSeconds(Time.deltaTime *114.0f);		
	}

	public void Turn ()
	{
		isBegin = true;
		qua = Quaternion.Euler (0f, 180f, 0f) * sprite.transform.localRotation;
		quaBg = Quaternion.Euler (0f, 180f, 0f) * spriteBg.transform.localRotation;
		
		spriteBg.transform.rotation = Quaternion.Slerp (spriteBg.transform.rotation, quaBg, Time.deltaTime);   
		sprite.transform.rotation = Quaternion.Slerp (sprite.transform.rotation, qua, Time.deltaTime);    
	}

	
	void mode3 ()
	{//"挑战模式-限时找相同水果，不限错次数";
		
	}

	IEnumerator playReplace ()
	{
		//		ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas>();
		//		ra.init();
		//		autoReverse = false;
		yield return new WaitForSeconds(0.9f);
		//		ra.NextSprite(sp.spriteName);
	}
	

}
