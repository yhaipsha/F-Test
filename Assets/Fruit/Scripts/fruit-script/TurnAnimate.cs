using UnityEngine;
using System.Collections;

public class TurnAnimate : MonoBehaviour
{	
	public event ProcessDelegate ProcessEvent;
	public event animatePlayOver EventPlayOver;

	public UISprite sprite;
	public UISprite spriteBg;

//	private GameObject target;
	public bool autoReverse = false;
	UISlicedSprite spHead;
	// Use this for initialization
	
	void ProcessAction (object sender, string e)
	{
		if (ProcessEvent == null)
			ProcessEvent += new ProcessDelegate (t_ProcessEvent);
		ProcessEvent (sender, e);
	}

	//如果没有自己指定关联方法，将会调用该方法抛出错误
	void t_ProcessEvent (object sender, string e)
	{
		print ("The method or operation is not implemented.??" + e);
	}
	string playOver (string animateName)
	{
		if (animateName == "turn_go_over" && PlayerPrefs.GetInt("NowMode") !=1) {
				SendMessageUpwards("doPunish",transform.name);			
		}
		else if(animateName =="turn_back_over")
		{
			SendMessageUpwards("delayPunish",transform.name);	
		}else if (animateName == "turn_go_over" && PlayerPrefs.GetInt("NowMode") ==1)
		{
			PlayerPrefs.SetInt ("turn_go_over",1);
		}
		return animateName;
	}
	
	void Start ()
	{		
		EventPlayOver += new animatePlayOver(playOver);
	}

	void OnClick ()
	{		
//		if (sprite != null && spriteBg != null) 
		{	
			switch (PlayerPrefs.GetInt ("NowMode")) {
			case 1:
				SendMessageUpwards ("mode1", transform.name);
				break;
			case 2:
				SendMessageUpwards ("mode2", transform.name);
				break;
			case 3:
				SendMessageUpwards("mode2",transform.name);
				break;
			}		
		}
	}

	void Update ()
	{

		if (PlayerPrefs.GetInt ("cardReady") == 1) {
			SendMessageUpwards ("show");
			PlayerPrefs.SetInt ("cardReady", 2);
		}
	}

	void mode11111 ()
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
			

//		ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas> (); 
//		if (target != null) {
		if (head == theNumber) {
			print (theNumber);
//				animation.PlayQueued("TurnGo",QueueMode.PlayNow);
				
			playReplace ();
			autoReverse = false;
//				ra.NextSprite (spHead.spriteName);
				
		} else {				
//				transform.animation["SpriteTurn"].time=0;
//				animation.PlayQueued("TurnGo",QueueMode.CompleteOthers);
//				animation.PlayQueued("TurnBack",QueueMode.PlayNow);
//				animation.Play("TurnBack");
//				transform.animation.Stop();
//				transform.animation.Sample();
//				gameObject.SampleAnimation(animation.clip, 0);				
			SendMessageUpwards ("turnBack", transform.name);
			SendMessageUpwards ("UpdateTime", 3 - Globe.differentSize.Count);		
				
			if (Globe.differentSize.Count >= 3) {
//					ra.toPanelWin (0);
			}
		}

//		}
	}

	void mode2222222 ()
	{//经典模式-看5秒找相同水果，限错N次";

		if (Globe.askatlases.Count > 0 && Globe.thisPanel != null && Globe.askatlases [0] != transform.name) {
			
			UISprite ltsp = Globe.thisPanel.FindChild ("Sprite-box").GetComponent<UISprite> ();
			UISprite tsp = transform.FindChild ("Sprite-box").GetComponent<UISprite> ();
		
			print (ltsp.spriteName + "?" + tsp.spriteName);
			if (ltsp.spriteName == tsp.spriteName) {
//				playReplace ();
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

	

	IEnumerator playReplace ()
	{
		//		ExampleAtlas ra = target.transform.GetComponent<ExampleAtlas>();
		//		ra.init();
		//		autoReverse = false;
		yield return new WaitForSeconds(0.9f);
		//		ra.NextSprite(sp.spriteName);
	}
}
