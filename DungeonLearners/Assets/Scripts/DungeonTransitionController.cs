using UnityEngine;

public class DungeonTransitionController : MonoBehaviour
{
    public GameObject DungeonController;
    // Detects when player enters room entrance/exit collider
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DungeonController = GameObject.FindWithTag("SceneTransition");
            if (gameObject.tag == "RoomEntrance")
            {
                DungeonController.GetComponent<DungeonController>().GoPreviousRoom();
            }
            else
            {
                DungeonController.GetComponent<DungeonController>().GoNextRoom();
            }
        }
 
    }
}
