//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Firestore;

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
        addQuestions("Computing", "Artificial Intelligence", 0);
        // addDefaultQuestions();
        // currentQuestions = new List<Question>(battleQuestions);
        // Debug.Log("This should run");
    }

    // Display correct answer text and remove submit button
    public void correctAns()
    {
        finalAnswer.text = correctText;
        submitButton.SetActive(false);
    }

    // Display wrong answer text and remove submit button
    public void wrongAns()
    {
        finalAnswer.text = wrongText;
        submitButton.SetActive(false);
    }

    // Display time's up text and remove submit button
    public void timesUp()
    {
        finalAnswer.text = timesUpText;
        submitButton.SetActive(false);
    }

    // Check correctness of answer
    public bool checkAnswer()
    {
        return finalAnswer.text == currentQuestion.options[currentQuestion.answer];
    }

    // Show/hide submit button based on selected answer
    // Update final answer card based on selected answer
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

    // If time is up or question is answered wrongly,
    // current question will be added to question list to be displayed later again
    public void requeueQuestion()
    {
        currentQuestions.Add(currentQuestion);
    }

    // Display 1st question and its options in question list
    // Removes displayed question from list
    public void popQuestion()
    {
        if (currentQuestions.Count > 0)
        {
            currentQuestion = currentQuestions[0];
            currentQuestions.RemoveAt(0);

            questionTM.text = currentQuestion.question;
            op1.text = currentQuestion.options[0];
            op2.text = currentQuestion.options[1];
            op3.text = currentQuestion.options[2];
            op4.text = currentQuestion.options[3];
        }
    }

    // Get number of questions for this battle
    public int returnQuestionNumber()
    {
        return currentQuestions.Count;
    }

    // Hard code add questions to this battle
    private void addDefaultQuestions()
    {
        
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
                "Sense of one's body position or pose",
                "Sense of touch experienced through different types of sensory mechanoreceptors",
                "Sense of balance",
                "Perception of pain"},
            0));
    }

    private async void addQuestions(string worldName, string topicName, int dungeonID)
    {
        var db = FirebaseFirestore.DefaultInstance;
        Query questions = db.Collection("question_bank").Document(worldName).Collection(topicName).Document("difficulty_" + (dungeonID+1).ToString()).Collection("questions");
        QuerySnapshot questionsSnapshot = await questions.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in questionsSnapshot.Documents)
        {
            string qnNo = documentSnapshot.Id.ToString();
            Debug.Log("Document data for " + qnNo);
            
            Dictionary<string, object> question = documentSnapshot.ToDictionary();
            Debug.Log(question["question"].ToString());
            Debug.Log(question["correctOpt"].ToString());
            battleQuestions.Add(new Question(
                question["question"].ToString(),
                new string[4] {
                    question["opt1"].ToString(),
                    question["opt2"].ToString(),
                    question["opt3"].ToString(),
                    question["opt4"].ToString()},
                int.Parse(question["correctOpt"].ToString())-1
            ));
        }

        currentQuestions = new List<Question>(battleQuestions);
    }
}
