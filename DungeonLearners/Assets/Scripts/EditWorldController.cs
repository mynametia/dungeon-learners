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
    string world1;
    string worlddesc1;

    void Start()
    {
        world1 = PlayerPrefs.GetString("WorldName1");
        text1.text = world1;
        worlddesc1 = PlayerPrefs.GetString("WorldDesc1");
        desc1.text = worlddesc1;
    }

    public void SaveWorldName()
    {
        world1 = text1.text;
        worlddesc1 = desc1.text;
        PlayerPrefs.SetString("WorldDesc1", worlddesc1);
        PlayerPrefs.SetString("WorldName1", world1); 

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        World world = new World(world1, worlddesc1, "Public");

        GameState.setCurrentWorld(world);

        string json = JsonUtility.ToJson(world);
        string worldId = GameState.getCurWorldID().ToString();
        print(worldId.GetType());
        reference.Child("worlds").Child(worldId).SetRawJsonValueAsync(json);

        SceneManager.LoadScene("World Manager");

    }
}
