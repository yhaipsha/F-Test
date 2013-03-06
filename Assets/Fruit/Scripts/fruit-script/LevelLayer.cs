using UnityEngine;
using System.Collections;

public class LevelLayer : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		levelOffsetGo = transform.FindChild ("Panel-sprite").FindChild ("Offset");
		init ();
		itemCardLayer ();
	}
	Transform levelOffsetGo ;
	// Update is called once per frame
	void Update ()
	{
	
	}

	void init ()
	{
		transform.FindChild ("LabelCaption").GetComponent<UILabel> ().text = PlayerPrefs.GetString ("NowModeCaption");
	}
	
	public void cleanLevels()
	{
		// 重置 (中途退出)
		levelOffsetGo.GetComponent<UIItemCard> ().cleaner();
		
	}
	
	void itemCardLayer ()
	{
		int maxItem = 0;
		int pages = 1;

		switch (PlayerPrefs.GetInt ("NowMode")) {
		case 1:
			maxItem = 50;
			pages = 3;
			break;

		case 2:
			maxItem = 50;
			pages = 3;
			break;
		case 3:
			maxItem = 20;
			pages = 1;
			break;
		}
		
		UIItemCard card = levelOffsetGo.GetComponent<UIItemCard> ();
		UIPanel panel = card.transform.parent.GetComponent<UIPanel> ();
		if (levelOffsetGo.transform.GetChildCount () > 0)
			card.cleaner ();


		Vector4 v4 = Vector4.zero;// panel.clipRange;
		v4.x = 13.8f;
		v4.y = panel.clipRange.y;
		v4.z = panel.clipRange.z;
		v4.w = panel.clipRange.w;

		panel.clipRange = v4;
		card.transform.parent.localPosition = new Vector3 (0f, 0f, 0f);
		card.transform.localPosition = new Vector3 (-311f, 225f, -10f);
                

		card.maxItemCount = maxItem;
		card.rows = 4;
		card.columns = 5;
		card.pages = pages;
		card.initLevel ();
        		
	}
}
