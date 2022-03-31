using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dungeon Room class
/// </summary>
public class DungeonRoom
{
    public int levelNumber;
    private List<Question> battleQuestions = new List<Question>();

    public DungeonRoom(int levelNumber){
        this.levelNumber = levelNumber;
    }

    public DungeonRoom(int levelNumber, List<Question> battleQuestions){
        this.levelNumber = levelNumber;
        this.battleQuestions = battleQuestions;
    }

    public List<Question> getBattleQuestions(){
        return this.battleQuestions;
    }
}



