using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable
{
	// Stacking first in first out
	private Stack<Item> items = new Stack<Item>();

	[SerializeField]
	private Image icon;

	public bool IsEmpty
	{
		get
		{
			// If count is zero this slot is empty
			return items.Count == 0; // items is the stack
		}
	}

	public Item MyItem
	{
		get
		{
			if (!IsEmpty)
			{
				return items.Peek();
			}
			return null;
		}
	}

	public Image MyIcon
	{
		get
		{
			return icon;
		}

		set
		{
			icon = value;
		}
	}

	public int MyCount
	{
		get  {return items.Count; }
	}

	public bool AddItem(Item item)
	{
		// Add an item to stack
		items.Push(item);
		// Change icon to set icon
		icon.sprite = item.MyIcon;
		// Charge icon colour to white
		icon.color = Color.white;
		item.MySlot = this;
		return true;
	}

	public void RemoveItem(Item item)
	{
		// If slot is not empty
		if (!IsEmpty)
		{
			// Remove the item from the stack
			items.Pop();
			//UIManager.MyInstance.UpdateStackSize(this);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			UseItem();
		}
	}

	public void UseItem()
	{
		if (MyItem is IUseable)
		{
			(MyItem as IUseable).Use();
		}
	}
}
