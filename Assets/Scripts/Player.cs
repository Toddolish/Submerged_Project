using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;

	public static Player MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<Player>();
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
