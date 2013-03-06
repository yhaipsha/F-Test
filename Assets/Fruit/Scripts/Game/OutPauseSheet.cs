using UnityEngine;
using System.Collections;

public class OutPauseSheet : MonoBehaviour
{
	
	public GameObject	gameObj;
	//播放动画与不播放
	bool isPlayAnim = false;
	
	void Start ()
	{
		
	}
	
	void OnClick ()
	{
		if (gameObj != null) {
			
			
			if (isPlayAnim) {
				//停止动画
				isPlayAnim = false;
				//销毁UISpriteAnimation组件
				Destroy (gameObj.GetComponent<TweenPosition> ());
				

			} else
			{
				//播放播放
				isPlayAnim = true;
				gameObj.AddComponent ("TweenPosition");
			
				TweenPosition tp = gameObj.GetComponent<TweenPosition> ();
				tp.method = UITweener.Method.EaseInOut;
				tp.style = UITweener.Style.Once;
				tp.from = new Vector3 (0, -640, -1);
				tp.to = new Vector3 (0, 0, -1);
				tp.enabled = true;
				
				tp.Play (true);
			
				//gameObj.transform.position= new Vector3(0,0,0);
				//Destroy (tp);
			}
		}
	}
	public LayerMask mask = -1;
	// Update is called once per frame
	void Update ()
	{
		if (Physics.Raycast(transform.position, transform.forward, 100, mask.value))
			Debug.Log("Hit something");
		
		
		if(transform.gameObject.activeSelf)
			print("is active");
		else{
			print ("isn't active");
			Destroy (gameObj.GetComponent<TweenPosition> ());
		}
	}
	
	
}
