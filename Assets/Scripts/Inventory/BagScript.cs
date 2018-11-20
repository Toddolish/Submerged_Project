using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
	[SerializeField]
	private GameObject slotPrefab;

	private CanvasGroup canvasGroup;

	private List<SlotScript> slots = new List<SlotScript>();

	public bool IsOpen
	{
		get { return canvasGroup.alpha > 0; }
	}

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void AddSlots(int slotCount)
	{
		for (int i = 0; i < slotCount; i++)
		{
			SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
			slots.Add(slot);
		}
	}

	public bool AddItem(Item item)
	{
		// Check all the slots to see if they are empty or full
		foreach (SlotScript slot in slots)
		{
			// If slots are empty add and item
			if (slot.IsEmpty)
			{
				slot.AddItem(item);

				return true;
			}
		}
		// If slots are full do not add and item and check the other bags
		return false;
	}

	public void OpenClose()
	{
		canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;

		canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
	}
}
