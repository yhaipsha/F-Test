using UnityEngine;
using System.Collections;

public delegate void closeCollider ();

public class GamePause : MonoBehaviour
{
	public event closeCollider EventPause;
	void Update ()
	{
		if (transform.position.y == 0) {
			colliderState (true);
		}
		if (transform.position.y < -1) {
			colliderState (false);
		}
	}
	
	void colliderState (bool on)
	{
		Transform ts = this.transform.FindChild ("SlicedSpriteMask");
		if (ts != null) {
							
			UISlicedSprite uisp = ts.GetComponent<UISlicedSprite> ();
			uisp.color = new Color (1.0f, 1.0f, 1.0f, 0.65f);
				
			BoxCollider box = ts.GetComponent<BoxCollider> ();
//			if (box.enabled) {
//				box.enabled=false;
//			}else
				box.enabled = on;
		}			
	}
	
}
