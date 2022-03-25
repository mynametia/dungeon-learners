//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditDungeonRoomController : MonoBehaviour
{
    public GameObject QuestionField, QuestionList, AddQuestionButton;
    public TextMeshProUGUI DungeonRoomNumberText;

    private int oldChildCount;

    void Start()
    {
        UpdateDungeonRoomNumber();
        UpdateQuestionCount();
    }

    void Update()
    {
        if (QuestionList.transform.childCount < oldChildCount)
        {
            UpdateQuestionCount();
            for (int i = 0; i < oldChildCount - 1; ++i)
            {
                QuestionList.transform.GetChild(i).gameObject.GetComponent<EditQuestionController>().UpdateQuestionNumber();
            }
            
        }
        else if (QuestionList.transform.childCount > oldChildCount)
        {
            UpdateQuestionCount();
        }
    }

    public void UpdateDungeonRoomNumber()
    {
        DungeonRoomNumberText.text = "Dungeon Room " + (gameObject.transform.GetSiblingIndex()).ToString();
    }

    public void AddQuestionField()
    {
        Instantiate(QuestionField, QuestionList.transform);
        AddQuestionButton.transform.SetAsLastSibling();
    }

    public void DeleteDungeonRoom()
    {
        Destroy(gameObject);
    }

    private void UpdateQuestionCount()
    {
        oldChildCount = QuestionList.transform.childCount;
    }
}
