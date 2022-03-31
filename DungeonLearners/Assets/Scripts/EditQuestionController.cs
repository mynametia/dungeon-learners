//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Controller for editing of questions
/// </summary>
public class EditQuestionController : MonoBehaviour
{
    public TextMeshProUGUI QuestionNumberText;
    void Start()
    {
        UpdateQuestionNumber();
    }

    public void UpdateQuestionNumber()
    {
        QuestionNumberText.text = "Question " + (gameObject.transform.GetSiblingIndex()).ToString() + ":";
    }

    public void DeleteQuestionField()
    {
        Destroy(gameObject);
    }
}
