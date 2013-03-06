using UnityEngine;
using System.Collections.Generic;

public class HistoryInit : MonoBehaviour
{
	public GameObject prefab;
	public GameObject prefabhui;
	public GameObject prefabbai;
	public Transform  ponit;
	private GameObject bai;
	public string[] atlasSpriteNames;
	List<UserData> users = new List<UserData> ();
	int start;
	int widthV = 576;

	void Start ()
	{
//		Globe.ListPanel = gameObject;
		Globe.helper = new HelpFruit();
		Globe.helper.ListPanel = gameObject;
		loadSQL ();
		if (atlasSpriteNames != null)
			initItem ();
	}

	void loadSQL ()
	{
		for (int i =0; i< atlasSpriteNames.Length; i ++) {
			string name = "momo =" + i;
			string age = "26 = " + i;
			string height = "183 =" + i;
			users.Add (new UserData (name, age, height));
		}
	}

	void initItem ()
	{
		int size = atlasSpriteNames.Length;
		int length = (size - 1) * 16;	
		start = (-length) >> 1;

		for (int i=0; i< size; i++) {
			GameObject o = (GameObject)Instantiate (prefab);
			o.transform.parent = transform;
			o.transform.localPosition = new Vector3 (i * widthV, 30f, 0f);
			o.transform.localScale = new Vector3 (0.9f, 0.9f, 1f);
			UISprite sp = o.GetComponentInChildren<UISprite> ();			
			sp.spriteName = atlasSpriteNames [i];
			sp.MakePixelPerfect ();
			setHui (i);
		}
//		Globe.list_count = size - 1;
//		Globe.list_offset = widthV;
//		Globe.list_currentIndex = 0;
//		Globe.list_go_name = "LoadScene";
		Globe.helper.list_count= size-1;
		Globe.helper.list_offset=widthV;
		Globe.helper.list_currentIndex=0;
		Globe.helper.list_go_name="LoadScene";
		
		bai = (GameObject)Instantiate (prefabbai);
		bai.GetComponent<UISprite> ().depth = 1;
		setBaiPos ();
	}

	void Update ()
	{
		if (bai != null) {
			setBaiPos ();
		}
	}

	void setHui (int p)
	{
		GameObject hui = (GameObject)Instantiate (prefabhui);
		hui.transform.parent = ponit;
		hui.transform.localPosition = new Vector3 (start + p * 100, 0f, 0f);
		hui.transform.localScale = new Vector3 (20, 20, 1);
		hui.GetComponent<UISprite> ().depth = 0;

	}

	void setBaiPos ()
	{
		bai.transform.parent = ponit;
		bai.transform.localPosition = new Vector3 (start + Globe.helper.list_currentIndex * 100, 0f, -10f);
		bai.transform.localScale = new Vector3 (30, 30, 1);

	}

}
