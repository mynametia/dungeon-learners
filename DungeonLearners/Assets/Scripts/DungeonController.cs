using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonController : MonoBehaviour
{
    // Controls traversing of dungeon room sequences
    public GameObject SceneController;
    public GameObject Player;

    public int dungeonRoomCount = 4;
    public int currentDungeonRoomID = 0; // Each dungeon room has ID from 0 to dungeonRoomCount - 1
    private bool currentRoomCleared = false;

    private bool[] roomClearedArray;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        currentDungeonRoomID = 0;
        roomClearedArray = new bool[dungeonRoomCount]; // Initialize all elements to false
    }

    // User starts battle
    //public void EnterBattle()
    //{
    //    Player = GameObject.FindWithTag("Player");
    //    PlayerPrefs.SetFloat("PlayerDungeonX", Player.transform.position.x);
    //    PlayerPrefs.SetFloat("PlayerDungeonY", Player.transform.position.y);
    //    SceneController.GetComponent<FadeTransitionController>().FadeToBlack("CardBattle");
        
    //}

    // User enters next dungeon room after defeating boss
    public void GoNextRoom()
    {

        if (currentDungeonRoomID >= dungeonRoomCount - 1)
        {
            // Go back to world
            ReturnToWorld();
        }
        else
        {
            // Go to next dungeon room
            FindSceneController();
            currentDungeonRoomID++;
            SceneController.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");
        }
    }

    //User goes back to previous room
    public void GoPreviousRoom()
    {
        FindSceneController();

        if (currentDungeonRoomID <= 0)
        {
            // Go back to world
            ReturnToWorld();
        }
        else
        {
            // Go to previous dungeon room
            FindSceneController();
            currentDungeonRoomID--;
            SceneController.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");
        }
    }

    public void ReturnToWorld()
    {
        // Go back to world
        FindSceneController();
        SceneController.GetComponent<FadeTransitionController>().FadeToBlack("OpenWorld");
        Destroy(gameObject);
    }

    public int getCurrentRoomID()
    {
        return currentDungeonRoomID;
    }

    public bool getCurrentRoomWinCond()
    {
        return currentRoomCleared;
    }

    public void updateCurrentRoomWinCond(bool win)
    {
        currentRoomCleared = win;
    }

    private void FindSceneController()
    {
        SceneController = GameObject.FindWithTag("SceneTransition");
    }
}
