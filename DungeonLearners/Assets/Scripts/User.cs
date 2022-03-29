using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class User
{
    //User Data variables
    [Header("UserData")]
    public TMP_Text currentUser;
    public TMP_InputField updateUsername;
    public TMP_InputField updatedPassword;
    public TMP_Text currentEmail;
    public TMP_Text currentCoins;

   public string UserName;
   public string Email;
   public int NoOfCoins;
   public string UserId;
   public ArrayList WorldList = new ArrayList();

   public User(string _username, string _email, int _coins)
   {
      this.UserName = _username;
      this.Email = _email;
      this.NoOfCoins = _coins;
   }

   public User()
   {

   }

}