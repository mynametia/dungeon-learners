using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomebaseController : MonoBehaviour
{
    // Detects user selecting find world/my world/shop options

    public GameObject SceneController;
    public GameObject Player;
    public GameObject FindWorld;

    private float worldEntryMaxDist = 0.3f;
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
            {
                // Create a ray starting from point of touch on screen
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

                if (hits.Length > 0)
                {
                    // Get the topmost collider
                    RaycastHit2D hit = hits[hits.Length - 1];

                    GameObject touchedObj = hit.transform.gameObject;

                    if (touchedObj.tag == "FindWorld")
                    {
                        // spawn find worlds UI
                        StartCoroutine(EnterWorld(touchedObj, "OpenWorld", 1f));
                    }
                    else if (touchedObj.tag == "MyWorlds")
                    {
                        // spawn world manager UI
                        StartCoroutine(EnterWorld(touchedObj, "World Manager", 1.8f));
                    }
                    else if (touchedObj.tag == "Shop")
                    {
                        // spawn shop UI
                    }
                }

            }
        }
    }

    // Triggers when find world portal is tapped
    private IEnumerator EnterWorld(GameObject obj, string sceneName, float radius)
    {
        Player.GetComponent<PlayerMovementController>().SetCurrentDestination(obj.transform.position);

        yield return new WaitForSeconds(0.05f);

        int count = 0;
        while (Vector3.Distance(Player.transform.position, obj.transform.position) > radius)
        {
            yield return new WaitForSeconds(.1f);
            count++;
            if (count > 80)
            {
                yield break;
            }
        }
        SceneController.GetComponent<FadeTransitionController>().FadeToBlack(sceneName);
        //// Wait for player to stop walking
        //while (Player.GetComponent<PlayerMovementController>().moving)
        //{
        //    yield return new WaitForSeconds(.1f);
        //}
        //Debug.Log(sceneName + " obj clicked stopped walking");
        //// If player is standing above world entrance, enter dungeon
        //if (Vector3.Distance(Player.transform.position, obj.transform.position) <= radius)
        //{
        //    SceneController.GetComponent<FadeTransitionController>().FadeToBlack(sceneName);
        //}
        yield return null;
    }
}
