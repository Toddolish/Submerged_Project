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
    public bool flip;
    public bool Driven;
    Collider2D subCollider;

    //lights
    public GameObject lights;
    public bool lighting = false;

    //particles, camera, speeds
    public ParticleSystem bubbleTrail;
    public ParticleSystem bubbleTrail2;

    //Scripts
    PlayerCTRL thisScript;
    PlayerStats StatsScript;

    //text
    public Light areaLight;
    #endregion

    #region Special
    // Light Potion
    float lightTimer = 10;
    public bool specialLights;
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
        SpecialLight();
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

            if (lightTimer <= 0)
            {
                specialLights = false;
                lightTimer = 30;
            }
        }
        else
        {
            areaLight.range = 8;
        }
    }
}