using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorldController : MonoBehaviour
{
    // Dynamically spawns objects in world
    // Detects user selecting dungeon
    public GameObject Player;
    public GameObject SceneController;

    public GameObject worldLayer;
    public GameObject worldLayerLast;

    public int dungeonCount = 4;

    private int layerCount = 1;
    private static int dungeonPerLayer = 4;
    private static int worldGridWidth = 22;
    private static int worldGridDepth = 24;

    private float layerOffset = 7f;

    private float dungeonEntryMaxDist = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        // Dynamically spawns objects in world
        GenerateWorldLayers();
        UpdatePathfindingGrid();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Create a ray starting from point of touch on screen
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            if (hits.Length > 0)
            {
                // Get the topmost collider
                RaycastHit2D hit = hits[hits.Length - 1];

                GameObject touchedObj = hit.transform.gameObject;

                if (touchedObj.tag == "DungeonEntrance")
                {
                    // Enter dungeon
                    StartCoroutine(EnterDungeon(touchedObj));
                }
            }
        }
    }

    private IEnumerator EnterDungeon(GameObject dungeonEntrance)
    {
        Player.GetComponent<PlayerMovementController>().SetCurrentDestination(dungeonEntrance.transform.position);

        yield return new WaitForSeconds(0.05f);

        // Wait for player to stop walking
        while (Player.GetComponent<PlayerMovementController>().moving)
        { 
            yield return new WaitForSeconds(.1f);
        }

        // If player is standing above dungeon entrance, enter dungeon
        if (Vector3.Distance(Player.transform.position, dungeonEntrance.transform.position) <= dungeonEntryMaxDist)
        {
            dungeonEntrance.GetComponent<DungeonEntranceController>().EnterDungeon();
            SceneController.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");
            PlayerPrefs.SetFloat("PlayerWorldX", dungeonEntrance.transform.position.x);
            PlayerPrefs.SetFloat("PlayerWorldY", dungeonEntrance.transform.position.y);
        }

        yield return null;
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
            layer.GetComponent<WorldLayerController>().worldLayerID = i;
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
