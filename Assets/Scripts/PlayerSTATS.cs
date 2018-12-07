using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    #region Singleton
    public static PlayerStats instance;

    public static PlayerStats MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerStats>();
            }

            return instance;
        }
    }
    #endregion
    public CharacterClass playerClass;
    #region Health
    [Header("HEALTH")]
    public float curHealth;
    public float maxHealth = 100;
    public Image hp_Bar;
    #endregion
    #region Exp
    [Header("EXP")]
    public float curExp;
    public float maxExp = 60;
    public Image exp_Bar;
    public int level = 0;
    public Text levelText;
    #endregion
    #region Stats
    [Header("STATS")]
    public bool showStatWindow;
    public Vector2 scrollPos;
    public int points = 0;
    public string[] statArray = new string[6];
    public int[] tempStats = new int[6];

    //level up button
    public GameObject LevelButton;

    //Affects attack damage increased health
    public int Strength;
    //Affects Stamina
    public int Constitution;
    //Affects mana and mana regeneration
    public int Intelligence;
    //increases magical accuracy
    public int Wisdom;
    //AffectsCritical hit damage
    public int Dexterity;
    //Affects Critical hit Damage and the ability to evade more
    public int Charisma;
    #endregion
    void Start()
    {
        curHealth = maxHealth;
        curExp = 0;
        level = 1;
        hp_Bar = GameObject.Find("HpBar_Fill").GetComponent<Image>();
        exp_Bar = GameObject.Find("ExpFill").GetComponent<Image>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        #region Stats
        //stats
        Strength = PlayerPrefs.GetInt("Strength", 10);
        Dexterity = PlayerPrefs.GetInt("Dexterity", 10);
        Constitution = PlayerPrefs.GetInt("Constitution", 10);
        Wisdom = PlayerPrefs.GetInt("Wisdom", 10);
        Intelligence = PlayerPrefs.GetInt("Intelligence", 10);
        Charisma = PlayerPrefs.GetInt("Charisma", 10);

        maxHealth += Strength * 1f;

        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        #endregion
    }

    void Update()
    {
        #region Health
        hp_Bar.fillAmount = curHealth / 100;
        #endregion
        #region Exp
        levelText.text = level.ToString();
        exp_Bar.fillAmount = curExp / maxExp;
        if (curExp >= maxExp)
        {
            //Enable levelUp button
            LevelButton.SetActive(true);

            //then the current experience is equal to our experience minus the maximum amount of experience
            curExp -= maxExp;

            //our level goes up by one
            level++;
            points += 3;
            curHealth = maxHealth;

            //the maximum amount of experience is increased by 50
            maxExp = maxExp + 20;
        }
        #endregion
    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
    #region OnGUI
    private void OnGUI()
    {
        // create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        #region Stats
        if (showStatWindow)
        {
            Cursor.visible = true;
            GUI.Box(new Rect(3.75f * scrW, 2f * scrH, 2f * scrW, 0.5f * scrH), "Points: " + points);
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + 0 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[0] + ": " + (Strength + tempStats[0]));
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + 1 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[1] + ": " + (Dexterity + tempStats[1]));
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + 2 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[2] + ": " + (Constitution + tempStats[2]));
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + 3 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[3] + ": " + (Wisdom + tempStats[3]));
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + 4 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[4] + ": " + (Intelligence + tempStats[4]));
            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + 5 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[5] + ": " + (Charisma + tempStats[5]));


            for (int s = 0; s < 6; s++)
            {
                if (points > 0)
                {
                    if (GUI.Button(new Rect(5.75f * scrW, 2.5f * scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "+"))
                    {
                        points--;
                        tempStats[s]++;
                    }
                }

                if (points < 10 && tempStats[s] > 0)
                {
                    if (GUI.Button(new Rect(3.25f * scrW, 2.5f * scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                    {
                        points++;
                        tempStats[s]--;
                    }
                }
            }
            if (GUI.Button(new Rect(3.75f * scrW, 2.5f * scrH + 6 * (0.5f * scrH), 2f * scrW, 0.5f * scrH), "Apply") && points == 0)
            {
                Time.timeScale = 1;

                Strength += tempStats[0];
                Dexterity += tempStats[1];
                Constitution += tempStats[2];
                Wisdom += tempStats[3];
                Intelligence += tempStats[4];
                Charisma += tempStats[5];
                for (int temp = 0; temp < 6; temp++)
                {
                    tempStats[temp] = 0;
                }
                showStatWindow = false;
                Cursor.visible = true;
            }
        }
        #endregion
    }
    #endregion
    public void LevelUp()
    {
        // Freeze time
        Time.timeScale = 0;
        // Disable button
        LevelButton.SetActive(false);
        // Enable stat Window
        showStatWindow = true;
    }
}