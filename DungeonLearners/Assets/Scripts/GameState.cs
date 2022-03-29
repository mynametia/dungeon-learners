using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class GameState : MonoBehaviour
{
    public static World currentWorld = null;
    public static User currentUser;

    public static int newWorldID = 0;

    void Start(){
        if (currentWorld == null){
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
                            setCurrentWorld(world);
                            break;
                    }
                }
            });
        }
    }
    public static void setCurrentWorld(World world){
        GameState.currentWorld = world;
        Debug.Log("Current World Set to: " + world.worldName);
    }

    public static void setCurrentUser(User user){
        GameState.currentUser = user;
    }
    public static World getCurrentWorld(){
        return currentWorld;
    }

    public static User getCurrentUser(){
        return currentUser;
    }

    public static int getNewWorldID(){
        newWorldID  = newWorldID + 1;
        return newWorldID;
    }
}

