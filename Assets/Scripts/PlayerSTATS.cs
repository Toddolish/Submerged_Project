using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	#region Singleton
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
	#endregion
	public float curHealth;
	public float maxHealth = 100;
	public Image hp_Bar;
	void Start()
	{
		curHealth = maxHealth;
		hp_Bar = GameObject.Find("HpBar_Fill").GetComponent<Image>();
	}

	void Update()
	{
		hp_Bar.fillAmount = curHealth / 100;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			curHealth -= 10;
		}
	}
	public void TakeDamage(int damage)
	{
		curHealth -= damage;
	}
}
