using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCTRL : MonoBehaviour
{
	#region Singleton
	public static PlayerCTRL instance;

	public static PlayerCTRL MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<PlayerCTRL>();
			}

			return instance;
		}
	}
	#endregion
	#region Sub_Vehicle
	public Camera cam;
	[Header("SPEED")]
	public float forwardSpeed = 10;
	public float reverseSpeed = 3;
	public float rotateSpeed = 15;
	public float ThrustForce = 5;
	GameObject sub;
	Transform SubTransform;
	Transform[] SubTrans;
	private Rigidbody2D myrb;
	private Vector3 moveInput;
	private Vector3 moveVelocity;
	Collider2D subCollider;
	public bool isGameOver;

	[Header("DetectionRadius")]
	// When boss fish enters this zone, boss will activate his dash attack
	public float radius;

	//Scripts
	PlayerCTRL thisScript;
	PlayerStats StatsScript;

	[Header("Submarine Parts")]
	public GameObject[] subParts;

	[Header("LIGHTS")]
	public GameObject lights;
	public Light areaLight;
	public bool lighting = false;

	[Header("Bubble Trail Particle")]
	public ParticleSystem bubbleTrail;
	public ParticleSystem bubbleTrail2;

	public GameObject spawner;
	public GameObject character;
	#endregion
	#region Special
	// Light Potion
	public float lightTimer = 20;
	public bool specialLights;
	public GameObject lightInterface;
	public Text lightText;

	//Power
	public float powerTimer = 20;
	public bool powerGain;
	public int weaponIndex;
	public GameObject powerBullet, defaultBullet;
	public GameObject powerInterface;
	public Text powerText;

	public GameObject[] weapons;
	#endregion
	void Start()
	{
		subCollider = GetComponent<Collider2D>();
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		#region SubMarine
		//SubAnim = GameObject.Find("Sub").GetComponent<Animator>();
		sub = GetComponent<GameObject>();
		myrb = gameObject.GetComponent<Rigidbody2D>();
		SubTransform = GetComponent<Transform>();
		#endregion
		//scripts
		thisScript = GetComponent<PlayerCTRL>();

	}
	private void Update()
	{
		GameOver();
		SpecialLight();
		PowerGain();
		//SelectWeapon(weaponIndex);
		if (Input.GetKeyDown(KeyCode.E))
		{
			//Destroy fish
			GameObject[] fish = GameObject.FindGameObjectsWithTag("Fish");
			for (int i = 0; i < fish.Length; i++)
			{
				Destroy(fish[i]);
			}
			// Switch to character

			character.SetActive(true);
			PlayerStats.MyInstance.enabled = false;
			Weapon.MyInstance.enabled = false;
			// Change camera target to submarine
			CamFollow.MyInstance.followingSub = false;
			CamFollow.MyInstance.followingChar = true;
			// Start the waves
			spawner.SetActive(false);
			// Set this gameobject active to false
			thisScript.enabled = false;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		#region AddItems
		// Pickup items and add them to inventory
		if (collision.tag == "hpp")
		{
			Destroy(collision.gameObject);
			// spawn item of tag into bag
			Inventory.MyInstance.AddHealthPotion();
		}
		if (collision.tag == "ss")
		{
			Destroy(collision.gameObject);
			// spawn item of tag into bag
			Inventory.MyInstance.AddSoftScales();
		}
		if (collision.tag == "sm")
		{
			Destroy(collision.gameObject);
			// spawn item of tag into bag
			Inventory.MyInstance.AddSoftMeat();
		}
		if (collision.tag == "lp")
		{
			Destroy(collision.gameObject);
			// spawn item of tag into bag
			Inventory.MyInstance.AddLightPotion();
		}
		if (collision.tag == "db")
		{
			Destroy(collision.gameObject);
			// spawn item of tag into bag
			Inventory.MyInstance.AddLargeBag();
		}
		if (collision.tag == "pp")
		{
			Destroy(collision.gameObject);
			// spawn item of tag into bag
			Inventory.MyInstance.AddPowerPotion();
		}
		#endregion
	}
	void FixedUpdate()
	{
		//ToggleLights.enabled = true;
		subCollider.isTrigger = false;
		MouseRotationMovement();
		Flip();
	}
	void Flip()
	{
		if (Mathf.Abs(transform.localScale.y - (-1)) < 0.01f)
		{
			if (!(Vector3.Dot(transform.up, Vector3.down) < 0.001f)) return;
			if (Vector3.Dot(transform.right, Vector3.down) > 0.825f)
			{
				transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
			}
			else if (Vector3.Dot(-transform.right, Vector3.down) > 0.05f)
			{
				transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
			}
		}
		else
		{
			if (!(Vector3.Dot(-transform.up, Vector3.down) < 0.001f)) return;
			if (Vector3.Dot(-transform.right, Vector3.down) > 0.825f)
			{
				transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
			}
			else if (Vector3.Dot(transform.right, Vector3.down) > 0.05f)
			{
				transform.localScale = new Vector3(transform.localScale.x, -1, transform.localScale.z);
			}
		}
	}
	void MouseRotationMovement()
	{
		if (Input.GetKey(KeyCode.W))
		{
			//SubAnim.Play("blades_moving");
			myrb.AddRelativeForce(new Vector3(forwardSpeed * Time.deltaTime, 0, 0));
			bubbleTrail.Play();
			bubbleTrail2.Play();
		}
		else
		{
			bubbleTrail.Stop();
			bubbleTrail2.Stop();
		}
		if (Input.GetKey(KeyCode.S))
		{
			//SubAnim.Play("blades_moving");
			myrb.AddRelativeForce(new Vector3(-reverseSpeed * Time.deltaTime, 0, 0));
			bubbleTrail.Play();
			bubbleTrail2.Play();
		}
		Vector3 RayPos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - RayPos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
	}

	public void SpecialLight()
	{
		if (specialLights)
		{
			areaLight.range = 50;
			lightTimer -= Time.deltaTime;
			lightInterface.SetActive(true);
			lightText.text = lightTimer.ToString("F1");

			if (lightTimer <= 0)
			{
				specialLights = false;
				lightTimer = 20;
				areaLight.range = 8;
				lightInterface.SetActive(false);
			}
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	public void PowerGain()
	{
		if (powerGain)
		{
			//do something
			powerInterface.SetActive(true);
			powerText.text = powerTimer.ToString("F1");
			Weapon.MyInstance.Bullet = powerBullet;
			powerTimer -= Time.deltaTime;
			if (powerTimer <= 0)
			{
				powerGain = false;
				powerTimer = 20;
				Weapon.MyInstance.Bullet = defaultBullet;
				powerInterface.SetActive(false);
			}
		}
	}
	void SelectWeapon(int selectedIndex)
	{
		for (int i = 0; i < weapons.Length; i++)
		{
			weapons[i].SetActive(false);
			if (i == selectedIndex)
			{
				weapons[i].SetActive(true);
			}
		}
	}
	void GameOver()
	{
		if (PlayerStats.MyInstance.curHealth <= 0)
		{
			PlayerStats.MyInstance.curHealth = 0;
			isGameOver = true;
		}
		if(isGameOver)
		{
			for (int i = 0; i < subParts.Length; i++)
			{
				subParts[i].GetComponent<Rigidbody2D>().isKinematic = false;
				subParts[i].GetComponent<PolygonCollider2D>().enabled = true;
				subParts[i].transform.parent = null;
				thisScript.enabled = false;
				Weapon.MyInstance.enabled = false;
				lights.SetActive(false);
			}
		}
	}
}