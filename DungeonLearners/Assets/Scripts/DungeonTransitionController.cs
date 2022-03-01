using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonTransitionController : MonoBehaviour
{
    public GameObject fadeTransition;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Nextscene!");
            fadeTransition.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");
        }
 
    }
}
