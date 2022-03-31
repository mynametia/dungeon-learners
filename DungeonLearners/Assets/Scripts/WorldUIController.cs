
using UnityEngine;

///<summary>
/// Shows world description 
///</summary>
public class WorldUIController : MonoBehaviour
{
    public GameObject worldDescriptionInfo;
    public void ShowWorldDescription()
    {
        worldDescriptionInfo.SetActive(true);
        //player.GetComponent<PlayerMovementController>().enableMove = false;
    }
}
