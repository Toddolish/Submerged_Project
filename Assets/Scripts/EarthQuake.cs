using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthQuake : MonoBehaviour {
    float ThrowForce = 6f;
    
    //boss fish
    FishAI Boss1_fishScript;
    public CameraShake cameraShake;

    //earthQuake
    public bool Earthquake;
    public float EarthquakeTimer;
    int randomLength;
    int randonSpawn;
    public Transform CameraPos;
    public GameObject[] Rocks;
    public GameObject Barrel;
    float RockTimer;
    int Count = 0;
    public bool bossEvent;

    void Start()
    {
        bossEvent = false;
        EarthquakeTimer = 0;
        Earthquake = false;
        
    }

    void Update()
    {
        if (EarthquakeTimer > 6)
        {
            //remove forcefeild //fish collider can now take damage.
            bossEvent = true;
            Earthquake = false;
            EarthquakeTimer = 0;
        }
        
    }
    public void RocksFalling()
    {
        RockTimer += Time.deltaTime;
        randomLength = Random.Range(0, Rocks.Length);
        randonSpawn = Random.Range(-5, 25);
        if (RockTimer > 0.2)
        {
            Instantiate(Rocks[randomLength], new Vector3(CameraPos.position.x + randonSpawn, CameraPos.position.y + 10, -8), transform.rotation);
            RockTimer = 0;
        }
        if (Count < 1)
        {
            Instantiate(Barrel, new Vector3(CameraPos.position.x + 10, CameraPos.position.y + 12, -8), transform.rotation);
            Count = Count + 2;
        }
    }
}
