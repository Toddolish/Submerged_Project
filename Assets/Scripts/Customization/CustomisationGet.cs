using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;

public class CustomisationGet : MonoBehaviour
{
    [Header("Character")]
    //public variable for the Skinned Mesh Renderer which is our character reference
    public Renderer charMesh;
    public CharacterHandler charH;

    #region Start
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        //our character reference connected to the Skinned Mesh Renderer via finding the Mesh
        charMesh = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
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
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        //grab the gameObject in scene that is our character and set its Object name to the Characters name
        gameObject.name = PlayerPrefs.GetString("CharacterName");
    }

    #endregion
    #region SetTexture
    void SetTexture(string type, int dir) //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    {
        Texture2D tex = null;
        int matIndex = 0;

        //we need variables that exist only within this function
        //these are int material index and Texture2D array of textures
        //inside a switch statement that is swapped by the string name of our material
        
        switch (type)
        {
            // Case Skin
            case "Skin":
                //textures is our Resource.Load Character Skin save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                //material index element number is 1
                matIndex = 1;
                break;

            // Case Hair
            case "Hair":
                //textures is our Resource.Load Character Hair save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                //material index element number is 2
                matIndex = 2;
                break;

            // Case Mouth
            case "Mouth":
                //textures is our Resource.Load Character Mouth save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                //material index element number is 4
                matIndex = 3;
                break;

            // Case Eyes
            case "Eyes":
                //textures is our Resource.Load Character Eyes save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                //material index element number is 3
                matIndex = 4;
                break;

            // Case Clothes
            case "Clothes":
                //textures is our Resource.Load Character Clothes save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                //material index element number is 5
                matIndex = 5;
                break;

            // Case Armour
            case "Armour":
                //textures is our Resource.Load Character Armour save index we loaded in set as our Texture2D
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                //material index element number is 6
                matIndex = 6;
                break;
        }
        //Material array is equal to our characters material list
        Material[] mats = charMesh.materials;
        //our material arrays current material index's main texture is equal to our texture arrays current index
        mats[matIndex].mainTexture = tex;
        //our characters materials are equal to the material array
        charMesh.materials = mats;
    }
    #endregion
}
