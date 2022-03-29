using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;

public class EditWorldController : MonoBehaviour
{
    public TMP_InputField text1;
    public TMP_InputField desc1;
    string worldName;
    string worldDescription;
    int worldID;
    public GameObject dungeonNameList, dungeonNameEntry;
    void Start()
    {
        World curWorld  = GameState.getCurrentWorld();
        worldName = curWorld.worldName;
        worldDescription = curWorld.description;
        worldID = curWorld.worldID;

        text1.text = worldName;
        desc1.text = worldDescription;

        if(curWorld.getDungeons() != null){
            foreach(var dungeon in curWorld.getDungeons()){
                AddDungeonEntry(dungeon.dungeonName);
            }
        }
    }

    public void AddDungeonEntry(string dungeonName){
        GameObject dEntry = Instantiate(dungeonNameEntry, dungeonNameList.transform);
        TMP_Text dName = dEntry.GetComponentInChildren<TMP_Text>();
        dName.text = dungeonName;
    }

    public async void SaveWorldToDB()
    {
        worldName = text1.text;
        worldDescription = desc1.text;

        World CurWorld = GameState.getCurrentWorld();
        CurWorld.setworldName(worldName);
        CurWorld.setWorldDescription(worldDescription);

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        string worldJSON = JsonUtility.ToJson(CurWorld);
        Debug.Log("Saving world info: " + worldJSON);
        await reference.Child("worlds").Child(worldID.ToString()).SetRawJsonValueAsync(worldJSON);
        
        if (CurWorld.getDungeons() != null){
            List<Dungeon> curDungeons =  CurWorld.getDungeons();
            
            for (var i = 0; i < curDungeons.Count; i++) {
                Dungeon curDungeon  = curDungeons[i];
                string dungeonJSON = JsonUtility.ToJson(curDungeon);
                DatabaseReference dungeonReference =  reference.Child("worlds").Child(worldID.ToString()).Child("Dungeons").Child(i.ToString());
                await dungeonReference.SetRawJsonValueAsync(dungeonJSON);

                if (curDungeon.getDungeonRooms() != null){
                    List<DungeonRoom> curDungeonRooms = curDungeon.getDungeonRooms();
                            
                    for (var j = 0; j < curDungeonRooms.Count; j++) {
                        DungeonRoom curDungeonRoom  = curDungeonRooms[j];
                        string dungeonRoomJSON = JsonUtility.ToJson(curDungeonRoom);
                        DatabaseReference dungeonRoomReference = dungeonReference.Child("DungeonRooms").Child(j.ToString());
                        await dungeonRoomReference.SetRawJsonValueAsync(dungeonRoomJSON);

                        if (curDungeonRoom.getBattleQuestions() != null){
                            List<Question> curQuestions = curDungeonRoom.getBattleQuestions();
                            
                            for (var k = 0; k < curQuestions.Count; k++) {
                                Question curQuestion  = curQuestions[k];
                                string questionJSON = JsonUtility.ToJson(curQuestion);
                                DatabaseReference questionReference = dungeonRoomReference.Child("Questions").Child(k.ToString());
                                await questionReference.SetRawJsonValueAsync(questionJSON);
                            }
                        }
                    }
                }

            }
        }

        SceneManager.LoadScene("World Manager");
    }
}
