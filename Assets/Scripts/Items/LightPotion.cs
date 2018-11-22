﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Light Potion", order = 1)]
public class LightPotion : Item, IUseable
{

	public int Seconds;
	public void Use()
	{
		//if (Player.MyInstance.curHealth < Player.MyInstance.maxHealth)
		//{
			//Remove();

			//Player.MyInstance.curHealth += health;
		//}
	}

	public override string GetDescription()
	{
		return base.GetDescription() + string.Format("\n <color=#00ff00ff>Use: Lights the way for {0} seconds</color>", Seconds);
	}
}
