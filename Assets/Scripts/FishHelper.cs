using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHelper : MonoBehaviour
{
    FishAI fish;

    private void Start()
    {
        fish = transform.GetComponentInParent<FishAI>();
    }
    public void AttackPlayer()
    {
        PlayerStats.MyInstance.TakeDamage(fish.attackDamage);
    }
}
