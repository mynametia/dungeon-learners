using UnityEngine;
using TMPro;

public class SelectCard : MonoBehaviour
{
    // Controls card selection mechanism

    public GameObject currentlySelected;

    public bool enableSelect = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enableSelect)
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && enableSelect)
        {
            // Create a ray starting from point of touch on screen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            if (hits.Length > 0)
            {
                // Get the topmost collider
                RaycastHit2D hit = hits[hits.Length - 1];

                GameObject touchedObj = hit.transform.gameObject;

                // Check if collider belonged to a card
                if (touchedObj.tag == "OptionCard") 
                {
                    if (touchedObj != currentlySelected)
                    {
                        // Unselect previously selected card
                        if (currentlySelected != null)
                        {
                            UnhighlightCard();
                        }
                        // Select new card
                        currentlySelected = touchedObj;
                        HighlightCard();
                    }
                    // Unselect previously selected card if same card was tapped twice
                    else if (touchedObj == currentlySelected)
                    {
                        UnhighlightCard();
                        currentlySelected = null;
                    }
                    // Update final answer card
                    GetComponent<BattleQuestionController>().updateFinalAnswer(currentlySelected);
                } 
            }   
        }
    }

    // Trigger card selection animation
    public void HighlightCard()
    {
        if (currentlySelected != null)
        {
            currentlySelected.GetComponentInChildren<Animator>().SetBool("Selected", true);
        }
    }

    // Trigger card deselection animation
    public void UnhighlightCard()
    {

        if (currentlySelected != null)
        {
            currentlySelected.GetComponentInChildren<Animator>().SetBool("Selected", false);
        }
    }

    public void deactivateChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
