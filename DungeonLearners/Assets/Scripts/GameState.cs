using UnityEngine;
using UnityEngine.SceneManagement;

using static World;

public class GameState : MonoBehaviour
{
        public static World currentWorld;
        public static User currentUser;
        
        // public GameState(World world){
        //     GameState.currentWorld = world;
        // }

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



}