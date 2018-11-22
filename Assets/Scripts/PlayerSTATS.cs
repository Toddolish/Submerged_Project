using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSTATS : MonoBehaviour {
    //health
    public Text Health;
    public Image HpBar;
    public float maxHP = 100; //health for the sub.
    public float curHP = 0;

    //Energy
    public float maxPower = 100; //battery power for the sub.
    public float curPower = 0;

    // Use this for initialization
    void Start () {

        //Health = GameObject.Find("HPlabel").GetComponent<Text>();
        HpBar = GameObject.Find("HealthBar").GetComponent<Image>();
        curHP = maxHP;
        curPower = maxPower;

    }
	// Update is called once per frame
	void Update () {
        HpBar.rectTransform.sizeDelta = new Vector2(curHP, 100f);
        if (curHP < 1)
        {
            curHP = 0;
            SubExplode();
        }
        Health.text = curHP.ToString();
        //HpBar.GetComponent<RectTransform>().localScale
    }
    void SubExplode()
    {
        Destroy(this.gameObject);
        return;
    }
}
