using UnityEngine;
using System.Collections;

public class GamePlayReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Transform transGamePlay;
    void OnClick()
    {
        GamePlayLayer gpl= transGamePlay.GetComponent<GamePlayLayer>();

        //µ±Ç°¹Ø¿¨
        int currentLevel = PlayerPrefs.GetInt("NowPlay") - 1;
        gpl.initGameWindow();

    }
}
