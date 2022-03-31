using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class User
{
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