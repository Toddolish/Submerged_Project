using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	public float bulletSpeed;
	public int damage;
	public float weaponFireRate;
	#region Destroy
	float destroyTimer;

	#endregion
	private void Start()
	{
	}
	void Update()
	{
		Weapon.MyInstance.fireRate = weaponFireRate;
		BulletMovement();
		Destroy();
	}
	void BulletMovement()
	{
		transform.Translate(new Vector3(bulletSpeed * Time.deltaTime, 0, 0));
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Fish")
		{
			FishAI fishScript = collision.gameObject.GetComponent<FishAI>();
			if (fishScript != null)
			{
				fishScript.DamageFish(damage);
				Destroy(gameObject);
			}
		}
	}

	private void Destroy()
	{
		destroyTimer += Time.deltaTime;
		if (destroyTimer > 10)
		{
			Destroy(gameObject);
		}
	}
}
