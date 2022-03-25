//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditQuestionController : MonoBehaviour
{
    public TextMeshProUGUI QuestionNumberText;
    void Start()
    {
        UpdateQuestionNumber();
    }

    public void UpdateQuestionNumber()
    {
        QuestionNumberText.text = "Question " + (gameObject.transform.GetSiblingIndex() + 1).ToString() + ":";
    }

    public void DeleteQuestionField()
    {
        Destroy(gameObject);
    }
}
