using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCTRL : MonoBehaviour
{
    public Camera cam;

    #region Sub_Vehicle
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
    PlayerSTATS StatsScript;
	
    //text
    //public Text ToggleLights;
    #endregion
    
    void Start()
    {
		Driven = true;
		//other
		//diverTrans = GameObject.Find("Diver").GetComponent<Transform>();
		subCollider = GetComponent<Collider2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        #region SubMarine
        //SubAnim = GameObject.Find("Sub").GetComponent<Animator>();
        sub = GetComponent<GameObject>();
        myrb = gameObject.GetComponent<Rigidbody2D>();
        SubTransform = GetComponent<Transform>();
        SubTrans = this.GetComponentsInChildren<Transform>();
        foreach (Transform t in SubTrans)
        {
            if (t.gameObject.name == "Diver")
            {
                GetComponent<Transform>();
            }
            if (t.gameObject.name == "EnterText")
            {
                GetComponent<GameObject>();
            }
        }
        #endregion

        //Text
        //ToggleLights.GetComponent<Text>();

        //scripts
        thisScript = GetComponent<PlayerCTRL>();
        StatsScript = GetComponent<PlayerSTATS>();
		
    }
    
    void FixedUpdate()
    {
        if (Driven == true)
        {
            myrb.freezeRotation = false;
            //ToggleLights.enabled = true;
            foreach (Transform t in SubTrans)
            {
                if(t.gameObject.name == "Spot")
                    {
                        enabled = true;
                    }
                if (t.gameObject.name == "Glow")
                    {
                        enabled = true;
                    }
                if (t.gameObject.name == "Back")
                    {
                        enabled = true;
                    }
            }
            subCollider.isTrigger = false;
        }
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

    void CastRay()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            //move our player to what we hit.
            //stop focusing any objects;
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
}