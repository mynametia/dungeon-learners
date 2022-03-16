using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen
    public GameObject loginUI;
    public GameObject registerUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    // Change the screens
    public void LoginScreen()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }

    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
}
