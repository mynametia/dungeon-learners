using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLayerController : MonoBehaviour
{
    // Dynamically spawns dungeons per layer
    public int worldLayerID = 0; // ID of world layer, goes from 0 to layerCount - 1

    public int dungeonCount = 0;
    private static int maxDungeonCount = 4, dungeonEntranceOffset = 5, firstDungeonXPos = -8, dungeonYPos = 7;

    public GameObject groundLayer;
    public GameObject dungeonEntrance;

    // Start is called before the first frame update
    void Start()
    {
        if (dungeonCount > maxDungeonCount) { dungeonCount = maxDungeonCount; }
        else if (dungeonCount < 0) { dungeonCount = 0; }
        
        // Instantiate dungeon entrances from left to right
        int count = 0;
        GameObject entrance;
        while (count < dungeonCount)
        {
            entrance = Instantiate(dungeonEntrance, groundLayer.transform);
            entrance.transform.localPosition = new Vector3(firstDungeonXPos + count * dungeonEntranceOffset, dungeonYPos, 0);
            entrance.GetComponent<DungeonEntranceController>().dungeonID = worldLayerID*maxDungeonCount + count;
            count++;
        }

    }

}
