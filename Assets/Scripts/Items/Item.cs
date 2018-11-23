using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality {Common, Uncommon, Rare, Epic }

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
	[SerializeField]
	private Sprite icon;

	[SerializeField]
	private int stackSize;

	[SerializeField]
	private string title;

	[SerializeField]
	private Quality quality;

	private SlotScript slot;

	public Sprite MyIcon 
	{
		get
		{
			return icon;
		}
	}

	public int MyStackSize
	{
		get
		{
			return stackSize;
		}
	}

	public SlotScript MySlot 
	{
		get
		{
			return slot;
		}

		set
		{
			slot = value;
		}
	}

	public virtual string GetDescription()
	{
		string color = string.Empty;

		switch (quality)
		{
			case Quality.Common:
				color = "#d6d6d7";
				break;

			case Quality.Uncommon:
				color = "#00d605";
				break;

			case Quality.Rare:
				color = "#00d8ff";
				break;

			case Quality.Epic:
				color = "#800080ff";
				break;
		}

		return string.Format("<color={0}> {1}</color>", color, title);
	}

	public void Remove()
	{
		if (MySlot != null)
		{
			MySlot.RemoveItem(this);
		}
	}
}
