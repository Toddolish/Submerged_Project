using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour
{
	public static HandScript instance;

	public static HandScript MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<HandScript>();
			}

			return instance;
		}
	}


	public IMoveable MyMoveable { get; set; }

	private Image icon;

	[SerializeField]
	private Vector3 offset;

	private void Start()
	{
		icon = GetComponent<Image>();
	}
	private void Update()
	{
		icon.transform.position = Input.mousePosition + offset;

		DeleteItem();
	}

	public void TakeMoveable(IMoveable moveable)
	{
		this.MyMoveable = moveable;
		icon.sprite = moveable.MyIcon;
		icon.color = Color.white;
	}

	public void Drop()
	{
		MyMoveable = null;
		icon.color = new Color(0, 0, 0, 0);
	}

	private void DeleteItem()
	{
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
		{
			if (MyMoveable is Item && Inventory.MyInstance.FromSlot != null)
			{
				(MyMoveable as Item).MySlot.Clear();
			}

			Drop();

            #region SpawnItemPrefabs
            // Once item is deleted from inventory spawn the prefab in the world
            //if (MyMoveable.MyIcon.name == "hpp")
            //{
            //    Instantiate(Inventory.MyInstance.healthPotion, transform.position, transform.rotation);
            //}
            #endregion

            Inventory.MyInstance.FromSlot = null;
		}
	}
}
