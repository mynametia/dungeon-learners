using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomController : MonoBehaviour
{
    // Control interactions in dungeon room
    public GameObject DungeonBoss;
    public GameObject Player;
    public GameObject SceneController;

    public float bossInteractionMaxDist = 2f;
    
    
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Create a ray starting from point of touch on screen
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            if (hits.Length > 0)
            {
                // Get the topmost collider
                RaycastHit2D hit = hits[hits.Length - 1];

                GameObject touchedObj = hit.transform.gameObject;

                if (touchedObj.tag == "Boss")
                {
                    // Enter dungeon
                    StartCoroutine(EnterBattle());
                }
            }
        }
    }


    // Enters battle with boss
    private IEnumerator EnterBattle()
    {
        Player.GetComponent<PlayerMovementController>().SetCurrentDestination(new Vector3(DungeonBoss.transform.position.x, DungeonBoss.transform.position.y - 0.75f * bossInteractionMaxDist, 0));

        yield return new WaitForSeconds(0.05f);

        // If player is standing near boss, enter card battle scene
        if (Vector3.Distance(Player.transform.position, DungeonBoss.transform.position) <= bossInteractionMaxDist)
        {
            SceneController.GetComponent<FadeTransitionController>().FadeToBlack("CardBattle");
        }

        yield return null;
    }
}
