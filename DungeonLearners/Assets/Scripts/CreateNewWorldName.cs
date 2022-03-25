using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;


public class CreateNewWorldName : MonoBehaviour
{
    public TMP_InputField worldName1;
    public TMP_InputField worldDesc1;
    public bool isTPMenabled;
    string world_nameOne;
    string world_descOne;

  

    public void SaveWorldName()
    {
        world_nameOne = worldName1.text;
        world_descOne = worldDesc1.text;
        PlayerPrefs.SetString("WorldDesc1", world_descOne);
        PlayerPrefs.SetString("WorldName1", world_nameOne); 
        
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        World world = new World(world_nameOne, world_descOne,"Public");

        GameState.setCurrentWorld(world);

        string json = JsonUtility.ToJson(world);
        string worldId = "0";
        reference.Child("worlds").Child(worldId).SetRawJsonValueAsync(json);

        SceneManager.LoadScene("World Manager");
    }

    void Start() {
    // Get the root reference location of the database.
  }

 
}
