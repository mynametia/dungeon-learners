using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectCard : MonoBehaviour
{
    public GameObject currentlySelected;
    public GameObject submitButton;
    public TextMeshProUGUI finalAnswer;

    private string promptText = "Pick an answer";
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                            UnhighlightCard(currentlySelected);
                        }
                        // Select new card
                        currentlySelected = touchedObj;
                        HighlightCard(currentlySelected);
                        UpdateAnswer();
                    }
                    // Unselect previously selected card if same card was tapped twice
                    else if (touchedObj == currentlySelected)
                    {
                        UnhighlightCard(currentlySelected);
                        currentlySelected = null;
                        UpdateAnswer();
                    }
                } 
            }   
        }
    }

    // Trigger card selection animation
    public void HighlightCard(GameObject card)
    {
        card.GetComponentInChildren<Animator>().SetBool("Selected", true);
    }

    // Trigger card deselection animation
    public void UnhighlightCard(GameObject card)
    {
        card.GetComponentInChildren<Animator>().SetBool("Selected", false);
    }

    // Update selected answer in answer card
    public void UpdateAnswer()
    {
        if (currentlySelected == null)
        {
            finalAnswer.text = promptText;
            submitButton.SetActive(false);
        }
        else
        {
            finalAnswer.text = currentlySelected.GetComponentInChildren<TextMeshProUGUI>().text;
            submitButton.SetActive(true);
        }
    }
}
