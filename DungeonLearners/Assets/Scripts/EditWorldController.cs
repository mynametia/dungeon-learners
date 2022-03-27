using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;

public class EditWorldController : MonoBehaviour
{
    public TMP_InputField text1;
    public TMP_InputField desc1;
    string worldName;
    string worldDescription;
    int worldID;

    void Start()
    {
        worldName = GameState.getCurrentWorld().worldName;
        worldDescription = GameState.getCurrentWorld().description;
        worldID = GameState.getCurrentWorld().worldID;

        text1.text = worldName;
        desc1.text = worldDescription;
    }

    public void SaveWorldName()
    {
        worldName = text1.text;
        worldDescription = desc1.text;
       
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        World world = new World(worldID, worldName, worldDescription, "Public");

        GameState.setCurrentWorld(world);

        string json = JsonUtility.ToJson(world);
        reference.Child("worlds").Child(worldID.ToString()).SetRawJsonValueAsync(json);

        SceneManager.LoadScene("World Manager");

    }
}
