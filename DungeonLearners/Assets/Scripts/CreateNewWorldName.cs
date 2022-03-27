using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;
using Firebase.Firestore;
using static Firebase.Firestore.DocumentReference;
public class CreateNewWorldName : MonoBehaviour
{
    public TMP_InputField worldName1;
    public TMP_InputField worldDesc1;
    string world_nameOne;
    string world_descOne;

    public void SaveWorldName()
    {
        world_nameOne = worldName1.text;
        world_descOne = worldDesc1.text;
        PlayerPrefs.SetString("WorldDesc1", world_descOne);
        PlayerPrefs.SetString("WorldName1", world_nameOne); 

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        World world = new World(world_nameOne, world_descOne, "Public");

        GameState.setCurrentWorld(world);

        string json = JsonUtility.ToJson(world);
        string worldId = GameState.getCurWorldID().ToString();
        print(worldId.GetType());
        reference.Child("worlds").Child(worldId).SetRawJsonValueAsync(json);


        // var worldVar = new WorldData{
            
        //     worldName = world_nameOne, 
        //     worldDescription = world_descOne, 
        //     visibility = "Public"
        // };

        // var firestore = FirebaseFirestore.DefaultInstance;
        // // DocumentReference docRef = firestoreDB.Collection("question_bank").Document(world_nameOne);
        
        // firestore.Document("question_bank").SetAsync(worldVar);
        
        // Dictionary<string, object> city = new Dictionary<string, object>
        // {
        //         // { "Name", "Los Angeles" },
        //         // { "State", "CA" },
        //         // { "Country", "USA" }
        // };
        // docRef.SetAsync(city).ContinueWithOnMainThread(task => {
        //         Debug.Log("Added world to the collection");
        // });

        SceneManager.LoadScene("World Manager");

    }
}
