using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
	// Stacking first in first out
	private ObservableStack<Item> items = new ObservableStack<Item>();

	[SerializeField]
	private Image icon;
	[SerializeField]
	private Text stackSize;

	public BagScript MyBag { get; set; }

	public bool IsEmpty
	{
		get
		{
			// If count is zero this slot is empty
			return MyItems.Count == 0; // items is the stack
		}
	}

	public bool IsFull
	{
		get
		{
			if (IsEmpty || MyCount < MyItem.MyStackSize)
			{
				return false;
			}

			return true;
		}	
	}

	public Item MyItem
	{
		get
		{
			if (!IsEmpty)
			{
				return MyItems.Peek();
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
		get  {return MyItems.Count; }
	}

	public Text MyStackText
	{
		get
		{
			return stackSize;
		}
	}

	public ObservableStack<Item> MyItems
	{
		get
		{
			return items;
		}
	}

	public void Awake()
	{
		MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
		MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
		MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
	}

	public bool AddItem(Item item)
	{
		// Add an item to stack
		MyItems.Push(item);
		// Change icon to set icon
		icon.sprite = item.MyIcon;
		// Charge icon colour to white
		icon.color = Color.white;
		item.MySlot = this;
		return true;
	}

	public bool AddItems(ObservableStack<Item> newItems)
	{
		if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
		{
			int count = newItems.Count;

			for (int i = 0; i < count; i++)
			{
				if (IsFull)
				{
					return false;
				}

				AddItem(newItems.Pop());
			}
			return true;
		}

		return false;
	}

	public void RemoveItem(Item item)
	{
		// If slot is not empty
		if (!IsEmpty)
		{
			// Remove the item from the stack
			MyItems.Pop();
			// Update the Stack
			
		}
	}

	public void Clear()
	{
		if (MyItems.Count > 0)
		{
			MyItems.Clear();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (Inventory.MyInstance.FromSlot == null && !IsEmpty)
			{
				if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
				{
					if (MyItem is Bag)
					{
						Inventory.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
					}
				}
				else
				{
					HandScript.MyInstance.TakeMoveable(MyItem as IMoveable); // If you don't have something to move
					Inventory.MyInstance.FromSlot = this;
				}
			}
			else if (Inventory.MyInstance.FromSlot == null && IsEmpty && (HandScript.MyInstance.MyMoveable is Bag))
			{
				// Dequips bag from inventory
				Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

				if (bag.MyBagScript != MyBag && Inventory.MyInstance.MyEmptySlotCount - bag.Slots > 0)
				{
					AddItem(bag);
					bag.MyBagButton.RemoveBag();
					HandScript.MyInstance.Drop();
				}
			}
			else if (Inventory.MyInstance.FromSlot != null) // If you have something to move
			{
				if (PutItemBack() || MergeItems(Inventory.MyInstance.FromSlot) || SwapItems(Inventory.MyInstance.FromSlot) || AddItems(Inventory.MyInstance.FromSlot.MyItems))
				{
					HandScript.MyInstance.Drop();
					Inventory.MyInstance.FromSlot = null;
				}
			}
		}
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

	public bool StackItem(Item item)
	{
		if (!IsEmpty && item.name == MyItem.name && MyItems.Count < item.MyStackSize)
		{
			MyItems.Push(item);
			item.MySlot = this;
			return true;
		}

		return false;
	}

	private bool PutItemBack()
	{
		if (Inventory.MyInstance.FromSlot == this)
		{
			Inventory.MyInstance.FromSlot.MyIcon.color = Color.white;
			return true;
		}

		return false;
	}

	private bool SwapItems(SlotScript from)
	{
		if (IsEmpty)
		{
			return false;
		}
		if (from.MyItem.GetType() != MyItem.GetType() // If the item i am moveing is different to the item i am clicking on then swap 
			|| from.MyCount+MyCount > MyItem.MyStackSize) // OR the from slot count plus the slot count is greater then the total stack size of the items
		{
			// Copy all the items to swap from A
			ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);

			// Clear Slot a
			from.MyItems.Clear();
			// All items from slot b and copy them into A
			from.AddItems(MyItems);

			// Clear B
			MyItems.Clear();
			// move the items from ACopy to B
			AddItems(tmpFrom);
		}

		return false;
	}
	private bool MergeItems(SlotScript from)
	{
		if (IsEmpty)
		{
			return false;
		}
		if (from.MyItem.GetType() ==  MyItem.GetType() && !IsFull)
		{
			// How many free slots available in the stack
			int free = MyItem.MyStackSize - MyCount;

			for (int i = 0; i < free; i++)
			{
				AddItem(from.MyItems.Pop());
			}
			return true;
		}
		return false;
	}

	private void UpdateSlot()
	{
		UIManager.MyInstance.UpdateStackSize(this);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// Show toolTip
		if (!IsEmpty)
		{
			UIManager.MyInstance.ShowToolTip(transform.position, MyItem);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// Hide toolTip
		UIManager.MyInstance.HideToolTip();
	}
}
