using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// UserData class
/// </summary>
public class UserData : MonoBehaviour
{

    //User Data variables
    [Header("UserData")]
    public TMP_Text currentUser;
    public TMP_InputField updateUsername;
    public TMP_InputField updatedPassword;
    public TMP_Text currentEmail;
    public TMP_Text currentCoins;

    public static string username;
    public static string email;
    public static string coins;    

    void Start()
    {
        currentUser.text = username;
        currentEmail.text = email;
        currentCoins.text = "Number of Coins: " + coins;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}