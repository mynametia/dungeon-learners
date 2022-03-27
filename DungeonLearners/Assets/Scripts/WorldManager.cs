using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
public class WorldManager : MonoBehaviour
{   
    public GameObject MyWorldEntry, WorldList;
    public TMP_Text WorldName;

    private int oldChildCount;

    async void Start()
    {
        readWorldDataFromDB();
    }

    public void readWorldDataFromDB()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.Child("worlds").GetValueAsync().ContinueWithOnMainThread(task => {
        if (task.IsFaulted) {
           Debug.Log("Could Read Data from DB");
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          foreach(var child in snapshot.Children) 
              {
                    World world = JsonUtility.FromJson<World>(child.GetRawJsonValue());
                    AddWorldEntry(world.worldName);
              }
        }
      });
    }

    public void AddWorldEntry(string worldName)
    {
        GameObject wEntry = Instantiate(MyWorldEntry, WorldList.transform);
        TMP_Text wName = wEntry.GetComponentInChildren<TMP_Text>();
        wName.text = worldName;
    }
}
