//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;

public class FindWorldController : MonoBehaviour
{
    public TMP_InputField searchbar;

    public GameObject MyWorldEntry, WorldList;
    private string searchQuery;
    public List<World> worldsList = new List<World>();
    public List<GameObject> worldsPrefabs = new List<GameObject>();

    // Start is called before the first frame update
    async void Start()
    {

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.Child("worlds").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Could Not Read Data from DB");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var child in snapshot.Children)
                {
                    World world = JsonUtility.FromJson<World>(child.GetRawJsonValue());
                    worldsList.Add(world);
                    AddWorldEntry(world.worldName);
                }
            }
        });
    }


    public void AddWorldEntry(string worldName)
    {
        GameObject wEntry = Instantiate(MyWorldEntry, WorldList.transform);
        //Debug.Log("world entry instatiated");
        worldsPrefabs.Add(wEntry);
        TMP_Text wName = wEntry.GetComponentInChildren<TMP_Text>();
        wName.text = worldName;
    }

    public void UpdateSearchQuery()
    {
        
        
        searchQuery = searchbar.text;
        Debug.Log(searchQuery);
        Debug.Log("List prefabs before destroy" + worldsPrefabs.Count);

        //Removes all the worlds that were listed previously
        foreach (GameObject wEntry in worldsPrefabs)
        {
            //Debug.Log(prefab);
            Destroy(wEntry);
        }

        Debug.Log("List prefabs after destroy" + worldsPrefabs.Count);
        worldsPrefabs.Clear();

        //Loads search results, users can enter lowercase and incomplete strings
        //and corresponding worlds will still appear
        foreach (World worldSearch in worldsList)
        {
            if(worldSearch.worldName.ToLower().Contains(searchQuery.ToLower()))
            {
                Debug.Log(worldSearch.worldName);
                AddWorldEntry(worldSearch.worldName);                
            }         
        }
    }
}
