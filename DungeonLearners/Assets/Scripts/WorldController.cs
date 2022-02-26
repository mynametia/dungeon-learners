using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // Spawns dungeons and layers in world based on number of dungeons 

    public GameObject worldLayer;
    public GameObject worldLayerLast;

    public int dungeonCount = 4;

    private int layerCount;
    private static int dungeonPerLayer = 4;

    private float layerOffset = 7f;
    // Start is called before the first frame update
    void Start()
    {
        if (dungeonCount < 0) { dungeonCount = 0; }
        
        // Number of layers in world
        layerCount = Mathf.CeilToInt((float) dungeonCount / dungeonPerLayer);

        Debug.Log("no. of layers: " + layerCount);

        GameObject layer;
        // Procedurally generate layers in world
        for (int i = 0; i < layerCount; i++)
        {
            int layerDungeonCount;
            if (i == layerCount - 1) // Generate last layer
            {
                layerDungeonCount = dungeonCount % dungeonPerLayer;
                layer = Instantiate(worldLayerLast, transform);
            }
            else // Generate middle layers
            {
                layerDungeonCount = dungeonPerLayer;
                layer = Instantiate(worldLayer, transform);
            }
            layer.transform.localPosition = new Vector3(0, i * layerOffset, 0);
        }
    }

}
