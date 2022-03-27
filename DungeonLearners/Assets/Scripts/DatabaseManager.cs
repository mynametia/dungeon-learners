using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DatabaseReference DBreference;
    //public FirebaseUser User;

    //Create variables
    [Header("Create")]
    public TMP_InputField usernameCreateField;
    public TMP_InputField emailCreateField;
    public TMP_InputField passwordCreateField;
    public TMP_Text warningCreateText;

    void Start()
    {
        Debug.Log("Setting up Firebase Database");
        //set database instance object
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        // emailCreateField.text = emailRegisterField.text;
        // passwordCreateField.text = passwordRegisterField.text;
        User user = new User();
        user.UserName = usernameCreateField.text;
        user.Email = emailCreateField.text;
        user.PassWord = passwordCreateField.text;
        string json = JsonUtility.ToJson(user);

        DBreference.Child("Users").Child(user.UserName).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if(task.IsCompleted)
            {
                Debug.Log("Successfully added data to firebase");
                warningCreateText.text = "User has been created!";
            }
            else
            {
                Debug.Log("Adding data to firebase not successful");
                warningCreateText.text = "Not able to create user";
            }
        }
        );
        SceneManager.LoadScene("Login");
    }

}
