using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishAI : MonoBehaviour {

    [SerializeField]
    public float RotateSpeed;

    [SerializeField]
    private float MovementSpeed;

    Rigidbody2D myRb;
    Transform myTransform;
    Animator myAnim;
    //Submarine PlayerScript;
    public GameObject Fish;

    //spriteRenderer
    public SpriteRenderer FishSpriteRenderer;
    public SpriteRenderer JawFishSpriteRenderer;
    public SpriteRenderer FinFishSpriteRenderer;
    public SpriteRenderer TailFishSpriteRenderer;
    public SpriteRenderer MidFishSpriteRenderer;
    public SpriteRenderer LightFishSpriteRenderer;

    //sprites
    public Sprite BodyMutatedSprite;
    public Sprite JawMutatedSprite;
    public Sprite FinMutatedSprite;
    public Sprite TailMutatedSprite;
    public Sprite MidMutatedSprite;
    public Sprite LightMutatedSprite;

    public bool FeedingTime;
    public bool FullBelly = false;
    public Transform target;
    public Transform targetBarrel;
    public GameObject BossUI;

    bool followPlayer;
    bool followBarrel;
    public bool DangerZone;

    //DashMove forward
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    //health
    public float CurBoss_Health;
    float MaxBoss_Health;
    Image HP_Bar;
    Text hp_Text;
    public ParticleSystem FishBlood;

    //explode
    public GameObject Meat1,Meat2,Meat3,Meat4,Meat5,Meat6,Meat7;
    int Burst = 5;


    //earthquake
    EarthQuake earthQuakeScript;
    CameraShake cameraShake;

    private void Start()
    {
        DangerZone = false;
        followPlayer = true;
        //PlayerScript = GameObject.Find("Player").GetComponent<Submarine>();

        myRb = gameObject.GetComponent<Rigidbody2D>();
        myTransform = gameObject.GetComponent<Transform>();
        myAnim = gameObject.GetComponent<Animator>();

        dashTime = startDashTime;

        //health
        MaxBoss_Health = 1000;
        CurBoss_Health = MaxBoss_Health;
        HP_Bar = GameObject.Find("1BOSS_bar").GetComponent<Image>();
        hp_Text = GameObject.Find("Boss_Text").GetComponent<Text>();
        FishBlood.Stop();

        earthQuakeScript = GameObject.Find("GameManagerShop").GetComponent<EarthQuake>();
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    void FixedUpdate()
    {

        //activate fish pickup
        if (earthQuakeScript.bossEvent == false)
        {
            if (CurBoss_Health <= 500)
            {
                earthQuakeScript.Earthquake = true;
                if (earthQuakeScript.Earthquake == true)
                {
                    //instanciate forcefield //fish collider to take no damage.
                    //Handheld.Vibrate();
                    earthQuakeScript.RocksFalling();
                    StartCoroutine(cameraShake.Shake(1f, 0.15f));
                    earthQuakeScript.EarthquakeTimer += Time.deltaTime;
                }
            }
        }
        
        //health 
        HP_Bar.fillAmount = Map(CurBoss_Health, 0, MaxBoss_Health, 0, 1);
        hp_Text.text = "     " + CurBoss_Health.ToString() + "/1000";
        DeadFishWalking();
        MajorRadiation();

        Distance();
        // make player speed = fish speed and increase speed slowly overlime with an end result of max speed...
        //or have fish differct speen name but same speed increase as player and sometimes fish goes faster then slower.


    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    void MajorRadiation()
    {
        if(DangerZone == true)
        {

            myTransform.localScale = new Vector3(-2.2f, 2.2f, 0.7f);
            FishSpriteRenderer.sprite = BodyMutatedSprite;
            JawFishSpriteRenderer.sprite = JawMutatedSprite;
            FinFishSpriteRenderer.sprite = FinMutatedSprite;
            TailFishSpriteRenderer.sprite = TailMutatedSprite;
            MidFishSpriteRenderer.sprite = MidMutatedSprite;
            LightFishSpriteRenderer.sprite = LightMutatedSprite;
        }
    }
    void Distance()
    {
        if (followPlayer == true)
        {
            myAnim.SetBool("food", false);
            if (target)
            {
                float dist = Vector3.Distance(target.position, myTransform.position);
                myAnim.SetFloat("distance", dist);
                if (dist > 2)
                {
                    FollowTarget();
                }
                if (FullBelly == false)
                {
                    if (dist < 4)
                    {
                        myAnim.SetBool("food", true);
                        //PlayerScript.feedTimeOuch = true;
                        myRb.velocity = Vector2.right * dashSpeed;
                    }
                }
                if( dist > 11)
                {
                    myRb.velocity = (new Vector3(MovementSpeed * 1, 0, 0));
                }
            }
            //if (PlayerScript.timer > 1)
            //{
                //FullBelly = true;
                //PlayerScript.feedTimeOuch = false;
                //myAnim.SetBool("food", false);
            //}
           // if (PlayerScript.timer > 0.21)
            //{
              //  dashTime = startDashTime;
             //   myRb.velocity = Vector2.zero;
            //    myRb.velocity = (new Vector3(MovementSpeed, 0, 0));
           // }
        }
    }
    void FollowTarget()
    {
        /////////////////////////////////////////////////////rotate towards player Always;
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * RotateSpeed);

        /////////////////////////////////////////////////////movement going Forward
        this.transform.Translate(new Vector3(MovementSpeed * Time.deltaTime, 0, 0));
    }
    public void DamageFish(int damage)
    {
        CurBoss_Health -= damage;
    }
    void DeadFishWalking()
    {
        if (CurBoss_Health < 200)
        {
            if(!FishBlood.isPlaying)
            {
                print("Play Blood");
                FishBlood.Play();
            }
        }

        if (CurBoss_Health < 0)
        {
            Instantiate(Meat1, transform.position, transform.rotation);
            Instantiate(Meat2, transform.position, transform.rotation);
            Instantiate(Meat3, transform.position, transform.rotation);
            Instantiate(Meat4, transform.position, transform.rotation);
            Instantiate(Meat5, transform.position, transform.rotation);
            Instantiate(Meat6, transform.position, transform.rotation);
            Instantiate(Meat7, transform.position, transform.rotation);
            PlayerPrefs.SetInt("bossCounter", 1);
            //boss one has been destroyed
            BossUI.SetActive(false);
            Fish.SetActive(false);
        }
    }
}
