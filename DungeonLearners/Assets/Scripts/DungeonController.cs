//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class DungeonController : MonoBehaviour
{
    // Controls traversing of dungeon room sequences
    public GameObject SceneController;
    public GameObject Player;

    public int dungeonRoomCount = 4;
    public int currentDungeonRoomID = 0; // Each dungeon room has ID from 0 to dungeonRoomCount - 1
    private bool currentRoomCleared = false;
    private bool giveEXPCoin = false;
    private float EXPmultiplier = 30f, coinMultiplier = 10f;
    public int totalScore = 0, totalCoins = 0;

    private bool[] roomClearedArray;

    private float[] scoreArray;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        currentDungeonRoomID = 0;
        roomClearedArray = new bool[dungeonRoomCount]; // Initialize all elements to false
        scoreArray = new float[dungeonRoomCount];
    }

    // This stores how much HP was left from the last battle,
    // which final score and coins will be calculated from
    // HP should be value between 0 and 1
    public void UpdateRoomScore(float HP)
    {
        scoreArray[currentDungeonRoomID] = HP;
    }

    public void CalculateDungeonTotalScore()
    {
        float score = 0;
        for (int i = 0; i < dungeonRoomCount; i++)
        {
            score += scoreArray[i] * EXPmultiplier;
        }
        totalScore = Mathf.RoundToInt(score);
    }

    public void CalculateDungeonTotalCoins()
    {
        float coins = 0;
        for (int i = 0; i < dungeonRoomCount; i++)
        {
            coins += scoreArray[i] * coinMultiplier;
        }
        totalCoins = Mathf.RoundToInt(coins);
    }
    // User enters next dungeon room after defeating boss
    public void GoNextRoom()
    {
        if (currentDungeonRoomID >= dungeonRoomCount - 1)
        {
            if (giveEXPCoin)
            {
                GoToScoreScene();
            }
            else
            {
                // Go back to world
                ReturnToWorld();
            } 
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

    public void GoToScoreScene()
    {
        FindSceneController();
        CalculateDungeonTotalScore();
        CalculateDungeonTotalCoins();

        //Save coins and exp to playerprefs for retrieval next scene
        PlayerPrefs.SetInt("EXP", totalScore);
        PlayerPrefs.SetInt("Coins", totalCoins);

        SceneController.GetComponent<FadeTransitionController>().FadeToBlack("ScoreCard");
        Destroy(gameObject);
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

    public bool[] getRoomClearedArray()
    {
        return roomClearedArray;
    }

    public void updateCurrentRoomWinCond(bool win)
    {
        currentRoomCleared = win;
        updateRoomClearedArray(); // Updates the array as well
    }

    public void updateRoomClearedArray()
    {
        int roomID = getCurrentRoomID();
        bool oldCond = roomClearedArray[roomID];
        bool newCond = getCurrentRoomWinCond();

        if (roomID == roomClearedArray.Length - 1 && oldCond == false && newCond == true)
        {
            giveEXPCoin = true;
        }

        roomClearedArray[roomID] = getCurrentRoomWinCond();
    }

    private void FindSceneController()
    {
        SceneController = GameObject.FindWithTag("SceneTransition");
    }
}
