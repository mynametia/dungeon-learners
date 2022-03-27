
using UnityEngine;
using TMPro;

public class WorldDescriptionController : MonoBehaviour
{
    public GameObject description, leaderboard;
    public GameObject leaderBoardEntry, rankingBoard;
    public TMP_Text worldName, worldID;
    
    private int playerNumber = 10;


    void Start()
    {
        GameObject rank;

        for (int i = 1; i <= playerNumber; i++)
        {
            rank = Instantiate(leaderBoardEntry, rankingBoard.transform);
            rank.GetComponent<RankDataController>().setRanking(i);
        }

        TMP_Text desc = description.GetComponentInChildren<TMP_Text>();
        desc.text = GameState.getCurrentWorld().description;
        worldName.text = GameState.getCurrentWorld().worldName;
        worldID.text = GameState.getCurrentWorld().worldID.ToString();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        //player.GetComponent<PlayerMovementController>().enableMove = true;
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
