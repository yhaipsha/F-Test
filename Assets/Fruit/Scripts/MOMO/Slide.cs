using UnityEngine;
using System.Collections;

public class Slide : MonoBehaviour
{
	bool isTouch = false;
	bool isRight = false;
	bool isLeft = false;
	bool isOnDrag = false;

	void OnDrag (Vector2 delta)
	{
		if (!isTouch) {
			if (delta.x > 0.5) {
				isRight = true;
				isOnDrag = true;
			} else if (delta.x < -0.5) {

				isLeft = true;
				isOnDrag = true;
			}

			isTouch = true;
		}

	}

	void OnPress ()
	{

		if (Globe.helper.list_currentIndex < Globe.helper.list_count && isLeft) {
			Globe.helper.list_currentIndex++;
		}



		if (Globe.helper.list_currentIndex > 0 && isRight) {
			Globe.helper.list_currentIndex--;
		}
		isTouch = false;
		isLeft = false;
		isRight = false;
	}

	void Update ()
	{
	
		if(Globe.helper != null){
//			print (Globe.helper.ListPanel);
		Globe.helper.ListPanel.transform.localPosition = Vector3.Lerp (Globe.helper.ListPanel.transform.localPosition, new Vector3 (-(Globe.helper.list_currentIndex * Globe.helper.list_offset), 0, 0), Time.deltaTime * 5);
			
		}
	}

	void OnClick ()
	{

	}

}
