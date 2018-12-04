using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//you will need to change Scenes
public class CustomisationSet : MonoBehaviour {

    #region Variables
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
    [Header("Index")]
    // index numbers for our current skin, hair, mouth, eyes textures
    public int headIndex;
    public int hairIndex, mouthIndex, eyesIndex, armourIndex, shoulderIndex, beltIndex, armIndex, bootsIndex, eyebrowsIndex;
    [Header("Max Index")]
    // max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int eyebrowsMaxIndex;
    public int hairMaxIndex, mouthMaxIndex, eyesMaxIndex, armourMaxIndex, shoulderMaxIndex, beltMaxIndex, armMaxIndex, bootsMaxIndex, headMaxIndex;
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
		for (int i = 0; i < headMaxIndex; i++)
		{
			//creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Skin_#
			Sprite temp = Resources.Load("Character/Head_" + i) as Sprite;
			head.Add(temp);
			//add our temp texture that we just found to the skin List
		}
		//for loop looping from 0 to less than the max amount of skin textures we need
		for (int i = 0; i < eyebrowsMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Skin_#
            Sprite temp = Resources.Load("Character/Eyebrows_" + i) as Sprite;
            eyebrows.Add(temp);
            //add our temp texture that we just found to the skin List
        }
        //for loop looping from 0 to less than the max amount of hair textures we need
        for (int i = 0; i < hairMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Hair_#
            Sprite temp = Resources.Load("Character/Hair_" + i) as Sprite;
            //add our temp texture that we just found to the hair List
            hair.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of mouth textures we need    
        for (int i = 0; i < mouthMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Mouth_#
            Sprite temp = Resources.Load("Character/Mouth_" + i) as Sprite;
            //add our temp texture that we just found to the mouth List
            mouth.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of eyes textures we need
        for (int i = 0; i < eyesMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Eyes_#
            Sprite temp = Resources.Load("Character/Eyes_" + i) as Sprite;
            //add our temp texture that we just found to the eyes List  
            eyes.Add(temp);
        }
        //for loop looping from 0 to less than the max amount of armour textures we need
        for (int i = 0; i < armourMaxIndex ; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Armour_#
            Sprite temp = Resources.Load("Character/Armour_" + i) as Sprite;
            //add our temp texture that we just found to the armour List  
            armour.Add(temp);
        }
        // shoudler
        for (int i = 0; i < shoulderMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Armour_#
            Sprite temp = Resources.Load("Character/Armour_Shoulder_" + i) as Sprite;
            //add our temp texture that we just found to the armour List  
            shoulder.Add(temp);
        }
        // arm
        for (int i = 0; i < armMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Armour_#
            Sprite temp = Resources.Load("Character/Armour_Arm_" + i) as Sprite;
            //add our temp texture that we just found to the armour List  
            arm.Add(temp);
        }
        // belt
        for (int i = 0; i < beltMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Armour_#
            Sprite temp = Resources.Load("Character/Armour_Belt_" + i) as Sprite;
            //add our temp texture that we just found to the armour List  
            belt.Add(temp);
        }
        // boots
        for (int i = 0; i < bootsMaxIndex; i++)
        {
            //creating a temp Sprite that it grabs using Resources.Load from the Character File looking for Armour_#
            Sprite temp = Resources.Load("Character/Armour_Boots_" + i) as Sprite;
            //add our temp texture that we just found to the armour List  
            boots.Add(temp);
        }
		#endregion
		#region do this after making the function SetTexture
		//SetTexture skin, hair, mouth, eyes to the first texture 0
		SetTexture("Haid", 0, parts[9]);
		SetTexture("Hair", 0, parts[1]);
		SetTexture("Mouth", 0, parts[2]);
		SetTexture("Eyes", 0, parts[0]);
		SetTexture("Eyebrows", 0, parts[8]);
		//armour peices
		SetTexture("Armour", 0, parts[3]);
		SetTexture("Shoulder", 0, parts[4]);
		SetTexture("Boots", 0, parts[5]);
		SetTexture("Arm", 0, parts[6]);
		SetTexture("Belt", 0, parts[7]);
		ChooseClass(selectedIndex);
		#endregion
	}
    #endregion

    #region SetTexture
    void SetTexture(string type, int dir, SpriteRenderer renderer)
    {
        //we need variables that exist only within this function
        //Create a function that is called SetTexture it should contain a string and int
        //the string is the name of the material we are editing, the int is the direction we are changing
        //these are ints index numbers, max numbers, material index and Sprite array of textures
        int index = 0, max = 0;
        Sprite[] textures = new Sprite[0];
        //inside a switch statement that is swapped by the string name of our material

        #region Switch Material
        switch (type)
        {
            //hair is 1
            case "Hair":
                //index is the same as our index
                index = hairIndex;
                //max is the same as our max
                max = hairMaxIndex;
                //textures is our list .ToArray()
                textures = hair.ToArray();
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
                //break
                break;

            //Armour is 7
            case "Shoulder":
                //index is the same as our index
                index = shoulderIndex;
                //max is the same as our max
                max = shoulderMaxIndex;
                //textures is our list .ToArray()
                textures = shoulder.ToArray();
                //break
                break;

            //Armour is 8
            case "Belt":
                //index is the same as our index
                index = beltIndex;
                //max is the same as our max
                max = beltMaxIndex;
                //textures is our list .ToArray()
                textures = belt.ToArray();
                //break
                break;

            //Armour is 9
            case "Arm":
                //index is the same as our index
                index = armIndex;
                //max is the same as our max
                max = armMaxIndex;
                //textures is our list .ToArray()
                textures = arm.ToArray();
                //break
                break;

            //Armour is 10
            case "Boots":
                //index is the same as our index
                index = bootsIndex;
                //max is the same as our max
                max = bootsMaxIndex;
                //textures is our list .ToArray()
                textures = boots.ToArray();
                //break
                break;
				
			//eyes are 8
			case "Eyebrows":
				//index is the same as our index
				index = eyebrowsIndex;
				//max is the same as our max
				max = eyebrowsMaxIndex;
				//textures is our list .ToArray()
				textures = eyebrows.ToArray();
				//break
				break;

			//eyes are 8
			case "Head":
				//index is the same as our index
				index = headIndex;
				//max is the same as our max
				max = headMaxIndex;
				//textures is our list .ToArray()
				textures = head.ToArray();
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
        //our material arrays current material index's main texture is equal to our texture arrays current index
        renderer.sprite = textures[index];

        #endregion
        #region Set Material Switch
        switch (type)
        {
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

			//case eyes
			case "Eyesbrows":
				//index equals our index
				eyebrowsIndex = index;
				//break
				break;

            //case armour
            case "Armour":
                //index equals our index
                armourIndex = index;
                //break
                break;

            //case armour
            case "Shoulder":
                //index equals our index
                shoulderIndex = index;
                //break
                break;

            //case armour
            case "Belt":
                //index equals our index
                beltIndex = index;
                //break
                break;

            //case armour
            case "Arm":
                //index equals our index
                armIndex = index;
                //break
                break;

            //case armour
            case "Boots":
                //index equals our index
                bootsIndex = index;
                //break
                break;

			//case head
			case "Head":
				//index equals our index
				headIndex = index;
				//break
				break;
		}
        #endregion
    }
    #endregion

    #region Save
    public void Save() // Function called Save this will allow us to save our indexes to PlayerPrefs
    {
        PlayerPrefs.SetInt("HeadIndex", headIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyesIndex", eyesIndex);
		PlayerPrefs.SetInt("EyebrowsIndex", eyebrowsIndex);
        PlayerPrefs.SetInt("ArmourIndex", armourIndex);
        PlayerPrefs.SetInt("ShoulderIndex", shoulderIndex);
        PlayerPrefs.SetInt("ArmIndex", armIndex);
        PlayerPrefs.SetInt("BeltIndex", beltIndex);
        PlayerPrefs.SetInt("BootsIndex", bootsIndex);

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
        #region Head
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f*scrW, scrH+ i*(0.5f*scrH),0.5f*scrW, 0.5f*scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Head", - 1, parts[9]);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), 1f * scrW, 0.5f * scrH), "Head");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Head", 1, parts[9]);
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
            SetTexture("Hair", -1, parts[1]);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.55f * scrH), 1f * scrW, 0.5f * scrH), "Hair");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.55f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Hair", 1, parts[1]);
        }
        //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //move dow
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.85f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Mouth", -1, parts[2]);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.85f * scrH), 1f * scrW, 0.5f * scrH), "Mouth");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.85f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Mouth", 1, parts[2]);
        }
        //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
        //move dow
        #endregion
        #region Eyes
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (1.15f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Eyes", -1, parts[0]);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (1.15f * scrH), 1f * scrW, 0.5f * scrH), "Eyes");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (1.15f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Eyes", 1, parts[0]);
        }
        #endregion
        #region Eyebrows
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (1.45f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Eyebrows", -1, parts[8]);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (1.45f * scrH), 1f * scrW, 0.5f * scrH), "Eyebrows");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (1.45f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Eyebrows", 1, parts[8]);
        }
        #endregion
        #region Armour
        //GUI button on the left of the screen with the contence <
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (1.75f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Armour", -1, parts[3]);
            SetTexture("Shoulder", -1, parts[4]);
            SetTexture("Boots", -1, parts[5]);
            SetTexture("Arm", -1, parts[6]);
            SetTexture("Belt", -1, parts[7]);
        }

        //GUI Box or Lable on the left of the screen with the contence Skin
        GUI.Box(new Rect(0.75f * scrW, scrH + i * (1.75f * scrH), 1f * scrW, 0.5f * scrH), "Armour");

        //GUI button on the left of the screen with the contence >
        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (1.75f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  
            SetTexture("Armour", 1, parts[3]);
            SetTexture("Shoulder", 1, parts[4]);
            SetTexture("Boots", 1, parts[5]);
            SetTexture("Arm", 1, parts[6]);
            SetTexture("Belt", 1, parts[7]);
        }
        #endregion
        #region Random Reset
        // create 2 buttons one Random and one Reset
        if (GUI.Button(new Rect(0.20f * scrW, scrH + i * (2.05f * scrH), 1.05f * scrW, 0.5f * scrH), "Random"))
        {
			// Random will feed a random amount to the direction 
			SetTexture("Head", Random.Range(0, headMaxIndex - 1), parts[9]);
			SetTexture("Mouth", Random.Range(0, mouthMaxIndex - 1), parts[2]);
            SetTexture("Hair", Random.Range(0, hairMaxIndex - 1), parts[1]);
            SetTexture("Eyes", Random.Range(0, eyesMaxIndex - 1), parts[0]);
            SetTexture("Eyebrows", Random.Range(0, eyebrowsMaxIndex - 1), parts[8]);
            SetTexture("Armour", Random.Range(0, armourMaxIndex - 1), parts[3]);
            SetTexture("Shoulder", Random.Range(0, shoulderMaxIndex - 1), parts[4]);
            SetTexture("Boots", Random.Range(0, bootsMaxIndex - 1), parts[5]);
            SetTexture("Arm", Random.Range(0, armMaxIndex - 1), parts[6]);
            SetTexture("Belt", Random.Range(0, beltMaxIndex - 1), parts[7]);

        }
        // reset will set all to 0 both use SetTexture
        if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (2.05f * scrH), 1.1f * scrW, 0.5f * scrH), "Reset"))
        {
			SetTexture("Head", headIndex = 0, parts[9]);
			SetTexture("Mouth", mouthIndex = 0, parts[2]);
            SetTexture("Hair", hairIndex = 0, parts[1]);
            SetTexture("Eyes", eyesIndex = 0, parts[0]);
			SetTexture("Eyebrows", eyebrowsIndex = 0, parts[8]);
			SetTexture("Armour", armourIndex = 0, parts[3]);
            SetTexture("Shoulder", shoulderIndex = 0, parts[4]);
            SetTexture("Boots", bootsIndex = 0, parts[5]);
            SetTexture("Arm", armIndex = 0, parts[6]);
            SetTexture("Belt", beltIndex = 0, parts[7]);
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
            SceneManager.LoadScene(1);
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
