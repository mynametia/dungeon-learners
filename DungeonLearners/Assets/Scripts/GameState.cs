using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class GameState : MonoBehaviour
{
        public static World currentWorld;
        public static User currentUser;

        public static int curWorldID = 0;
        
        public static void setCurrentWorld(World world){
            GameState.currentWorld = world;
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

        
        public static int getCurWorldID(){
        // Gets the largest key in the DB and increments 1 
            int largestKey  = 0;
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            reference.Child("worlds").GetValueAsync().ContinueWithOnMainThread(task => {
           
            if (task.IsFaulted) {
                Debug.Log("Could Read Data from DB");
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result;
                
                foreach(var child in snapshot.Children) 
                    {
                        print("L" + largestKey);
                        print("K:" + Int16.Parse(child.Key));
                        largestKey = Math.Max(largestKey, Int16.Parse(child.Key));
                        
                    }
                }
                 return largestKey + 1;
            });
           
           return 0;
           
        }

}

