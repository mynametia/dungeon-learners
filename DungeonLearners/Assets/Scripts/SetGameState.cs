using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading;
public class SetGameState : MonoBehaviour
{
    public TMP_Text entryWorldName;
    public static string worldName;
    public void SetCurWorld()
    {
        worldName = entryWorldName.text;
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        reference.Child("worlds").GetValueAsync().ContinueWithOnMainThread(task => {
        if (task.IsFaulted) {
           Debug.Log("Could Not Read Data from DB");
        }

        else if (task.IsCompleted) {
            DataSnapshot snapshot = task.Result;
            foreach(var child in snapshot.Children) 
                {
                    World world = JsonUtility.FromJson<World>(child.GetRawJsonValue());
                    if(String.Equals(world.worldName, worldName))
                    {
                        GameState.setCurrentWorld(world);
                        break;
                    }
                }
            }
        });
    }

    private void isWorldSet (string nick, Action<bool> callbackFunction) 
    {
    FirebaseDatabase.DefaultInstance.GetReference ("users").OrderByChild ("name").EqualTo (nick).GetValueAsync ().ContinueWith (task => {
        if (task.IsFaulted) {
            Debug.LogError ("A error encountered: " + task.Exception);
        } else if (task.IsCanceled) {
            Debug.LogError ("Canceled ...");
        } else if (task.IsCompleted) {
            DataSnapshot snapshot = task.Result;
            callbackFunction(snapshot.GetValue(true) == null);
        }
    });
}

}

