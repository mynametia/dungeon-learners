using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// Manages change in user password
/// </summary>
public class ChangeUserPassword : MonoBehaviour
{
    //Firebase Variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth; 

    //Change Password Variables
    [Header("Change Password")]
    public TMP_InputField NewPasswordField;
    public TMP_InputField confirmNewPasswordField;
    public TMP_Text newpasswordWarningText;
    public TMP_Text newpasswordConfirmText;

    /// <summary>
    /// Check that all of the necessary dependencies for Firebase are present on the system
    /// </summary>
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

    /// <summary>
    /// Initialize Firebase
    /// </summary>
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }    

    /// <summary>
    /// Event triggers when change password button is clicked
    /// </summary>
    public void ChangePasswordButton()
    {
        if (NewPasswordField.text != confirmNewPasswordField.text)
        {
            newpasswordWarningText.text = "Passwords do not match, try again!";
        }
        else
        {
            CheckPasswordCondition();
            newpasswordChange(NewPasswordField.text);
            newpasswordConfirmText.text = "Password change successful! Please login again.";
            //auth.SignOut();
            //yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Login");
        }
    }

    /// <summary>
    /// Event triggers when back button is clicked
    /// </summary>
    public void BackButton()
    {
        SceneManager.LoadScene("Profile info (Account Info)");
    }

    /// <summary>
    /// Change password
    /// </summary>
    void newpasswordChange(string NewPasswordField)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user!=null)
        {
            user.UpdatePasswordAsync(NewPasswordField).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("Update Password was cancelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("Update Password was faulted");
                    return;
                }

                Debug.Log("Password Update is successful!");
            });
        }
    }

    /// <summary>
    /// Check if password is good password
    /// </summary>
    public void CheckPasswordCondition()
    {
        string ReceivedString = NewPasswordField.text;
        if (ReceivedString.Any(char.IsLetter) && ReceivedString.Any(char.IsDigit) && ReceivedString.Any(char.IsUpper) && 
        ReceivedString.Any(ch => ! char.IsLetterOrDigit (ch)) && ReceivedString.Length > 8)
        {
            Debug.Log("Password is good, allowed to register");
        }
        else
        {
            Debug.Log("Password is bad, registration is not allowed");
            newpasswordWarningText.text = "Weak Password. Please make sure it contains digits, letters and special characters";
        }
    }
}
