using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;

public class CustomisationGet : MonoBehaviour
{
    [Header("Sprite Renderer List")]
    public SpriteRenderer[] parts;
    [Header("Texture List")]
    // Sprite List for skin,hair, mouth, eyes
    public List<Sprite> head = new List<Sprite>();
    public List<Sprite> hair = new List<Sprite>();
    public List<Sprite> mouth = new List<Sprite>();
    public List<Sprite> eyes = new List<Sprite>();
    public List<Sprite> eyebrows = new List<Sprite>();
    public List<Sprite> weapon = new List<Sprite>();
    public List<Sprite> armour = new List<Sprite>();
    public List<Sprite> boots = new List<Sprite>();
    public List<Sprite> shoulder = new List<Sprite>();
    public List<Sprite> arm = new List<Sprite>();
    public List<Sprite> belt = new List<Sprite>();
    #region Start
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        //our character reference connected to the Skinned Mesh Renderer via finding the Mesh
        //Run the function LoadTexture
        LoadTexture();
    }
    #endregion

    #region LoadTexture Function
    void LoadTexture()
    {
        //check to see if PlayerPrefs (our save location) HasKey (has a save file...you will need to reference the name of a file)
        //if it doesnt then load the CustomSet level
        if(!PlayerPrefs.HasKey("CharacterName"))
        {
            SceneManager.LoadScene(1);
        }
        //if it does have a save file then load and SetTexture Skin, Hair, Mouth and Eyes from PlayerPrefs
        // SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"),parts[0]);
     
        SetTexture("Head",0, parts[9]);
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"), parts[1]);
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"), parts[0]);
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"), parts[2]);
        SetTexture("Eyebrows", PlayerPrefs.GetInt("EyebrowIndex"), parts[8]);
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"), parts[3]);
        SetTexture("Shoulder", PlayerPrefs.GetInt("ShoulderIndex"), parts[4]);
        SetTexture("Arm", PlayerPrefs.GetInt("ArmIndex"), parts[6]);
        SetTexture("Belt", PlayerPrefs.GetInt("BeltIndex"), parts[7]);
        SetTexture("Boots", PlayerPrefs.GetInt("BootsIndex"), parts[5]);
        //grab the gameObject in scene that is our character and set its Object name to the Characters name
        gameObject.name = PlayerPrefs.GetString("CharacterName");

    
    }

    #endregion
    #region SetTexture
    void SetTexture(string type, int dir, SpriteRenderer spriteRenderer) //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    {
        Sprite tex = null;

        //we need variables that exist only within this function
        //these are int material index and Sprite array of textures
        //inside a switch statement that is swapped by the string name of our material
        
        switch (type)
        {
            // Case Skin
            case "Head":
                //textures is our Resource.Load Character Skin save index we loaded in set as our Sprite
                tex = head[dir];
                //material index element number is 1
                break;

            // Case Hair
            case "Hair":
                //textures is our Resource.Load Character Hair save index we loaded in set as our Sprite
                tex = hair[dir];
                //material index element number is 2
                break;

            // Case Mouth
            case "Mouth":
                //textures is our Resource.Load Character Mouth save index we loaded in set as our Sprite
                tex = mouth[dir];
                //material index element number is 4
                break;

            // Case Eyes
            case "Eyes":
                //textures is our Resource.Load Character Eyes save index we loaded in set as our Sprite
                tex = eyes[dir];
                //material index element number is 3
                break;

            // Case Armour
            case "Armour":
                //textures is our Resource.Load Character Armour save index we loaded in set as our Sprite
                tex = armour[dir];
                //material index element number is 6
                break;

            case "Boots":
                //textures is our Resource.Load Character Armour save index we loaded in set as our Sprite
                tex = boots[dir];
                //material index element number is 6
                break;

            case "Shoulder":
                //textures is our Resource.Load Character Armour save index we loaded in set as our Sprite
                tex = shoulder[dir];
                //material index element number is 6
                break;

            case "Arm":
                //textures is our Resource.Load Character Armour save index we loaded in set as our Sprite
                tex = arm[dir];
                //material index element number is 6
                break;

            case "Belt":
                //textures is our Resource.Load Character Armour save index we loaded in set as our Sprite
                tex = belt[dir];

                //material index element number is 6
                break;
        }
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.sprite = tex;
        }
    }
    #endregion
}
