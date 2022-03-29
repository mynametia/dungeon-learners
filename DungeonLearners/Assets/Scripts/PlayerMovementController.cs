using UnityEngine;
using Pathfinding;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour
{
    // Controlls player movement 
    public bool moving = false, enableMove = true;

    public GameObject playerMovementDestination;
    public GameObject currentDestination;
    public AIPath aiPath;
    void Start()
    {
        if (currentDestination == null)
        {
            currentDestination = Instantiate(playerMovementDestination);
        }
        currentDestination.transform.position = transform.position;

        // Set A* pathfinding destination to track position of currentDestination
        GetComponent<AIDestinationSetter>().target = currentDestination.transform;
        aiPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        UpdateMoving();
    }

    private void UpdateMoving()
    {
        if (aiPath.desiredVelocity.magnitude == 0)
        {
            moving = false;
        }
        else 
        {
            moving = true;
        }
    }

    private void MovePlayer()
    {
        //if (enableMove && Input.GetMouseButtonDown(0))
        if (enableMove && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(0) && touch.phase == TouchPhase.Began)
            {
                //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                // Set position of currentDestination to worldspace position of touch input
                // A* destination will automatically be updated
                SetCurrentDestination(touchPosition);
            }
 
        }
    }

    public void SetCurrentDestination(Vector3 touchPosition)
    {
        currentDestination.transform.position = touchPosition;
    }

}
