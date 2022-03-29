using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class ScoreManager : MonoBehaviour
{
    private int score, coins;
    
    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        score = PlayerPrefs.GetInt("EXP");
        coins = PlayerPrefs.GetInt("Coins");

        StartCoroutine(UpdateCoins(coins));
        StartCoroutine(UpdateScore(score));
    }

    private IEnumerator UpdateCoins (int _coins)
    {
        //Set the currently logged in user
        DatabaseReference DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        string username = GameState.User;

        var DBTask = DBreference.Child("users").Child(username).Child("coins").SetValueAsync(_coins);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //coins is now updated
        }
    }

    private IEnumerator UpdateScore (int _score)
    {
        //Set the currently logged in user
        DatabaseReference DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        var DBTask = DBreference.Child("wolrds").Child(User.UserName).Child("EXP").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Score is now updated
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
