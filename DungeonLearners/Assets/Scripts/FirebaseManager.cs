using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;
    public DatabaseReference DBreference;

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
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //ForgetPassword variables
    [Header("Forget Password")]
    public TMP_InputField emailForgetField;
    public TMP_Text warningResetText;

    //User Data variables
    [Header("UserData")]
    public TMP_Text username;
    public TMP_InputField updateUsername;
    public TMP_InputField updatedPassword;
    public TMP_Text email;
    public TMP_Text coins;

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

    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    public void LogOutButton()
    {
        auth.SignOut();
        //UIManager.instance.LoginScreen();
        SceneManager.LoadScene("Login");
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }

    public void ChangePasswordAccountInfoButton()
    {
        SceneManager.LoadScene("Change Password");
    }

    public void ForgetPassword()
    {
        if (emailForgetField.text == "")
        {
            warningResetText.text = "Please enter your email!";
        }
        else
        {
            forgetPasswordSubmit(emailForgetField.text);
            warningResetText.text = "Successfully sent reset email!";
        }
    }

    public void UpdateUserNameButton()
    {
        //StartCoroutine(UpdateUsernameAuth(username.text));
        StartCoroutine(UpdateUsernameDatabase(username.text));
    }

    // public void ProfileButton()
    // {
    //     DBreference.Child("users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => 
    //     {
    //         Firebase.Auth.FirebaseUser User = auth.CurrentUser;  
    //         Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
    //         DataSnapshot snapshot = task.Result;
    //         UserData.username = snapshot.Child("username").Value.ToString();
    //         UserData.email = snapshot.Child("email").Value.ToString();
    //         UserData.coins = snapshot.Child("coins").Value.ToString();        
    //     });         
    // }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
                case AuthError.UnverifiedEmail:
                    message = "Email is not verified";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //if (User.IsEmailVerified)
            //{
                //User is now logged in
                //Now get the result
                User = LoginTask.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);

                warningLoginText.text = "";
                confirmLoginText.text = "Logged In";

                //StartCoroutine(LoadUserData());
                //username.text = UserData.username;
                //StartCoroutine(LoadUserData(auth.CurrentUser)); 
                yield return new WaitForSeconds(2);

                // DBreference.Child("users").GetValueAsync().ContinueWithOnMainThread(task => {
                //     if (task.IsFaulted) 
                //     {
                //         Debug.Log("Could Not Read Data from DB");
                //     }

                //     else if (task.IsCompleted) 
                //     {
                //         DataSnapshot snapshot = task.Result;
                //         foreach(var child in snapshot.Children) 
                //         {
                //             //User user = JsonUtility.FromJson<User>(child.GetRawJsonValue());
                //             if(_email.CompareTo(DBreference.Child("users").Child("email").GetValueAsync())==0)
                //             {
                //                 //GameState.setCurrentUser(user);
                //                 //var username = childSnapshot.Child ("name").Value.ToString ();
                //                 UserData.username = DBreference.Child("users").Child("username").GetValueAsync().ToString();
                //                 UserData.email = DBreference.Child("users").Child("email").GetValueAsync().ToString();

                //                 break;
                //             }
                //         }
                //     }
                // });
                
                // DBreference.Child("users").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWith(task => 
                // {
                //     Firebase.Auth.FirebaseUser User = auth.CurrentUser;  
                //     Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
                //     DataSnapshot snapshot = task.Result;
                //     UserData.username = snapshot.Child("username").Value.ToString();
                //     UserData.email = snapshot.Child("email").Value.ToString();
                //     UserData.coins = snapshot.Child("coins").Value.ToString();
                    
                // });   

                SceneManager.LoadScene("HomeBase"); // Change to Homebase scene
                
                confirmLoginText.text = "";
                ClearLoginFeilds();
                ClearRegisterFeilds();
            // }
            // else
            // {
            //     StartCoroutine(SendVerificationEmail());
        }
    }
    //}

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else 
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
                CheckPasswordCondition();
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        // User user = new User();
                        // UserData.username = usernameRegisterField.text;
                        // user.Email = emailRegisterField.text;
                        // user.NoOfCoins = 0;
                        
                        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(usernameRegisterField.text);
                        DBTask = DBreference.Child("users").Child(User.UserId).Child("email").SetValueAsync(emailRegisterField.text);
                        DBTask = DBreference.Child("users").Child(User.UserId).Child("coins").SetValueAsync("0");

                        // string json = JsonUtility.ToJson(user);
                        // DBreference.Child("users").Child(User.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
                        // {
                        //     if(task.IsCompleted)
                        //     {
                        //         Debug.Log("Successfully added data to firebase");
                        //         warningRegisterText.text = "User has been created!";
                        //     }
                        //     else
                        //     {
                        //         Debug.Log("Adding data to firebase not successful");
                        //         warningRegisterText.text = "Not able to create user";
                        //     }
                        // });

                        //UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        StartCoroutine(SendVerificationEmail());
                    }
                }
            }
        }
    }

    // private IEnumerator UpdateUsernameAuth(string _username)
    // {
    //     //Create a user profile and set the username
    //     UserProfile profile = new UserProfile { DisplayName = _username };

    //     //Call the Firebase auth update user profile function passing the profile with the username
    //     var ProfileTask = User.UpdateUserProfileAsync(profile);
    //     //Wait until the task completes
    //     yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

    //     if (ProfileTask.Exception != null)
    //     {
    //         Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
    //     }
    //     else
    //     {
    //         //Auth username is now updated
    //     }        
    // }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(UserData.username).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateCoins(int _coins)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(UserData.username).Child("coins").SetValueAsync(_coins);

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


    private IEnumerator LoadUserData(Firebase.Auth.FirebaseUser User)
    {
        //Get the currently logged in user data
        //Firebase.Auth.FirebaseUser User = auth.CurrentUser;
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

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
            email.text = snapshot.Child("email").Value.ToString();
            coins.text = "Number of Coins: " + snapshot.Child("coins").Value.ToString();
        }
    }

    public void CheckPasswordCondition()
    {
        string ReceivedString = passwordRegisterField.text;
        if (ReceivedString.Any(char.IsLetter) && ReceivedString.Any(char.IsDigit) && ReceivedString.Any(char.IsUpper) && 
        ReceivedString.Any(ch => ! char.IsLetterOrDigit (ch)) && ReceivedString.Length > 8)
        {
            Debug.Log("Password is good, allowed to register");
        }
        else
        {
            Debug.Log("Password is bad, registration is not allowed");
            warningRegisterText.text = "Weak Password. Please make sure it contains digits, letters and special characters";
        }
    }

    private IEnumerator SendVerificationEmail()
    {
        if(User != null)
        {
            //Call the firebase to do email verification
            var emailTask = User.SendEmailVerificationAsync();
            //Wait until the email task completes
            yield return new WaitUntil(predicate: () => emailTask.IsCompleted);
            //If there are errors
            if(emailTask.Exception != null)
            {
                FirebaseException firebaseEx = emailTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string output = "Unknown Error, Try again!";

                switch (errorCode)
                {
                    case AuthError.Cancelled:
                        output = "Verification Task was cancelled";
                        break;
                    case AuthError.InvalidRecipientEmail:
                        output = "Invalid Email!";
                        break;
                    case AuthError.TooManyRequests:
                        output = "Too Many Requests!";
                        break;
                }

                UIManager.instance.AwaitVerification(false, User.Email, output);
            }
            else
            {
                UIManager.instance.AwaitVerification(true, User.Email, null);
                Debug.Log("Email Sent Successfully");
            }
        }
    }

    void forgetPasswordSubmit(string emailForgetField)
    {
        auth.SendPasswordResetEmailAsync(emailForgetField).ContinueWithOnMainThread(task => {

            if (task.IsCanceled)
            {
                Debug.LogError("Sending reset email is cancelled");
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Sending reset email encountered an error: " + task.Exception);
            }
        });
    }

}