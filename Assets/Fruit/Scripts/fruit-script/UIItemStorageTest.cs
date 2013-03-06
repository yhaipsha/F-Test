//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Storage container that stores items.
/// </summary>

[AddComponentMenu("NGUI/Game/UI Item Storage Test")]
public class UIItemStorageTest : MonoBehaviour
{
	/// <summary>
	/// Maximum size of the container. Adding more items than this number will not work.
	/// </summary>

	/// <summary>
	/// Maximum number of rows to create.
	/// </summary>

	public int maxRows = 4;

	/// <summary>
	/// Maximum number of columns to create.
	/// </summary>

	public int maxColumns = 4;

	/// <summary>
	/// Template used to create inventory icons.
	/// </summary>

	public GameObject template;

	/// <summary>
	/// Background widget to scale after the item slots have been created.
	/// </summary>

	public bool childrenAutoReverse = true;

	/// <summary>
	/// Spacing between icons.
	/// </summary>

	public int spacing = 128;

	/// <summary>
	/// Padding around the border.
	/// </summary>

	public int padding = 10;
	
	public Transform templateSprite;	
	List<InvGameItem> mItems = new List<InvGameItem>();


	void Start ()
	{		
		//createTemp();
		
	}
	GameObject addGameObject(string spriteName)
	{
		GameObject tempObj = (GameObject)Instantiate (template);
		tempObj.transform.parent = transform;
		tempObj.transform.localScale = new Vector3 (1f, 1f, 1f);
//		print ("in the test ="+spriteName);
//		tempObj.GetComponent<TurnRight2>().autoReverse = childrenAutoReverse;
		
		UISlicedSprite sprite = tempObj.transform.FindChild("Sprite-box").GetComponent<UISlicedSprite> ();			
//		sprite.transform.rotation = Quaternion.Euler (0f, 180f, 0f);
		sprite.spriteName = spriteName;		
		sprite.MakePixelPerfect ();
		
		return tempObj;
	}
	
	public static Dictionary<string, int> dir = new Dictionary<string, int>();
	
	public void createTemp(string[] arrSprites)
	{
		if (template != null)
		{
			int count = 0;
			Bounds b = new Bounds();
			int i=0;
			for (int y = 0; y < maxRows; ++y)
			{
				for (int x = 0; x < maxColumns; ++x)
				{
//					template.GetComponent<TurnRight2>().enabled=true;
					
					//GameObject go = NGUITools.AddChild(gameObject, template);					
					GameObject go = addGameObject(arrSprites[i]);
					go.name="player"+i;i++;
					Transform t = go.transform;
					t.localPosition = new Vector3(padding + (x + 0.5f) * spacing, -padding - (y + 0.5f) * spacing, 0f);
					b.Encapsulate(new Vector3(padding * 2f + (x + 1) * spacing, -padding * 2f - (y + 1) * spacing, 0f));

					if (++count >= arrSprites.Length)
					{
						return;
					}
				}
			}
		}
	}
	
	public void UpdateArrange()
	{
		Debug.Log("3333");
		
		NGUITools.SetActive(gameObject,true);
	}
	public void  cleaner()
	{
		for (int i = 0; i < transform.GetChildCount(); i++) {
			Destroy(transform.GetChild(i).gameObject,1.0f);
		}
		//Destroy(this.gameObject,1.0f);
	}
	
	void LateUpdate()
	{
		//UpdateArrange();
		foreach (var item in dir)   
        {   
           print(item.Key+":==:"+item.Value);   
        }  
	}
}