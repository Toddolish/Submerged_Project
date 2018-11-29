using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
	#region Singleton
	public static Weapon instance;

	public static Weapon MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<Weapon>();
			}

			return instance;
		}
	}
	#endregion
	public GameObject Bullet;
    public Transform spawnPoint;
    public float rotateSpeed;
	public GameObject flash;

	public float fireRate =  15f;
	private float nextTimeToFire = 0f;

	float flashTimer;
	bool startFlash;
    
    void Update()
    {
        Shoot();
		FlashTimer();
		Vector3 RayPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - RayPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
    void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && Time.time >= nextTimeToFire)
        {
			Effect();
			nextTimeToFire = Time.time + 1f / fireRate;
            Instantiate(Bullet, spawnPoint.position, spawnPoint.rotation);
        }
    }
	void Effect()
    {
		flash.SetActive(true);
        float size = Random.Range(0.1f, 0.3f);
        flash.GetComponent<Transform>().localScale = new Vector3(size, size, size);
		startFlash = true;
	}
	void FlashTimer()
	{
		if (startFlash)
		{
			flashTimer += Time.deltaTime;

			if (flashTimer > 0.05f)
			{
				flash.SetActive(false);
				flashTimer = 0;
				startFlash = false;
			}
		}
	}
}
