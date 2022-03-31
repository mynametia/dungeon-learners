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
/// Display the user's information
/// </summary>
public class DisplayUserInfo : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public DatabaseReference DBreference;

    //User Data variables
    [Header("UserData")]
    public TMP_Text username;
    public TMP_Text email;
    public TMP_Text coins;
    public TMP_Text AccountUsername;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }    

    private IEnumerator Start()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        // DBreference.Child("users").Child(user.UserId).GetValueAsync().ContinueWith(task => 
        // {  
        //     Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        //     DataSnapshot snapshot = task.Result;
        //     username.text = snapshot.Child("username").Value.ToString();
        //     email.text = snapshot.Child("email").Value.ToString();
        //     coins.text = snapshot.Child("coins").Value.ToString();                   
        // });   
        var DBTask = DBreference.Child("users").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            username.text = snapshot.Child("username").Value.ToString();
            AccountUsername.text = snapshot.Child("username").Value.ToString();
            email.text = snapshot.Child("email").Value.ToString();
            coins.text = "Number of Coins: " + snapshot.Child("coins").Value.ToString();
        }
    }
}
