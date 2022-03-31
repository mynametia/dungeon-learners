using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Modification of PlayerPrefs
/// </summary>
public class ModifyPlayerPrefs : MonoBehaviour
{
    public TMP_Text preloadWorldName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPreloadWorldChoice()
    {
       Debug.Log(preloadWorldName.text);
       PlayerPrefs.SetString("preloadWorldChoice", preloadWorldName.text);
    }
}
