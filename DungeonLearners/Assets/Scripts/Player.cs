public class Player
{
    public int exp;
   // public string Email;
    public string userName;
    // public int Coins;
    //public string User;

    public Player( int exp,string userName )
    {//, User createdBy) {
        this.exp = exp;
        //this.User = user;
        //this.Email = email;
       // this.Coins = coins;
        this.userName = userName;
        // this.createdBy = createdBy;
    }

    public string getUserName()
    {
        return userName;
    }

    //public string getEmail()
    //{
        //return Email;
   // }

    public int getEXP()
    {
        return exp;
    }

    ///public int getCoins()
    //{
    //    return Coins;
   // }
}