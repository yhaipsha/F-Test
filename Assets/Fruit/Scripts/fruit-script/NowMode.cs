using UnityEngine;
using System.Collections;

public class NowMode : MonoBehaviour {

	
//	public GameObject levelOffsetGo;
	void OnClick()
	{
        string Caption = string.Empty;
        switch (transform.name)
        {
            case "BtnTop":
                Caption = "Normal Mode";//"标准模式-看3秒，找出指定水果，限错3次";
                PlayerPrefs.SetInt("NowMode", 1);
                break;
            case "BtnMiddle":
                Caption = "Classic Mode";//"经典模式-看5秒找相同水果，限错N次";
                PlayerPrefs.SetInt("NowMode", 2);
                break;
            case "BtnBotton":
                Caption = "Challenge Mode";//"挑战模式-限时找相同水果，不限错次数";
                PlayerPrefs.SetInt("NowMode", 3);
                break;
        }
		PlayerPrefs.SetString("NowModeCaption",Caption);
//		PlayerPrefs.Save();
//        itemCardLayer();
//		Application.LoadLevelAdditive("Example - FruitLevel");
//		Application.LoadLevel("Game");
		Application.LoadLevel("Level");
	}
	
	/*
    public void itemCardLayer()
	{
        int maxItem = 0;
        int pages = 1;

        switch (PlayerPrefs.GetInt("NowMode"))
        {
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

        UIItemCard card = levelOffsetGo.GetComponent<UIItemCard>();
        UIPanel panel = card.transform.parent.GetComponent<UIPanel>();
        if (levelOffsetGo.transform.GetChildCount() > 0)
            card.cleaner();


        Vector4 v4 = Vector4.zero;// panel.clipRange;
        v4.x = 13.8f;
        v4.y = panel.clipRange.y;
        v4.z = panel.clipRange.z;
        v4.w = panel.clipRange.w;

        panel.clipRange = v4;
        card.transform.parent.localPosition = new Vector3(0f, 0f, 0f);
        card.transform.localPosition = new Vector3(-311f, 225f, -1f);
                

        card.maxItemCount = maxItem;
        card.rows = 4; card.columns = 5;
        card.pages = pages;
        card.initLevel();
        		
	}*/
}
