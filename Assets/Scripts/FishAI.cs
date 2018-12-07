using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishAI : MonoBehaviour
{
	#region Singleton
	public static FishAI instance;

	public static FishAI MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<FishAI>();
			}

			return instance;
		}
	}
	#endregion
	#region Variables
	[Header("SPEED")]
	[SerializeField]
	public float rotateSpeed;
	[SerializeField]
	private float movementSpeed;
	public float originalSpeed;

	Rigidbody2D myRb;
	Transform myTransform;
	Animator myAnim;

	[Header("FISH TARGET")]
	public Transform target;

	[Header("HEALTH")]
	public float curHealth;
	public float maxHealth = 100;
	Image HP_Bar;
	Text hp_Text;

	[Header("EXPLODE")]
	public GameObject[] meats;
    public GameObject[] randomDrops;
    int Burst = 5;

	[Header("ATTACK")]
	public int attackDamage = 10;

	[Header("BOSS FISH")]
	[Header("Is this fish a boss? set to true!")]
	public bool boss;

	[Header("Boss Variables")]
	public Animator anim;
	public float dashSpeed;
	public bool dashing;
	float dashTimer;

    int randomIndex;
    
	#endregion
	private void Start()
	{
        randomIndex = Random.Range(0, 2);
        //PlayerScript = GameObject.Find("Player").GetComponent<Submarine>();
        target = GameObject.Find("Submarine").GetComponent<Transform>();
		myRb = gameObject.transform.GetComponent<Rigidbody2D>();
		myTransform = gameObject.GetComponent<Transform>();
		myAnim = transform.GetChild(0).GetComponent<Animator>();

		//health
		curHealth = maxHealth;
	}
	private void Update()
	{
		Dashing();
		Boss();
		Explode();
		ForwardMovement();
	}
	void FixedUpdate()
	{
		// If we are dashing disable folowing the player and flipping 
		if (!dashing)
		{
			Distance();
			Flip();
		}
	}
	private float Map(float value, float inMin, float inMax, float outMin, float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	void Distance()
	{
		//myAnim.SetBool("food", false);
		if (target)
		{
			float dist = Vector3.Distance(target.position, myTransform.position);
			//myAnim.SetFloat("distance", dist);
			if (dist > 2)
			{
				FollowTarget();
			}
			if (dist > 11)
			{
				// speed up movement when fish is to far away from player
				//myRb.velocity = (new Vector3(MovementSpeed * 1, 0, 0));
			}
			if (dist < 2)
			{
				if (myAnim != null)
				{
					myAnim.SetTrigger("bite");
				}
			}
		}
	}
	void FollowTarget()
	{
		/////////////////////////////////////////////////////rotate towards player Always;
		Vector3 vectorToTarget = target.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
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
	void ForwardMovement()
	{
		/////////////////////////////////////////////////////movement going Forward
		transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Submarine")
		{
			if (anim != null)
			{
				anim.SetTrigger("bite");
			}
		}
	}
	public void DamageFish(int damage)
	{
		curHealth -= damage;
	}
	public void Explode()
	{
		if (curHealth <= 0)
		{
			// Fish will explode
			// Instanciate all fish parts
			for (int i = 0; i < meats.Length; i++)
			{
				Instantiate(meats[i], transform.position, transform.rotation);
			}
            for (int i = 0; i < randomDrops.Length; i++)
            {
                i = randomIndex;
                Instantiate(randomDrops[randomIndex], transform.position, transform.rotation);
                break;
            }
			Destroy(this.gameObject);
		}
	}
	void Dashing()
	{
		if (dashing)
		{
			dashTimer += Time.deltaTime;
			movementSpeed = dashSpeed;
			if (dashTimer > 2)
			{
				movementSpeed = originalSpeed;
				dashTimer = 0;
				dashing = false;
			}
		}
	}
	void Boss()
	{
		if (boss)
		{
			if (this.transform.position.magnitude < PlayerCTRL.MyInstance.radius)
			{
				dashing = true;
			}
		}
	}
}
