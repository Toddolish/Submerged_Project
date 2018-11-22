using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	public static UIManager MyInstance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<UIManager>();
			}

			return instance;
		}
	}

	[SerializeField]
	private GameObject toolTip;

	private Text toolTipText;

	private void Awake()
	{
		toolTipText = toolTip.GetComponentInChildren<Text>();
	}
	public void UpdateStackSize(IClickable clickable)
	{
		// If the item stach is greater then 1, show the text 
		if (clickable.MyCount > 1)
		{
			clickable.MyStackText.text = clickable.MyCount.ToString();
			clickable.MyStackText.color = Color.white;
			clickable.MyIcon.color = Color.white;
		}
		// Else disable the text by making the color to zero on the RGB,A color
		else
		{
			clickable.MyStackText.color = new Color(0, 0, 0, 0);
			clickable.MyIcon.color = Color.white;
		}
		// If there is no item then change the image and text to zero on RGB,A color
		if (clickable.MyCount == 0)
		{
			clickable.MyIcon.color = new Color(0, 0, 0, 0);
			clickable.MyStackText.color = new Color(0, 0, 0, 0);
		}
	}

	public void ShowToolTip(Vector3 position, IDescribable description)
	{
		toolTip.SetActive(true);
		toolTip.transform.position = position;
		toolTipText.text = description.GetDescription();
	}

	public void HideToolTip()
	{
		toolTip.SetActive(false);
	}
}
