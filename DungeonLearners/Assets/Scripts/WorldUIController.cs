using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIController : MonoBehaviour
{
    public GameObject worldDescriptionInfo;

    public void ShowWorldDescription()
    {
        worldDescriptionInfo.SetActive(true);
    }
}
