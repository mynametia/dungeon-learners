using System.Collections.Generic;
using System;
using UnityEngine;
public class World {
    public string worldName;
    public string description;
    public string visibility;
    public int worldID;
    public List<Dungeon> dungeons = new List<Dungeon>();

    public List<Player> players = new List<Player>();


    public World(int worldID, string worldName, string description, string visibility){//, User createdBy) {
        this.worldID = worldID;
        this.worldName = worldName;
        this.description = description; 
        this.visibility = visibility;
        // this.createdBy = createdBy;
    }   

    public string getWorldName(){
        return worldName;
    }

    public string getDescription(){
        return description;
    }

    // public User getCreatedBy(){
    //     return createdBy;
    // }

    public void setworldName(string worldName){
        this.worldName = worldName; 
    }

    public void setWorldDescription(string descr){
        this.description = descr;
    }

    public void setVisibility(string visibility){
        this.visibility = visibility;
    }

    public void setDungeons(List<Dungeon> dungeons){
        this.dungeons = dungeons; 
    }

    public void addDungeon(Dungeon newDungeon){
        Debug.Log(newDungeon.getDungeonName());

        if(dungeons == null){
            dungeons = new List<Dungeon>();
        }

        dungeons.Add(newDungeon);
    }

    public List<Dungeon> getDungeons(){
        return this.dungeons;
    }
}