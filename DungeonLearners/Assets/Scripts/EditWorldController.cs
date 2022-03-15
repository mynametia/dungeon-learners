using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditWorldController : MonoBehaviour
{
    public TMP_InputField text1;
    public TMP_InputField desc1;
    string world1;
    string worlddesc1;

    void Start()
    {
        world1 = PlayerPrefs.GetString("WorldName1");
        text1.text = world1;
        worlddesc1 = PlayerPrefs.GetString("WorldDesc1");
        desc1.text = worlddesc1;
    }
}
