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
		if (bag != null)
		{
			bag.MyBagScript.OpenClose();
		}
	}
}
