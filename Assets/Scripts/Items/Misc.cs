using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Misc", menuName = "Items/Misc", order = 1)]
public class Misc : Item, IUseable
{
    public void Use()
    {
       
    }
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n <color=#ffffff>Used: In Crafting these soft scales\n can craft fishy things</color>");
    }
}
