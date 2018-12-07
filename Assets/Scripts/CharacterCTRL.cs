using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCTRL : MonoBehaviour
{
	public float moveSpeed;
	public GameObject spawner;
	Animator anim;
	Rigidbody2D rigid;
    // Press [E] text
    public Text enter;

	private void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        enter = GameObject.Find("PressE_Text").GetComponent<Text>();

    }
	void Update()
	{
		float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		rigid.velocity = Vector3.right * move;
		anim.SetFloat("walking", move);

		if (Input.GetKeyDown(KeyCode.E))
		{
			// Switch to submarine
			PlayerCTRL.MyInstance.enabled = true;
			PlayerStats.MyInstance.enabled = true;
			Weapon.MyInstance.enabled = true;
			// Change camera target to submarine
			CamFollow.MyInstance.followingSub = true;
			// Start the waves
			spawner.SetActive(true);
			// Set this gameobject active to false
			this.gameObject.SetActive(false);
            // Disable enter text
            enter.enabled = false;
        }

		
	}
}
