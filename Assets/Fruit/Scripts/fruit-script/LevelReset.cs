using UnityEngine;
using System.Collections;

public class LevelReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject levelOffsetGo;
	void OnClick()
	{
		// 重置 (中途退出)
		levelOffsetGo.GetComponent<UIItemCard> ().cleaner();
		
	}
}
