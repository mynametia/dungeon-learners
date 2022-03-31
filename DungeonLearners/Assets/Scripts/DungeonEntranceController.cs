using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages dungeon entrances
/// </summary>
public class DungeonEntranceController : MonoBehaviour
{
    // Keeps track of which dungeon entrance leads to which dungeon 

    public int dungeonID = 0; //dungeon ID goes from 0 to totalDungeons - 1
    public GameObject DungeonController;
    public void EnterDungeon()
    {
        // Creates DungeonController (which persists across dungeon rooms) to control traversing dungeon rooms
        Instantiate(DungeonController);
    }
}
