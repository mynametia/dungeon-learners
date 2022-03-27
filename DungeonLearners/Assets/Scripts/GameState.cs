using UnityEngine;
using UnityEngine.SceneManagement;

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
            curWorldID  = curWorldID + 1;
            return curWorldID;
        }

}

