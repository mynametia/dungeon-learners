using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleQuestionController : MonoBehaviour
{
    private List<Question> battleQuestions = new List<Question>();

    // Start is called before the first frame update
    void Start()
    {
        addDefaultQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void addDefaultQuestions()
    {
        // Hard code add questions to this battle
        battleQuestions.Add(new Question(
            "Which of the following explains what is the meaning of 'subjective satisfaction'?",
            new string[4] {
                "How long it takes for typical members of community to learn relevant task",
                "How long it takes to perform relevant benchmarks",
                "How many and what kind of errors made during benchmark tasks",
                "How much did the users like using various aspects of the interface"},
            3));
        battleQuestions.Add(new Question(
            "Which of the following is NOT true about command language?",
            new string[4] {
                "Supports user initiatives",
                "Requires substantial training and memorization",
                "Appeals to novice users",
                "Allows creation of user defined macros"},
            2));
        battleQuestions.Add(new Question(
            "What is the meaning of 'proprioception'?",
            new string[4] {
                "Sense of oneÅfs body position or pose",
                "Sense of touch experienced through different types of sensory mechanoreceptors",
                "Sense of balance",
                "Perception of pain"},
            0));
    }
}
