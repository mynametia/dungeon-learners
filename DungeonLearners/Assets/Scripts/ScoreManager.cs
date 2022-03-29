using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score, coins;

    public TextMeshProUGUI scoreText, coinText;
    
    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        score = PlayerPrefs.GetInt("EXP");
        coins = PlayerPrefs.GetInt("Coins");

        scoreText.text = score.ToString();
        coinText.text = coins.ToString();

        StartCoroutine(UpdateCoins(coins));
        StartCoroutine(UpdateScore(score));
    }

    private IEnumerator UpdateCoins (int _coins)
    {
        //Set the currently logged in user
        DatabaseReference DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        User user = GameState.getCurrentUser();
        string username = user.UserName;

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

        User user = GameState.getCurrentUser();
        string username = user.UserName;

        var DBTask = DBreference.Child("wolrds").Child(user.UserName).Child("EXP").SetValueAsync(_score);

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

}
