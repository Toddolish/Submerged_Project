using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerPotion", menuName = "Items/Power Potion", order = 1)]
public class PowerPotion : Item, IUseable
{
	public int Seconds;
	public void Use()
	{
		if (PlayerCTRL.MyInstance.powerGain == false)
		{
			Remove();

			PlayerCTRL.MyInstance.powerGain = true;
		}
	}
	public override string GetDescription()
	{
		return base.GetDescription() + string.Format("\n <color=#00ff00ff>Use: Gain a powerful weapon for {0} seconds</color>", Seconds);
	}
}
