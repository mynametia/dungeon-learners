using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;

public class CreateDungeon : MonoBehaviour
{
    //Firebase Variables
    public DatabaseReference DBreference;

    [Header("Dungeon Information")]
    public TMP_InputField dungeonNameField;

    void Start()
    {
        Debug.Log("Setting up Firebase Database");
        //set database instance object
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveDungeon()
    {
        Dungeon dungeon = new Dungeon();
        dungeon.DungeonName = dungeonNameField.text;
        string json = JsonUtility.ToJson(dungeon);

        DBreference.Child("Dungeon").Child(dungeon.DungeonName).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if(task.IsCompleted)
            {
                Debug.Log("Successfully added data to firebase");
            }
            else
            {
                Debug.Log("Not successful");
            }
        }
        );

    }
}
