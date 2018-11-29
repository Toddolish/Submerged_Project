using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//you will need to change Scenes
public class CustomisationSet : MonoBehaviour {

    #region Variables
    [Header("Texture List")]
    // Texture2D List for skin,hair, mouth, eyes
    public List<Texture2D> Head = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> weapon = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    [Header("Index")]
    // index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int hairIndex, mouthIndex, eyesIndex, clothesIndex, armourIndex;
    [Header("Renderer")]
    // renderer for our character mesh so we can reference a material list
    public Renderer character;
    [Header("Max Index")]
    // max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMaxIndex;
    public int hairMaxIndex, mouthMaxIndex, eyesMaxIndex, clothesMaxIndex, armourMaxIndex;
    [Header("Character Name")]
    // name of our character that the user is making
    public string charName = "Adventurer";
    // base stats that will better our character
    [Header("Stats")]
    // base stats for player
    public string[] statArray = new string[6];
    public int[] stats = new int[6];
    public int[] tempStats = new int[6];

    // points in which we use to increase our stats
    public int points = 3;
    public CharacterClass charClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[8];
    public string button = "Choose Class";
    public int selectedIndex = 0;
    #endregion
    public bool showDropdown;
    public Vector2 scrollPos;
   
    #region Start
    private void Start() // in start we need to set up the following
    {
        statArray = new string[] { "Strength", "Dexterity", "Constitution", "Wisdom", "Intelligence", "Charisma" };
        selectedClass = new string[] { "Barbarian", "Bard", "Druid", "Monk", "Paladin", "Ranger", "Sorcerer", "Warlock" };
        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMaxIndex; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/Skin_" + i) as Texture2D;
            Head.Add(temp);
            //add our temp texture that we just found to the skin List
        }
        //for loop looping from 0 to less than the max amount of hair textures we need
        for (int i = 0; i < hairMaxIndex; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            //add our temp texture that we just found to the hair List
            hair.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of mouth textures we need    
        for (int i = 0; i < mouthMaxIndex; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            //add our temp texture that we just found to the mouth List
            mouth.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of eyes textures we need
        for (int i = 0; i < eyesMaxIndex; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            //add our temp texture that we just found to the eyes List  
            eyes.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of clothes textures we need
        for (int i = 0; i < clothesMaxIndex ; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Clothes_#
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            //add our temp texture that we just found to the clothes List  
            weapon.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of armour textures we need
        for (int i = 0; i < armourMaxIndex ; i++)
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Armour_#
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            //add our temp texture that we just found to the armour List  
            armour.Add(temp);
        }
        #endregion

        // connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();

        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        #endregion
        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        ChooseClass(selectedIndex);
    }
    #endregion

    #region SetTexture
    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //Create a function that is called SetTexture it should contain a string and int
        //the string is the name of the material we are editing, the int is the direction we are changing
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, matIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        //inside a switch statement that is swapped by the string name of our material

        #region Switch Material
        switch (type)
        {
            //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as our skin max
                max = skinMaxIndex;
                //textures is our skin list .ToArray()
                textures = Head.ToArray();
                //material index element number is 1
                matIndex = 1;
                //break
                break;
            //now repeat for each material 

            //hair is 2
            case "Hair":
                //index is the same as our index
                index = hairIndex;
                //max is the same as our max
                max = hairMaxIndex;
                //textures is our list .ToArray()
                textures = hair.ToArray();
                //material index element number is 2
                matIndex = 2;
                //break
                break;

            //mouth is 3
            case "Mouth":
                //index is the same as our index
                index = mouthIndex;
                //max is the same as our max
                max = mouthMaxIndex;
                //textures is our list .ToArray()
                textures = mouth.ToArray();
                //material index element number is 3
                matIndex = 3;
                //break
                break;

            //eyes are 4
            case "Eyes":
                //index is the same as our index
                index = eyesIndex;
                //max is the same as our max
                max = eyesMaxIndex;
                //textures is our list .ToArray()
                textures = eyes.ToArray();
                //material index element number is 4
                matIndex = 4;
                //break
                break;

            //clothes are 5
            case "Clothes":
                //index is the same as our index
                index = clothesIndex;
                //max is the same as our max
                max = clothesMaxIndex;
                //textures is our list .ToArray()
                textures = weapon.ToArray();
                //material index element number is 5
                matIndex = 5;
                //break
                break;

            //Armour is 6
            case "Armour":
                //index is the same as our index
                index = armourIndex;
                //max is the same as our max
                max = armourMaxIndex;
                //textures is our list .ToArray()
                textures = armour.ToArray();
                //material index element number is 6
                matIndex = 6;
                //break
                break;
        }
        #endregion
        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }
        //Material array is equal to our characters material list
        Material[] mat = character.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mat[matIndex].mainTexture = textures[index];
        //our characters materials are equal to the material array
        character.materials = mat;
        //create another switch that is goverened by the same string name of our material

        #endregion
        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;

            //case hair
            case "Hair":
                //index equals our index
                hairIndex = index;
                //break
                break;

            //case mouth
            case "Mouth":
                //index equals our index
                mouthIndex = index;
                //break
                break;

            //case eyes
            case "Eyes":
                //index equals our index
                eyesIndex = index;
                //break
                break;

            //case clothes
            case "Clothes":
                //index equals our index
                clothesIndex = index;
                //break
                break;

            //case armour
            case "Armour":
                //index equals our index
                armourIndex = index;
                //break
                break;
        }
        #endregion
    }
    #endregion

    #region Save
    public void Save() // Function called Save this will allow us to save our indexes to PlayerPrefs
    {
        // SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex, ClothesIndex, ArmourIndex
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
        PlayerPrefs.SetInt("ClothesIndex", clothesIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);

        // SetString CharacterName
        PlayerPrefs.SetString("CharacterName", charName);

        // Set player stats
        for (int i = 0; i < stats.Length; i++)
        {
            PlayerPrefs.SetInt(statArray[i], (stats[i] + tempStats[i]));
        }
        PlayerPrefs.SetString("CharacterClass", selectedClass[selectedIndex]);
    }
    #endregion
    #region OnGUI

    // point in which we use increase our stats
    public int point = 10;
    private void OnGUI()// Function for our GUI elements
    {
        // create the floats scrW and scrH that govern our 16:9 ratio
        float scrW = Screen.width/16;
        float scrH = Screen.height/9;
        // create an int that will help with shuffling your GUI elements under eachother
        int i = 1;
        #region Skin
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f*scrW, scrH+ i*(0.5f*scrH),0.5f*scrW, 0.5f*scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Skin", - 1);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Skin");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Skin", 1);
        }
        //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Hair
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.55f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Hair", -1);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.55f * scrH), 1f * scrW, 0.5f * scrH), "Hair");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.55f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Hair", 1);
        }
        //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //move dow
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.85f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Mouth", -1);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.85f * scrH), 1f * scrW, 0.5f * scrH), "Mouth");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.85f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Mouth", 1);
        }
        //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //move dow
        #endregion
        #region Eyes
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (1.15f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Eyes", -1);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (1.15f * scrH), 1f * scrW, 0.5f * scrH), "Eyes");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (1.15f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Eyes", 1);
        }
        #endregion
        #region Clothes
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (1.45f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Clothes", -1);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (1.45f * scrH), 1f * scrW, 0.5f * scrH), "Clothes");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (1.45f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Clothes", 1);
        }
        #endregion
        #region Armour
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (1.75f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Armour", -1);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (1.75f * scrH), 1f * scrW, 0.5f * scrH), "Armour");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (1.75f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Armour", 1);
        }
        #endregion
        #region Random Reset
        // create 2 buttons one Random and one Reset
        if (GUI.Button(new Rect(0.20f * scrW, scrH + i * (2.05f * scrH), 1.05f * scrW, 0.5f * scrH), "Random"))
        {
            // Random will feed a random amount to the direction 
            SetTexture("Skin",Random.Range(0, skinMaxIndex - 1));
            SetTexture("Mouth", Random.Range(0, mouthMaxIndex - 1));
            SetTexture("Hair", Random.Range(0, hairMaxIndex - 1));
            SetTexture("Eyes", Random.Range(0, eyesMaxIndex - 1));
            SetTexture("Clothes", Random.Range(0, clothesMaxIndex - 1));
            SetTexture("Armour", Random.Range(0, armourMaxIndex - 1));

        }
        // reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (2.05f * scrH), 1.1f * scrW, 0.5f * scrH), "Reset"))
        {
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Eyes", eyesIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
        }
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        i++;
        #endregion
        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        charName = GUI.TextField(new Rect(0.10f * scrW, scrH + i * (2.05f * scrH), 2f * scrW, 0.5f * scrH), charName, 16);
        if (GUI.Button(new Rect(0.05f * scrW, scrH + i * (2.30f * scrH), 2f * scrW, 0.5f * scrH), "Save & Play") && points == 0)
        {
            Save();
            SceneManager.LoadScene(2);
        }
        //GUI Button called Save and Play
        //this button will run the save function and also load into the game level
        // Create a back button to get to the main menu
        if (GUI.Button(new Rect(0.05f * scrW, scrH + i * (2.5f * scrH), 2f * scrW, 0.5f * scrH), "Back to Menu"))
        {
            SceneManager.LoadScene(0);
        }
        #endregion
        #region Stats
        if (GUI.Button(new Rect(11.6f * scrW, 2f * scrH, 2f * scrW, 0.5f * scrH), button))
        {
            showDropdown = !showDropdown;
        }
        if (showDropdown) //if show drop down is on
        {
            //our scroll position is equal to our posision in our scroll view
            //start on GUI elements in scroll view
            if (selectedClass.Length <= 6)
            {
                scrollPos = GUI.BeginScrollView(new Rect(11.6f * scrW, 3f * scrH, 3f * scrW, 3f * scrH), scrollPos, new Rect(0, 0, 2 * scrW, 3f * scrH), false, false);
            }
            else
            {
                scrollPos = GUI.BeginScrollView(new Rect(11.6f * scrW, 3f * scrH, 3f * scrW, 3f * scrH), scrollPos, new Rect(0, 0, 2 * scrW, 3f * scrH + ((selectedClass.Length - 6) * (scrH * 0.5f))), false, true);
            }
            for (int c = 0; c < selectedClass.Length; c++)// for all the options
            {
                //create a button in top of scroll view
                if(GUI.Button(new Rect(0,0+c*(scrH*0.5f),3f * scrW,0.5f * scrH), selectedClass[c]))
                {
                    selectedIndex = c;
                    ChooseClass(selectedIndex);
                    button = selectedClass[c];
                    showDropdown = !showDropdown;
                }
            }
            //end gui elements inside scroll view
            GUI.EndScrollView();
        }
        #region Classes
        GUI.Box(new Rect(3.75f * scrW, 2f * scrH, 2f * scrW, 0.5f * scrH), "Points: " + points);
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

            GUI.Box(new Rect(3.75f * scrW, 2.5f * scrH + s *(0.5f * scrH), 2f * scrW, 0.5f * scrH), statArray[s] + ": " + (stats[s] + tempStats[s]));
            if (points < 10 && tempStats[s] > 0)
            {
                if (GUI.Button(new Rect(3.25f * scrW, 2.5f * scrH + s * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "-"))
                {
                    points++;
                    tempStats[s]--;
                }
            }
        }
        #endregion
        #endregion

    }
    void ChooseClass(int className)
    {
        switch (className)
        {
            //Barbarian
            case 0:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Barbarian;
                break;

            //Bard
            case 1:
                stats[0] = 5;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 15;
                charClass = CharacterClass.Bard;
                break;

            //Druid
            case 2:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 10;
                charClass = CharacterClass.Druid;
                break;

            //Monk
            case 3:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 15;
                stats[3] = 10;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Monk;
                break;

            //Paladin
            case 4:
                stats[0] = 15;
                stats[1] = 10;
                stats[2] = 15;
                stats[3] = 5;
                stats[4] = 5;
                stats[5] = 10;
                charClass = CharacterClass.Paladin;
                break;

            //Ranger
            case 5:
                stats[0] = 5;
                stats[1] = 15;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Ranger;
                break;

            //Sorcerer
            case 6:
                stats[0] = 10;
                stats[1] = 10;
                stats[2] = 10;
                stats[3] = 15;
                stats[4] = 10;
                stats[5] = 5;
                charClass = CharacterClass.Sorcerer;
                break;

            //Warlock
            case 7:
                stats[0] = 5;
                stats[1] = 5;
                stats[2] = 5;
                stats[3] = 15;
                stats[4] = 15;
                stats[5] = 15;
                charClass = CharacterClass.Warlock;
                break;
        }
    }
    #endregion
}
public enum CharacterClass
{
    Barbarian,
    Bard,
    Druid,
    Monk,
    Paladin,
    Ranger,
    Sorcerer,
    Warlock
}
