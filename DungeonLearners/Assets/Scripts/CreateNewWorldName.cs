using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateNewWorldName : MonoBehaviour
{
    public TMP_InputField worldName1;
    public TMP_InputField worldDesc1;
    string world_nameOne;
    string world_descOne;

    public void SaveWorldName()
    {
        world_nameOne = worldName1.text;
        world_descOne = worldDesc1.text;
        PlayerPrefs.SetString("WorldDesc1", world_descOne);
        PlayerPrefs.SetString("WorldName1", world_nameOne); 
        SceneManager.LoadScene(4);

    }

 
}
