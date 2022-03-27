using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using TMPro;

public class FirebaseManager : MonoBehaviour
{

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField confirmpasswordRegisterField;
    public TMP_Text warningRegisterText;



    private void Awake()
    {
        //Check for necessary dependencies for Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //initialise Firebase
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
        //Set the Firebase authentication instance object
        auth = FirebaseAuth.DefaultInstance;

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != User)
        {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && User != null)
            {
                Debug.Log("Signed Out " + User.UserId);
            }

            User = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed In " + User.UserId);
            }
        }
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine to pass the username and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    //Funtion for the register button
    public void RegisterButton()
    {
        //Call the login coroutine to pass the email, username and password
        StartCoroutine(Register(emailRegisterField.text, usernameRegisterField.text, passwordRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //Error handling
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing email!";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing password!";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong password!";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid email!";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist...";
                    break;
            }
            
            warningLoginText.text = message;
        }
        else
        {
            //User is logged in
            User = LoginTask.Result;
            Debug.LogFormat("User has signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            //Bring to profile page
            UnityEngine.SceneManagement.SceneManager.LoadScene("HomeBase");
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //show warning if username is blank
            warningRegisterText.text = "Missing username!";
        }
        // else if (_email == "")
        // {
        //     //show warning if email is blank
        //     warningRegisterText.text = "Missing email!";
        // }
        else if (passwordRegisterField.text != confirmpasswordRegisterField.text)
        {
            //if passwords don't match
            warningRegisterText.text = "Passwords do not match!";
        }
        else
        {
            //call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //Error handling
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Registration failed!";
                switch(errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing email!";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing password!";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak password!";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Account with this email has already been created...";
                        break;                    
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create user profile and username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call Firebase auth to update the user profile function passing the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //Delete the user if user update failed
                        //User.DeleteAsync();

                        //Error handling
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username set failed";
                    }
                    else
                    {
                        //username is set
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        Debug.Log("Registration Successful, Welcome "+ User.DisplayName);
                        //Bring to create profile page
                        //UnityEngine.SceneManagement.SceneManager.LoadScene("Profile info (Account Info)");
                    }
                }
            }
        } 
    }
}

