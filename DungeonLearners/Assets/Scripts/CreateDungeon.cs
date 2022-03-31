using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;

/// <summary>
/// Create dungeon
/// </summary>
public class CreateDungeon : MonoBehaviour
{
    //Firebase Variables
    public DatabaseReference DBreference;

    [Header("Dungeon Information")]
    public TMP_InputField dungeonNameField;
    public GameObject DungeonList;

    void Start()
    {
        Debug.Log("Setting up Firebase Database");
        //set database instance object
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void SaveDungeon()
    {
        string dungeonName = dungeonNameField.text;

        List<DungeonRoom> dungeonRooms = new List<DungeonRoom>();
        int numDungeonRooms = DungeonList.transform.childCount - 2 ; // -2 since also have Add Button & Dungeon Name
        

        for (int i = 1; i <= numDungeonRooms; i++){
        
            string dRoomName = "ROOM " + i.ToString(); 
            GameObject dRoom = DungeonList.transform.GetChild(i).gameObject;

            List<Question> battleQuestions = new List<Question>();
            GameObject questionsObject = dRoom.transform.GetChild(1).GetChild(0).gameObject;
            int numQuestions = questionsObject.transform.childCount - 2; //- 1 since also have Add button 

            Debug.Log("NUM QUESTS: " + numQuestions.ToString());

            for (int j = 1; j<= numQuestions; j++){
                    GameObject question = questionsObject.transform.GetChild(j).gameObject;
                    string Qname = question.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_InputField>().text;
                    string op1 = question.transform.GetChild(1).gameObject.GetComponentInChildren<TMP_InputField>().text;
                    string op2 = question.transform.GetChild(2).gameObject.GetComponentInChildren<TMP_InputField>().text;
                    string op3 = question.transform.GetChild(3).gameObject.GetComponentInChildren<TMP_InputField>().text;
                    string op4 = question.transform.GetChild(4).gameObject.GetComponentInChildren<TMP_InputField>().text;

                    string[] ops = {op1, op2, op3, op4};

                    Debug.Log("Q NAME:" + Qname);
                    Debug.Log("OPTIONS: " + ops);

                    Question ques = new Question(Qname, ops, 1); // TODO: Need an option to add correct answer in the UI 
                    battleQuestions.Add(ques);
            }

            Debug.Log("DROOM NAME: " + dRoomName);

            DungeonRoom dungeonRoom = new DungeonRoom(i, battleQuestions);
            dungeonRooms.Add(dungeonRoom);

        }

        Debug.Log("DUNGEON NAME:" + dungeonName);

        Dungeon dungeon = new Dungeon(dungeonName, dungeonRooms);
        World world = GameState.getCurrentWorld();
        Debug.Log(world.getWorldName());
        Debug.Log(dungeon.getDungeonName());
        world.addDungeon(dungeon);
        ChangeScene.ReturnPreviousScene();

        // Debug.Log();
        // GameObject pEntry = Instantiate(Rank, PlayerList.transform);
        // TMP_Text pName = pEntry.GetComponentInChildren<TMP_Text>();
        // pName.text = userName;
        // Debug.Log(pName.text);
        // TMP_Text pEXP = pEntry.GetComponentInChildren<TMP_Text>();
        // pEXP.text = exp.ToString();

        // Dungeon dungeon = new Dungeon();
        // dungeon.DungeonName = dungeonNameField.text;
        // string json = JsonUtility.ToJson(dungeon);

        // DBreference.Child("Dungeon").Child(dungeon.DungeonName).SetRawJsonValueAsync(json).ContinueWith(task =>
        // {
        //     if(task.IsCompleted)
        //     {
        //         Debug.Log("Successfully added data to firebase");
        //     }
        //     else
        //     {
        //         Debug.Log("Not successful");
        //     }
        // }
        // );

    }
}
