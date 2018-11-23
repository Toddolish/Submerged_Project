using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats instance;

	public static PlayerStats MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<PlayerStats>();
			}

			return instance;
		}
	}

	public float curHealth;
	public float maxHealth = 100;
	void Start()
	{
		curHealth = maxHealth;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			curHealth -= 10;
		}
	}
}
