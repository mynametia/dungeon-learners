using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    public void OnExitClick()
    {
        Debug.Log("Hello");
        // Go back to world
        // FindSceneController();
        // SceneController.GetComponent<FadeTransitionController>().FadeToBlack("OpenWorld");
    }
}
