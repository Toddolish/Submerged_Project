using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishAI : MonoBehaviour
{

    [SerializeField]
    public float RotateSpeed;

    [SerializeField]
    private float MovementSpeed;

    Rigidbody2D myRb;
    Transform myTransform;
    Animator myAnim;
    public GameObject Fish;

    public Transform target;


    //health
    public float curHealth;
    float maxHealth = 100;
    Image HP_Bar;
    Text hp_Text;

    //explode
    public GameObject[] Meats;
    int Burst = 5;

    //Attack
    public int attackDamage = 10;

    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Y))
        {
            curHealth = 0;
        }
        Explode();
        ForwardMovement();
    }
    void FixedUpdate()
    {
        Distance();
        Flip();
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
                myAnim.SetTrigger("bite");
            }
        }
    }
    void FollowTarget()
    {
        /////////////////////////////////////////////////////rotate towards player Always;
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotateSpeed);
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
        transform.Translate(Vector3.right * MovementSpeed * Time.deltaTime);
    }
    public void DamageFish(int damage)
    {
        curHealth -= damage;
    }
    public void Explode()
    {
        if(curHealth <= 0)
        {
            // Fish will explode
            // Instanciate all fish parts
            for (int i = 0; i < Meats.Length; i++)
            {
                Instantiate(Meats[i], transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

}
