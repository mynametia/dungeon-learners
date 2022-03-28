using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject verifyEmailUI;
    public TMP_Text verifyEmailText;

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
        verifyEmailUI.SetActive(false);
    }

    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
        verifyEmailUI.SetActive(false);
    }

    public void AwaitVerification(bool _emailSent, string _email, string _output)
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        verifyEmailUI.SetActive(true);        
        if (_emailSent)
        {
            verifyEmailText.text = $"Sent Email!\nPlease Verify {_email}";
        }
        else
        {
            verifyEmailText.text = $"Email not sent: {_output}\nPlease Verify {_email}";
        }
    }
}
