using UnityEngine;
using System.Collections;

public class GamePause : MonoBehaviour
{
	
	public Transform transMaskPanel;
	
	void Update()
	{
		if (PlayerPrefs.GetInt("game_animate_over") == 1) {
//			transMaskPanel.
		}
	}
	void OnClick ()
	{
		if (transMaskPanel != null) {
							
			UISlicedSprite uisp = transMaskPanel.GetComponent<UISlicedSprite> ();
			uisp.color= new Color (1.0f, 1.0f, 1.0f, 0.65f);
				
			BoxCollider box = transMaskPanel.GetComponent<BoxCollider> ();
			if (box.enabled) {
				box.enabled=false;
			}else
				box.enabled=true;
		}
	}
}
