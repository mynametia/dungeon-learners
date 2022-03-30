
using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Extensions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;

public class WorldDescriptionController : MonoBehaviour
{
    public GameObject description, leaderboard;
    public GameObject leaderBoardEntry, rankingBoard;
    public TMP_Text worldName, worldID;
    public GameObject Rank, PlayerList;
    //public TMP_Text PlayerName;
    //public TMP_Text PlayerEXP;



    private int playerNumber = 10;


    void Start()
    {
        StartCoroutine(LoadingTime());

        


    }

    IEnumerator LoadingTime()
    {
        
        yield return new WaitForSeconds(1);
        Debug.Log("timecheck");
        GameObject rank;

        TMP_Text desc = description.GetComponentInChildren<TMP_Text>();
        desc.text = GameState.getCurrentWorld().description;
        worldName.text = GameState.getCurrentWorld().worldName;
        Debug.Log("check 1" + worldName.text);
        worldID.text = GameState.getCurrentWorld().worldID.ToString();
        readPlayerDataFromDB();
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
        Debug.Log("desc active");
    }

    public void ShowLeaderBoard()
    {
        leaderboard.SetActive(true);
        description.SetActive(false);
        Debug.Log("leadeerbrd active");

        //readPlayerDataFromDB();
    }

    public void readPlayerDataFromDB()
    {
        List<Player> pList = new List<Player>();
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
       // Debug.Log("check 1" + worldName.text);

        reference.Child("worlds").Child(worldID.text).Child("players").OrderByChild("exp").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Could Read Data from DB");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int childcount = (int)snapshot.ChildrenCount;
                int count = 1;

                foreach (var child in snapshot.Children)
                {
                   // Debug.Log(child.GetRawJsonValue());
                    Player playerRead = JsonUtility.FromJson<Player>(child.GetRawJsonValue());
                   // Debug.Log(playerRead.userName);
                    //Debug.Log(playerRead.exp);
                    pList.Add(playerRead);

                }

                pList.Reverse();

                foreach (Player player in pList)
                {
                    //Debug.Log("second list" + player.userName);
                   // Debug.Log(player.exp);
                    AddPlayerEntry(count, player.userName, player.exp);
                    count++;
                }
            }
        });
    }

    public void AddPlayerEntry(int count, string userName, int exp)
    {
        //Debug.Log("does it enter");
        GameObject pEntry = Instantiate(Rank, PlayerList.transform);
        TMP_Text[] fields = pEntry.GetComponentsInChildren<TMP_Text>();
        //Debug.Log(fields.Length);
        int boxCount = 0;

        foreach (TMP_Text textbox in fields)
        {
            if (boxCount == 0)
            {
                TMP_Text rankCount = textbox;
                rankCount.text = count.ToString();
                boxCount++;
                continue;
            }
            if (boxCount == 1)
            {
                TMP_Text pName = textbox;
                pName.text = userName;
                boxCount++;
                continue;
            }
            if (boxCount == 2)
            {
                TMP_Text pEXP = textbox;
                pEXP.text = exp.ToString();
                boxCount++;
                continue;
            }
        }


        //TMP_Text pName = pEntry.GetComponentInChildren<TMP_Text>();
        //pName.text = userName;
        //TMP_Text pEXP = pEntry.GetComponentInChildren<TMP_Text>();
        //pEXP.text = exp.ToString();



    }
}

