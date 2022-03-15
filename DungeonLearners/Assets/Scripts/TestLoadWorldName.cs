using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestLoadWorldName : MonoBehaviour
{
    public TMP_Text text1;
    string world1;

    void Start()
    {
        world1 = PlayerPrefs.GetString("WorldName1");
        text1.text = world1;
    }
}
