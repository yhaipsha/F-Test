using UnityEngine;
using System.Collections.Generic;

public class HelpFruit
{
	public  GameObject ListPanel;
	public  int list_count;
	public  int list_currentIndex;
	public  int list_offset;
	public  string list_go_name;
}

public class Globe
{
	/*public static int list_count;
	public static int list_currentIndex;
	public static int list_offset;
	public static string list_go_name;*/
	
	public static string jsonURL = "file://" + Application.dataPath + @"/Fruit/Data/FruitJson.json";
	public static string assetURL = "file://" + Application.dataPath + @"/Fruit/Data/SystemData.assetbundle";
	public static string[] stayNames=new string[]{"Offset","GameWindow"};
	public static HelpFruit helper;
	public static Transform thisPanel;
	public static string tmpString;
	public static bool punish;
	
	public static Vector3 gamePause = new Vector3 (0, -680f, -5f);
	public static Vector3 cardPanel1 = new Vector3 (-256f, 128f, -4f);
	public static Vector3 cardPanel2 = new Vector3 (-330f, 128f, -4f);
	public static Vector3 cardPanel3 = new Vector3 (-410f, 128f, -4f);
	public static Vector3 cardPanel4 = new Vector3 (-410f, 220f, -4f);//220
	//随机分配卡牌
	public static string[] boxes = {"boxfind1","boxfind2","boxfind3","boxfind4"
		,"boxfind5","boxfind6","boxfind7","boxfind8"
		,"boxfind9","boxfind10","boxfind11","boxfind12"
		,"boxfind13","boxfind14","boxfind15","boxfind16"};
 
	//在游戏中储存卡牌
	public static int[] box;			//随机数组
	public static List<string[]> askbox;	//每个关卡数组
	public static List<string[]> askbox2;	
	public static List<string[]> askbox3;
	public static List<string> askatlases;//卡牌头 && lastSprite
	public static List<string> cards;		//卡牌
	public static int findCount;
	public static int errorCount=0;
	public static List<GameObject> tempGameObject;
	
	//public static List<int>sameSize ; //=new List<int>()
	public static Dictionary<string,int> sameSize;
	public static Dictionary<string,int> differentSize;
	public static Dictionary<string,GameObject> stayObject;
	public static  string[] jsonLableNames = new string[]{"star-first","star-second","star-third"};
	public static string Compare (int mode)
	{
		string star = string.Empty;
		switch (mode) {
		case 1:
			star = jsonLableNames[0];
			break;
		case 2:
			star =  jsonLableNames[1];
			break;
		case 3:
			star =  jsonLableNames[2];
			break;
		}
		return star;
	}

	public static GameObject[] getPrefabButtons (string[] names)
	{			
		GameObject[] gos = new GameObject[names.Length];
		for (int i = 0; i < names.Length; i++) {
			gos [i] = Resources.Load (names [i]) as GameObject;			
		}		
		
		return 	gos;		
	}
	public static  GameObject[] getPanelObject (Transform ts, string[] names)
	{		
		GameObject[] gos = new GameObject[names.Length];
		for (int i = 0; i < names.Length; i++) {
			gos [i] = ts.parent.FindChild (names [i]).gameObject;
		}
		
		return gos;			
	}
	
	public static Transform getPanelOfParent (Transform ts, int num, string childName)
	{
		Transform trans = null;
		switch (num) {
		case 1:
			trans = ts.parent.FindChild (childName);
			break;
		case 2:
			trans = ts.parent.parent.FindChild (childName);
			break;
		case 3:
			trans = ts.parent.parent.parent.FindChild (childName);
			break;
		case 4:
			trans = ts.parent.parent.parent.parent.FindChild (childName);
			break;
		}
		return trans;
	}

	
}
