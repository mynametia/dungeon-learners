using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // Dynamically spawns layers in world based on number of dungeons 

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

        if (layerCount == 0) { layerCount = 1; }

        Debug.Log("no. of layers: " + layerCount);

        GameObject layer;
        // Procedurally generate layers in world
        for (int i = 0; i < layerCount; i++)
        {
            if (i == layerCount - 1) // Generate last layer
            {
                layer = Instantiate(worldLayerLast, transform);
                if (dungeonCount % dungeonPerLayer != 0)
                {
                    layer.GetComponent<WorldLayerController>().dungeonCount = dungeonCount % dungeonPerLayer;
                }
                else if (dungeonCount != 0)
                {
                    layer.GetComponent<WorldLayerController>().dungeonCount = dungeonPerLayer;
                }
                else
                {
                    layer.GetComponent<WorldLayerController>().dungeonCount = 0;
                }
            }
            else // Generate middle layers
            {
                layer = Instantiate(worldLayer, transform);
                layer.GetComponent<WorldLayerController>().dungeonCount = dungeonPerLayer;
            }
            layer.transform.localPosition = new Vector3(0, i * layerOffset, 0);
        }
    }

}
