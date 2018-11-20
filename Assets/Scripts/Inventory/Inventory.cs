using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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

	private void Awake()
	{
		Bag bag = (Bag)Instantiate(items[0]);
		bag.Initialize(16);
		bag.Use();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			Bag bag = (Bag)Instantiate(items[0]);
			bag.Initialize(16);
			bag.Use();
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			Inventory.MyInstance.OpenClose();
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			Bag bag = (Bag)Instantiate(items[0]);
			bag.Initialize(16);
			AddItem(bag);
		}
		if (Input.GetKeyDown(KeyCode.L))
		{
			HealthPotion potion = (HealthPotion)Instantiate(items[1]);
			AddItem(potion);
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
				break; // Only want to do this for one bag not for all the others
			}
		}
	}

	public void AddItem(Item item)
	{
		foreach (Bag bag in bags)
		{ 
			// Check that it can add a bag
			if (bag.MyBagScript.AddItem(item))
			{
				return;
			}
		}
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

}
