using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorldController : MonoBehaviour
{
    // Dynamically spawns objects in world

    public GameObject worldLayer;
    public GameObject worldLayerLast;

    public int dungeonCount = 4;

    private int layerCount = 1;
    private static int dungeonPerLayer = 4;
    private static int worldGridWidth = 22;
    private static int worldGridDepth = 24;
    private static Vector3 worldGridCenter = new Vector3(0, 5, 0);

    private float layerOffset = 7f;
    // Start is called before the first frame update
    void Start()
    {
        GenerateWorldLayers();
        UpdatePathfindingGrid();
    }

    // Spawns layers in world based on number of dungeons 
    private void GenerateWorldLayers() 
    {
        if (dungeonCount < 0) { dungeonCount = 0; }

        // Number of layers in world
        layerCount = Mathf.CeilToInt((float)dungeonCount / dungeonPerLayer);

        if (layerCount == 0) { layerCount = 1; }

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

    // Recalculate A* Pathfinding grid based on world size
    private void UpdatePathfindingGrid()
    {
        Debug.Log("layer count:" + layerCount);
        worldGridDepth = 16 + 8 * (layerCount - 1);
        
        GridGraph grid = AstarPath.active.data.gridGraph;
        grid.SetDimensions(worldGridWidth, worldGridDepth, 1);
        grid.center = new Vector3(0, 1 + 4 * (layerCount - 1), 0);
        AstarPath.active.Scan();
    }
}
