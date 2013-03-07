using UnityEngine;
using System.Collections.Generic;

public class ExampleAtlas : MonoBehaviour
{
	
	public delegate void replaceSprite ();
	public event replaceSprite EventReplace;
	
	public GameObject nextLayer;
	private string[] atlases;
	int index = 0;
	int count=0;
	bool clearance=false;
	UISlicedSprite sprite;

	void Start ()
	{
		sprite = GetComponent<UISlicedSprite> ();
//		sprite.transform.rotation = Quaternion.Euler (0f, 180f, 0f);
	}
	void Update()
	{
		toPanelWin ();
	}
	public void NextSprite (string name)
	{		
		if (sprite == null) {
			print (name + "|| is null" + index);
			sprite = this.transform. GetComponent<UISlicedSprite> ();
		}

		if (EventReplace != null && Globe.askatlases.Contains(name)) {
			//do replace && Globe.sameSize.ContainsKey (name)
			index++;//print (index+" || "+name);
			if(index == Globe.findCount)
			{
				switch (PlayerPrefs.GetInt("NowMode")) {
				case 1:
					index=0;
					clearance=true;
					PlayerPrefs.SetInt("result",1);
//					toPanelWin(1);
					break;
				case 2:
					break;
				case 3:
					break;
				}
			}
//			int _value = Globe.sameSize [name];
//			print ("eventreplace is not null -- "+_value+"--"+index+" -- ");
			if (count - 1 == 0) {
				//找一对，显示成功页面
				print (transform.parent.name);				
				//更换下一个图片
				
//				Globe.sameSize.Remove (name);
				
				sprite.spriteName = atlases [0];
			} else {
//				Globe.sameSize.Remove (name);
//				Globe.sameSize.Add (name, _value - 1);			
			}
						
			//移空所有项，显示成功页面			
//			sprite.spriteName = name;
		} else {			
			sprite.spriteName = name;
			
		}	
		
		
//		sprite.transform.rotation = Quaternion.Euler (0f, 180f, 0f);
		sprite.MakePixelPerfect (); 
		
		//return sprite.spriteName;
	}
	
	void toPanelWin ()
	{
		//写通关数据
//		print(LitJsonUtil.readAll(Globe.jsonURL.Replace("file://",""), 0));
		
//		transform.parent.parent.GetComponent<TweenPosition> ().Reset();		
//		nextLayer.GetComponent<TweenPosition> ().Play (true);		
//		nextLayer.GetComponent<GameWinLayer>().init(result);
		
		if (PlayerPrefs.GetInt ("turn_go_over") == 1 && clearance) {			
			PlayerPrefs.DeleteKey("turn_go_over");
			Application.LoadLevel("GameUpshot");			
		}
//		PlayerPrefs.SetInt("result",result);
//		Application.LoadLevel("GameUpshot");
	}
	
	/// <summary>
	/// 提示动画结束
	/// </summary>
	/// <returns>
	/// The over.
	/// </returns>
	/// <param name='a'>
	/// If set to <c>true</c> a.
	/// </param>
	public bool dollyOver(int a)
	{
		PlayerPrefs.SetInt("animate_exam_over",1);
		return true;
	}
}
