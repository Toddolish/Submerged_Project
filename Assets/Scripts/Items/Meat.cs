using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Meat", menuName = "Items/Meat", order = 1)]
public class Meat : Item, IUseable
{
    public int health;
    public int food;
    public void Use()
    {
        if (PlayerStats.MyInstance.curHealth < PlayerStats.MyInstance.maxHealth)
        {
            Remove();

            PlayerStats.MyInstance.curHealth += health;
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n <color=#00ff00ff>Use: Restores {0} health</color>", health) + 
        string.Format("\n <color=#00ff00ff>Use: Restores {0} hunger</color>", food);
    }
}
