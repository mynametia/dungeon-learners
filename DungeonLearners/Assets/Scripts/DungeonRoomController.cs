using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomController : MonoBehaviour
{
    // Control interactions in dungeon room
    public GameObject DungeonBoss;
    public GameObject Player;
    public GameObject SceneController;

    public float bossInteractionMaxRange = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    private IEnumerator EnterBattle()
    {
        Player.GetComponent<PlayerMovementController>().SetCurrentDestination(DungeonBoss.transform.position);

        // Wait for player to stop walking
        while (Player.GetComponent<PlayerMovementController>().moving)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (Vector3.Distance(Player.transform.position, DungeonBoss.transform.position) <= bossInteractionMaxRange)
        {
            SceneController.GetComponent<FadeTransitionController>().FadeToBlack("CardBattle");
            PlayerPrefs.SetFloat("PlayerDungeonX", Player.transform.position.x);
            PlayerPrefs.SetFloat("PlayerDungeonY", Player.transform.position.y);
        }

        yield return null;
    }
}
