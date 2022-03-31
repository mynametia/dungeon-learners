//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Editor controls for dungeons
/// </summary>
public class EditDungeonController : MonoBehaviour
{
    
    public GameObject DungeonRoomField, DungeonInfo, AddDungeonRoomButton;

    private int oldChildCount;

    void Start()
    {
        UpdateDungeonRoomCount();
    }

    void Update()
    {
        if (DungeonInfo.transform.childCount < oldChildCount)
        {
            UpdateDungeonRoomCount();
            for (int i = 1; i < oldChildCount - 1; ++i)
            {
                DungeonInfo.transform.GetChild(i).gameObject.GetComponent<EditDungeonRoomController>().UpdateDungeonRoomNumber();
            }

        }
        else if (DungeonInfo.transform.childCount > oldChildCount)
        {
            UpdateDungeonRoomCount();
        }
    }

    public void AddDungeonRoomField()
    {
        Instantiate(DungeonRoomField, DungeonInfo.transform);
        AddDungeonRoomButton.transform.SetAsLastSibling();
    }

    private void UpdateDungeonRoomCount()
    {
        oldChildCount = DungeonInfo.transform.childCount;
    }
}
