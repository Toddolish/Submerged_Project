using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
	#region Singleton
	public static CamFollow instance;

	public static CamFollow MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<CamFollow>();
			}

			return instance;
		}
	}
	#endregion
	#region Variables
	[Header("TARGETS")]
	[Header("What to follow")]
	public GameObject subTarget;
	public GameObject charTarget;

	public Vector3 OffsetSub;

	Camera cam;

	public bool followingChar;
	public bool followingSub;

	public Transform anchorPoint;
	public GameObject submarine;
	public float anchorSpeed = 1;
	#endregion

	void Start()
	{
		followingChar = true;
		cam = this.gameObject.GetComponent<Camera>();
		OffsetSub = transform.position - subTarget.transform.position;
	}
	private void Update()
	{
		GameOver();
	}
	void LateUpdate()
	{
		//FollowCharacter();
		FollowSubmarine();
		if (!followingSub)
		{
			submarine.transform.position = Vector3.MoveTowards(submarine.transform.position, anchorPoint.transform.position, anchorSpeed * Time.deltaTime);
			submarine.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
		}
	}
	void FollowCharacter()
	{
		if (followingChar)
		{
			if (charTarget != null)
			{
				// sub anchor point is active
				transform.position = charTarget.transform.position + new Vector3(-1, -0.69f, -10);
			}
		}
	}
	void FollowSubmarine()
	{
		if (followingSub)
		{
			if (subTarget != null)
			{
				transform.position = subTarget.transform.position + new Vector3(-1, -0.69f, -20);
			}
		}
	}
	void GameOver()
	{
		if (PlayerCTRL.MyInstance.isGameOver)
		{
			subTarget = null;
		}
	}
}
