using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomebaseController : MonoBehaviour
{
    // Detects user selecting find world/my world/shop options

    public GameObject SceneController;

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
                
                if (touchedObj.tag == "FindWorld")
                {
                    // spawn find worlds UI
                    SceneController.GetComponent<FadeTransitionController>().FadeToBlack("OpenWorld");
                }
                else if (touchedObj.tag == "MyWorlds")
                {
                    // spawn world manager UI
                }
                else if (touchedObj.tag == "Shop")
                {
                    // spawn shop UI
                }
            }
        }
    }
}
