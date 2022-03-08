using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleQuestionController : MonoBehaviour
{
    // Controls question selection and checks answers
    // Shows feedback upon answer submission

    private List<Question> battleQuestions = new List<Question>();
    private List<Question> currentQuestions;
    private Question currentQuestion;

    private string promptText = "Pick an answer";
    private string timesUpText = "Time's up!";
    private string wrongText = "Wrong!";
    private string correctText = "Correct!";

    public TextMeshProUGUI questionTM;
    public TextMeshProUGUI op1, op2, op3, op4;
    public TextMeshProUGUI finalAnswer;
    public GameObject submitButton;


    // Start is called before the first frame update
    void Start()
    {
        addDefaultQuestions();
        currentQuestions = new List<Question>(battleQuestions);
    }

    public void correctAns()
    {
        finalAnswer.text = correctText;
        submitButton.SetActive(false);
    }
    public void wrongAns()
    {
        finalAnswer.text = wrongText;
        submitButton.SetActive(false);
    }
    public void timesUp()
    {
        finalAnswer.text = timesUpText;
        submitButton.SetActive(false);
    }

    public bool checkAnswer()
    {
        return finalAnswer.text == currentQuestion.options[currentQuestion.answer];
    }

    public void updateFinalAnswer(GameObject answer)
    {
        if (answer == null)
        {
            finalAnswer.text = promptText;
            submitButton.SetActive(false);
        }
        else
        {
            finalAnswer.text = answer.GetComponentInChildren<TextMeshProUGUI>().text;
            submitButton.SetActive(true);
        }
    }

    public void requeueQuestion()
    {
        currentQuestions.Add(currentQuestion);
    }

    public void popQuestion()
    {
        currentQuestion = currentQuestions[0];
        currentQuestions.RemoveAt(0);

        questionTM.text = currentQuestion.question;
        op1.text = currentQuestion.options[0];
        op2.text = currentQuestion.options[1];
        op3.text = currentQuestion.options[2];
        op4.text = currentQuestion.options[3];
    }

    public int returnQuestionNumber()
    {
        return currentQuestions.Count;
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
