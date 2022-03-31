using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

///<summary>
/// Shows ranking on leaderboard in a descending order
///</summary>
public class WorldDescRanking: MonoBehaviour
{
    public GameObject Rank, PlayerList;
    public TMP_Text PlayerName;
    public TMP_Text PlayerEXP;

    private int oldChildCount;

    async void Start()
    {
        readPlayerDataFromDB();
    }

    public void readPlayerDataFromDB()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        reference.Child("users").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.Log("Could Read Data from DB");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var child in snapshot.Children)
                {
                    Player player = JsonUtility.FromJson<Player>(child.GetRawJsonValue());
                    AddPlayerEntry(player.userName, player.exp);
                }
            }
        });
    }

    public void AddPlayerEntry(string userName, int exp)
    {
        Debug.Log("works here");
        GameObject pEntry = Instantiate(Rank, PlayerList.transform);
        TMP_Text pName = pEntry.GetComponentInChildren<TMP_Text>();
        pName.text = userName;
        Debug.Log(pName.text);
        TMP_Text pEXP = pEntry.GetComponentInChildren<TMP_Text>();
        pEXP.text = exp.ToString();
    }
}
