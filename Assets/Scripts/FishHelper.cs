using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHelper : MonoBehaviour
{
	public void AttackPlayer()
    {
		PlayerStats.MyInstance.TakeDamage(FishAI.MyInstance.attackDamage);
    }
}
