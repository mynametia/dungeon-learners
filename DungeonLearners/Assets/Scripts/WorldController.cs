using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.EventSystems;
using TMPro;

///<summary>
/// Synamically spawn objects in world and call update once per frame
///</summary>
public class WorldController : MonoBehaviour
{
    // Dynamically spawns objects in world
    // Detects user selecting dungeon
    public GameObject Player;
    public GameObject SceneController;

    public GameObject worldLayer;
    public GameObject worldLayerLast;

    public GameObject DungeonNameUIText; // Added by Ziyuan
    public TMP_Text worldNameText;

    public int dungeonCount = 8;

    private int layerCount = 1;
    private static int dungeonPerLayer = 4;
    private static int worldGridWidth = 22;
    private static int worldGridDepth = 24;

    private float layerOffset = 7f;

    private float dungeonEntryMaxDist = 1f;
    private float dungeonEntranceRadius = 1.7f;

    private List<string> computingDungeonNamelist = new List<string> {"Artificial Intelligence", "Human Computer Interaction", "Software Engineering"};
    private List<string> ethicsDungeonNamelist = new List<string> {"Deontology", "General Ethics", "Research Ethics"};
    private List<string> writingDungeonNamelist = new List<string> {"Formal Writing I", "Formal Writing II"};

    private string worldName;
    private List<string> currentDungeonList;

    // Start is called before the first frame update
    void Start()
    {
        // Dynamically spawns objects in world
        GenerateWorldLayers();
        UpdatePathfindingGrid();
        DungeonNameUIText.SetActive(false); // Added by Ziyuan

        worldName = PlayerPrefs.GetString("preloadWorldChoice");
        worldNameText.text = worldName;
        switch (worldName)
        {
            case "Computing":
                currentDungeonList = computingDungeonNamelist;
                break;
            case "Ethics":
                currentDungeonList = ethicsDungeonNamelist;
                break;
            case "Formal Writing":
                currentDungeonList = writingDungeonNamelist;
                break;
            default:
                currentDungeonList = new List<string> {"","","","","", "", "", ""};
                break;
        }

        dungeonCount = currentDungeonList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(0) && touch.phase == TouchPhase.Began)
            {
                // Create a ray starting from point of touch on screen
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
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
    }

    private IEnumerator EnterDungeon(GameObject dungeonEntrance)
    {
        DungeonNameUIText.SetActive(true); // Added by Ziyuan

        // Set name of dungeon
        DungeonNameUIText.GetComponent<TextMeshProUGUI>().text = currentDungeonList[dungeonEntrance.GetComponent<DungeonEntranceController>().dungeonID];

        Player.GetComponent<PlayerMovementController>().SetCurrentDestination(dungeonEntrance.transform.position);

        yield return new WaitForSeconds(0.05f);

        // Wait for player to stop walking
        while (Player.GetComponent<PlayerMovementController>().moving)
        { 
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("Entrance");
        // If player is standing above dungeon entrance, enter dungeon
        if (Vector3.Distance(Player.transform.position, dungeonEntrance.transform.position) <= dungeonEntryMaxDist)
        {
            PlayerPrefs.SetString("preloadDungeonChoice", currentDungeonList[dungeonEntrance.GetComponent<DungeonEntranceController>().dungeonID]);
            dungeonEntrance.GetComponent<DungeonEntranceController>().EnterDungeon();
            SceneController.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");
            PlayerPrefs.SetFloat("PlayerWorldX", dungeonEntrance.transform.position.x);
            PlayerPrefs.SetFloat("PlayerWorldY", dungeonEntrance.transform.position.y);
        }
        else if (Vector3.Distance(Player.transform.position, dungeonEntrance.transform.position) > dungeonEntranceRadius)
        {
            DungeonNameUIText.SetActive(false);
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
