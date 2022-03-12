using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    public GameObject SceneController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExitButtonClick()
    {
        Debug.Log("Hello");
        // Go back to world
        FindSceneController();
        SceneController.GetComponent<FadeTransitionController>().FadeToBlack("OpenWorld");
    }

    private void FindSceneController()
    {
        SceneController = GameObject.FindWithTag("SceneTransition");
    }
}
