//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// A UI script that keeps an eye on the slot in a storage container.
/// </summary>

[AddComponentMenu("NGUI/Game/UI Storage Slot")]
public class UIStorageSlotMy : UIItemSlot
{
	public UIItemStorageMy storage;
	public int slot = 0;

	override protected InvGameItem observedItem
	{
		get
		{
			return (storage != null) ? storage.GetItem(slot) : null;
		}
	}

	/// <summary>
	/// Replace the observed item with the specified value. Should return the item that was replaced.
	/// </summary>

	override protected InvGameItem Replace (InvGameItem item)
	{
		return (storage != null) ? storage.Replace(slot, item) : item;
	}
	
	protected void OnClick ()
	{
		
		
		Debug.Log("dddd");
		
		/*if (mDraggedItem != null)
		{
			OnDrop(null);
		}
		else if (mItem != null)
		{
			mDraggedItem = Replace(null);
			if (mDraggedItem != null) NGUITools.PlaySound(grabSound);
			UpdateCursor();
		}*/
	}
}