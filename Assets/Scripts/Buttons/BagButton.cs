using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagButton : MonoBehaviour, IPointerClickHandler
{

	private Bag bag;

	[SerializeField]
	private Sprite full, empty;

	public Bag MyBag
	{
		get
		{
			return bag;
		}

		set
		{
			// if image not null change the sprite to full
			if (value != null)
			{
				GetComponent<Image>().sprite = full;
			}
			// else the sprite will stay empty
			else
			{
				GetComponent<Image>().sprite = empty;
			}
			bag = value;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (Inventory.MyInstance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
			{
				if (MyBag != null)
				{
					Inventory.MyInstance.SwapBags(MyBag, HandScript.MyInstance.MyMoveable as Bag);
				}
				else
				{
					Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
					tmp.MyBagButton = this;
					tmp.Use();
					MyBag = tmp;
					HandScript.MyInstance.Drop();
					Inventory.MyInstance.FromSlot = null;
				}
			}
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				HandScript.MyInstance.TakeMoveable(MyBag);
			}
			else if (bag != null)
			{
				bag.MyBagScript.OpenClose();
			}
		}
	}

	public void RemoveBag()
	{
		Inventory.MyInstance.RemoveBag(MyBag);
		MyBag.MyBagButton = null;

		foreach (Item item in MyBag.MyBagScript.GetItems())
		{
			Inventory.MyInstance.AddItem(item);
		}

		MyBag = null;
	}
}
