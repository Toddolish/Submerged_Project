using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {

    Rigidbody2D rb;
    float dirX;
    float dirY;
    float torque;

    #region Destroy
    float destroyTimer;
	public float destroyTime = 10f;
    #endregion

    // Use this for initialization
    void Start () {
        dirX = Random.Range(-5, 5);
        dirY = Random.Range(-5, 6);
        torque = Random.Range(-5, 10);
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(dirX, dirY), ForceMode2D.Impulse);
        rb.AddTorque(torque, ForceMode2D.Force);

    }
	
	// Update is called once per frame
	void Update ()
    {
        destroyTimer += Time.deltaTime;

        if(destroyTimer > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
