using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionInfo
{
    public string question;
    public string opt1;
    public string opt2;
    public string opt3;
    public string opt4;

    public int correctOpt;

    public static QuestionInfo CreateFromJSON(string jsonString)
    {
        Debug.Log("Create from JSON");
        return JsonUtility.FromJson<QuestionInfo>(jsonString);
    }
}