using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Items to Spawn
    public GameObject healthPotion;
    #endregion
    #region Singleton
    public static Inventory instance;

	public static Inventory MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<Inventory>();
			}

			return instance;
		}
	}
    #endregion
    public int MyTotalSlotCount
	{
		get
		{
			int count = 0;

			foreach (Bag bag in bags)
			{
				count += bag.MyBagScript.MySlots.Count;
			}

			return count;
		}
	}

	public int MyFullSlotCount
	{
		get
		{
			return MyTotalSlotCount - MyTotalSlotCount;
		}
	}

	private SlotScript fromSlot;

	private List<Bag> bags = new List<Bag>();

	[SerializeField]
	private BagButton[] bagButtons;

	// For Debugging
	[SerializeField]
	private Item[] items;

	public bool CanAddBag
	{
		get { return bags.Count < 5; }
	}

	public int MyEmptySlotCount
	{
		get
		{
			int count = 0;

			foreach (Bag bag in bags)
			{
				count += bag.MyBagScript.MyEmptySlotCount;
			}

			return count;
		}
	}

	public SlotScript FromSlot
	{
		get
		{
			return fromSlot;
		}

		set
		{
			fromSlot = value;
			if (value != null)
			{
				fromSlot.MyIcon.color = Color.grey;
			}
		}
	}

	private void Awake()
	{
		Bag bag = (Bag)Instantiate(items[0]);
		bag.Initialize(16);
		bag.Use();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			Inventory.MyInstance.OpenClose();
		}
	}

	public void AddBag(Bag bag)
	{
		foreach (BagButton bagButton in bagButtons)
		{
			if (bagButton.MyBag == null)
			{
				bagButton.MyBag = bag;
				bags.Add(bag);
				bag.MyBagButton = bagButton;
				break; // Only want to do this for one bag not for all the others
			}
		}
	}

	public void AddBag(Bag bag, BagButton bagButton)
	{
		bags.Add(bag);
		bagButton.MyBag = bag;
	}

	public void RemoveBag(Bag bag)
	{
		bags.Remove(bag);
		Destroy(bag.MyBagScript.gameObject);
	}

	public void SwapBags(Bag oldBag, Bag newBag)
	{
		int newSlotCount = (MyTotalSlotCount - oldBag.Slots) + newBag.Slots;

		if (newSlotCount - MyFullSlotCount >= 0)
		{
			// Do Swap
			List<Item> bagItems = oldBag.MyBagScript.GetItems();

			RemoveBag(oldBag);

			newBag.MyBagButton = oldBag.MyBagButton;

			newBag.Use();

			foreach (Item item in bagItems)
			{
				if (item != newBag) // To make sure there is no Dulpicates
				{
					AddItem(item);
				}
			}

			AddItem(oldBag);

			HandScript.MyInstance.Drop();

			MyInstance.fromSlot = null;
		}
	}

	public void AddItem(Item item)
	{
		if (item.MyStackSize > 0)
		{
			if (PlaceInStack(item))
			{
				return;
			}
		}

		PlaceInEmpty(item);
	}

	private void PlaceInEmpty(Item item)
	{
		foreach (Bag bag in bags)
		{
			if (bag.MyBagScript.AddItem(item))
			{
				return;
			}
		}
	}

	private bool PlaceInStack(Item item)
	{
		foreach (Bag bag in bags)
		{
			foreach (SlotScript slots in bag.MyBagScript.MySlots)
			{
				if (slots.StackItem(item))
				{
					return true;
				}
			}
		}

		return false;
	}

	public void OpenClose()
	{
		bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);
		// If closed bag == true, then open all closed bags
		// If closed bag == false, then close all open bags
		foreach (Bag bag in bags)
		{
			if (bag.MyBagScript.IsOpen != closedBag)
			{
				bag.MyBagScript.OpenClose();
			}
		}
	}
    #region AddItems
    public void AddHealthPotion()
    {
        HealthPotion potion = (HealthPotion)Instantiate(items[1]);
        AddItem(potion);
    }
    public void AddLightPotion()
    {
        LightPotion potion = (LightPotion)Instantiate(items[2]);
        AddItem(potion);
    }
    public void AddLargeBag()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(16);
        AddItem(bag);
    }
    public void AddSmallBag()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(8);
        AddItem(bag);
    }
    public void AddSoftScales()
    {
        Misc softScales = (Misc)Instantiate(items[3]);
        AddItem(softScales);
    }
    public void AddSoftMeat()
    {
        Meat softMeat = (Meat)Instantiate(items[4]);
        AddItem(softMeat);
    }
	public void AddPowerPotion()
	{
		PowerPotion powerPotion = (PowerPotion)Instantiate(items[5]);
		AddItem(powerPotion);
	}
	#endregion

}
