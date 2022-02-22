using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovementController : MonoBehaviour
{
    public GameObject playerMovementDestination;
    public GameObject currentDestination;
    void Start()
    {
        if (currentDestination == null)
        {
            currentDestination = Instantiate(playerMovementDestination);
        }
        currentDestination.transform.position = transform.position;
        //currentDestination.transform.position = new Vector3(-2.54f, -2.95f, 0);
        GetComponent<AIDestinationSetter>().target = currentDestination.transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            currentDestination.transform.position = touchPosition;
        }
    }
}
