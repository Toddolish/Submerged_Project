using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[AddComponentMenu("FirstPerson/Character Handler")]
public class CharacterHandler : MonoBehaviour
{
    #region Variables

    #region Character 
    [Header("Character")]
    public bool alive;
    public CharacterController controller;
    public CharacterClass playerClass;
    #endregion
    #region Health, Mana, Stamina
    [Space(20)]
    [Header("Health")]
    public GUIStyle RedBox;
    public float curHealth, maxHealth;

    [Space(20)]
    [Header("Mana")]
    public GUIStyle blueBox;
    public float curMana, maxMana;

    [Space(20)]
    [Header("Stamina")]
    public GUIStyle yellowBox;
    public float curStamina, maxStamina;
    public GUIStyle orangeBox;
    public GUIStyle greenBox;

    #region Stats
    [Space(40)]
    [Header("Stats")]
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

    public float regenerationTime;
    #endregion
    #endregion
    #region Level and Exp
    [Header("Levels and Exp")]
    //players current level
    public int level;
    public Text levelText;
    //max and min experience 
    public float maxEXP, curEXP;
    #endregion
    #region MiniMap
    [Header("Camera Connection")]
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    public bool showStatWindow;
    public Vector2 scrollPos;
    public int points = 0;
    public string[] statArray = new string[6];
    public int[] tempStats = new int[6];
    #endregion
    #region Start
    private void Start()
    {
        #region Health
        //set max health to 100
        maxHealth = 100;

        //set current health to max
        curHealth = maxHealth;

        //make sure player is alive
        alive = true;

        #endregion
        #region Mana
        //set max mana to 100
        maxMana = 100;

        //set current mana to max
        curMana = maxMana;

        #endregion
        #region Stamina
        //set max stamina to 100
        maxStamina = 100;

        //set current stamina to max
        curStamina = maxStamina;

        #endregion
        #region EXP
        points = 0;
        level = 1;
        levelText = GameObject.Find("Level").GetComponent<Text>();
        //max exp starts at 60
        maxEXP = 60;
        //connect the Character Controller to the controller variable
        controller = GetComponent<CharacterController>();
        #endregion
        #region Loading

        //stats
        Strength = PlayerPrefs.GetInt("Strength", 10);
        Dexterity = PlayerPrefs.GetInt("Dexterity", 10);
        Constitution = PlayerPrefs.GetInt("Constitution", 10);
        Wisdom = PlayerPrefs.GetInt("Wisdom", 10);
        Intelligence = PlayerPrefs.GetInt("Intelligence", 10);
        Charisma = PlayerPrefs.GetInt("Charisma", 10);

        maxHealth += Strength * 0.5f;
        maxMana += Wisdom * 1f;
        maxStamina += Dexterity * 1f;

        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        #endregion
    }
    #endregion
    #region Update
    private void Update()
    {
        #region Exp
        levelText.text = level.ToString("F0");
        //if our current experience is greater or equal to the maximum experience
        if (curEXP >= maxEXP)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            curEXP -= maxEXP;

            //our level goes up by one
            level = level + 1;
            points += 3;
            showStatWindow = true;

            //the maximum amount of experience is increased by 50
            maxEXP = maxEXP + 50;
        }
        #endregion
        #region Stats
        curHealth += Time.deltaTime * regenerationTime;
        curMana += Time.deltaTime * regenerationTime;
        curStamina += Time.deltaTime * regenerationTime;


        //set up our aspect ratio for the GUI elements
        //scrW - 16
        float scrW = Screen.width / 16;
        //scrH - 9
        float scrH = Screen.height / 9;
    }
    #endregion
    #endregion
    #region LateUpdate
    private void LateUpdate()
    {
        #region Health
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            //then our current health is equal to the max health
            curHealth = maxHealth;
        }

        if (curHealth < 0 || !alive)
        {
            //current health equals 0
            curHealth = 0;
            Debug.Log("If less than 0");
        }
        #endregion
        #region Stamina
        if (curStamina > maxStamina)
        {
            //then our current health is equal to the max health
            curStamina = maxStamina;
        }
        if (curStamina <= 0)
        {
            //then our current health is equal to the max health
            curStamina = 0;
        }
        #endregion
        #region Mana
        if (curMana > maxMana)
        {
            //then our current health is equal to the max health
            curMana = maxMana;
        }
        if (curMana <= 0)
        {
            //then our current health is equal to the max health
            curMana = 0;
        }
        #endregion
        #region PlayerAlive
        //if the player is alive
        //and our health is less than or equal to 0
        if (alive && curHealth <= 0)
        {
            //alive is false
            alive = false;

            //controller is turned off
            controller.enabled = false;
            Debug.Log("Disable on Death");
        }
        #endregion
    }

    #endregion
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "exp")
        {
            curEXP += Time.deltaTime * 10;
        }
    }
    #region OnGUI
    private void OnGUI()
    {
        // create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        #region Stats

        if (GUI.Button(new Rect(1f * scrW, 2.5f * scrH * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "LEVEL UP"))
        {

        }


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
                GettingData();
                showStatWindow = false;
                Cursor.visible = false;
            }
        }

        #endregion

        // HEALTH------------
        //GUI Box on screen for the healthbar background
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.3f * scrH), "");
        //GUI Box for current health that moves in same place as the background bar
        GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.3f * scrH), curHealth.ToString("F0") + "/" + maxHealth.ToString("F0"), RedBox);
        //current Health divided by the posistion on screen and timesed by the total max health

        // MANA-------------
        //GUI Box on screen for the experience background
        GUI.Box(new Rect(6 * scrW, 0.55f * scrH, 4 * scrW, 0.3f * scrH), "");
        //GUI Box for current experience that moves in same place as the background bar
        GUI.Box(new Rect(6 * scrW, 0.55f * scrH, curMana * (4 * scrW) / maxMana, 0.3f * scrH), curMana.ToString("F0") + "/" + maxMana.ToString("F0"), blueBox);

        // STAMINA----------
        //GUI Box on screen for the experience background
        GUI.Box(new Rect(6 * scrW, 0.8f * scrH, 4 * scrW, 0.3f * scrH), "");
        //GUI Box for current experience that moves in same place as the background bar
        GUI.Box(new Rect(6 * scrW, 0.8f * scrH, curStamina * (4 * scrW) / maxStamina, 0.3f * scrH), curStamina.ToString("F0") + "/" + maxStamina.ToString("F0"), orangeBox);

        // EXP--------------
        //GUI Box on screen for the experience background
        GUI.Box(new Rect(6 * scrW, 1.1f * scrH, 4 * scrW, 0.25f * scrH), "");
        //GUI Box for current experience that moves in same place as the background bar
        GUI.Box(new Rect(6 * scrW, 1.1f * scrH, curEXP * (4 * scrW) / maxEXP, 0.25f * scrH), curEXP.ToString("F0") + "/" + maxEXP.ToString("F0"), greenBox);

        //current experience divided by the posistion on screen and timesed by the total max experience
        //GUI Draw Texture on the screen that has the mini map render texture attached
        GUI.DrawTexture(new Rect(13.75f * scrW, 0.25f * scrH, 2 * scrW, 2 * scrH), miniMap);
    }
    void GettingData()
    {
    }
    void SavingData()
    {
       
    }
    #endregion
}