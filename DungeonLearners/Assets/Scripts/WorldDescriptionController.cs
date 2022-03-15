using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDescriptionController : MonoBehaviour
{
    public GameObject description, leaderboard;
    public GameObject leaderBoardEntry, rankingBoard;
        
    private int playerNumber = 10;

    void Start()
    {
        GameObject rank;
        for (int i = 1; i <= playerNumber; i++)
        {
            rank = Instantiate(leaderBoardEntry, rankingBoard.transform);
            rank.GetComponent<RankDataController>().setRanking(i);
        }
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void ShowDescription()
    {
        description.SetActive(true);
        leaderboard.SetActive(false);
    }

    public void ShowLeaderBoard()
    {
        leaderboard.SetActive(true);
        description.SetActive(false);
    }
}
