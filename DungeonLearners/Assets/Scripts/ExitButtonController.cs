using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    public GameObject SceneController;
    
    public void OnExitClick()
    {
        // Go back to world
        FindSceneController();
        SceneController.GetComponent<FadeTransitionController>().FadeToBlack("OpenWorld");
        Destroy(gameObject);
    }

    private void FindSceneController()
    {
        SceneController = GameObject.FindWithTag("SceneTransition");
    }
}
