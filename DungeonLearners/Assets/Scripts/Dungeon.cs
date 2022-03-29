using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon
{
    public string dungeonName;
    private List<DungeonRoom> dungeonRooms = new List<DungeonRoom>();

    public Dungeon(){}
    public Dungeon(string dungeonName){
        this.dungeonName = dungeonName;
    }

    public Dungeon(string dungeonName, List<DungeonRoom> dungeonRooms){
        this.dungeonName = dungeonName;
        this.dungeonRooms = dungeonRooms;
    }

    public void addDungeonRoom (DungeonRoom newDungeonRoom){
        dungeonRooms.Add(newDungeonRoom);
    }

    public string getDungeonName(){
        return dungeonName;
    }

    public List<DungeonRoom> getDungeonRooms(){
        return this.dungeonRooms;
    } 

}
