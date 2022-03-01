using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovementController : MonoBehaviour
{
    // Controlls player movement 

    public GameObject playerMovementDestination;
    public GameObject currentDestination;
    void Start()
    {
        if (currentDestination == null)
        {
            currentDestination = Instantiate(playerMovementDestination);
        }
        currentDestination.transform.position = transform.position;

        // Set A* pathfinding destination to track position of currentDestination
        GetComponent<AIDestinationSetter>().target = currentDestination.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Set position of currentDestination to worldspace position of touch input
            // A* destination will automatically be updated
            currentDestination.transform.position = touchPosition;
        }
    }
}
