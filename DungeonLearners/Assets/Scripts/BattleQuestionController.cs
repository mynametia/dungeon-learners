//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Firestore;
using UnityEditor;
using System.IO;

/// <summary>
/// Manages question in battle.
/// </summary>
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


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get dungeon room ID
        int curDungeonRoomID = DungeonController.currentDungeonRoomID;
        addQuestions(PlayerPrefs.GetString("preloadWorldChoice"), PlayerPrefs.GetString("preloadDungeonChoice"), curDungeonRoomID);
        // addHardcodedQuestions("Computing", "Artificial Intelligence", curDungeonRoomID);
        // addDefaultQuestions();
        //addQuestions("Computing", "Artificial Intelligence", curDungeonRoomID);
        // currentQuestions = new List<Question>(battleQuestions);
    }

    /// <summary>
    /// Display correct answer text and remove submit button
    /// </summary>
    public void correctAns()
    {
        finalAnswer.text = correctText;
        submitButton.SetActive(false);
    }

    /// <summary>
    /// Display wrong answer text and remove submit button
    /// </summary>
    public void wrongAns()
    {
        finalAnswer.text = wrongText;
        submitButton.SetActive(false);
    }

    /// <summary>
    /// Display time's up text and remove submit button
    /// </summary>
    public void timesUp()
    {
        finalAnswer.text = timesUpText;
        submitButton.SetActive(false);
    }

    /// <summary>
    /// Check correctness of answer
    /// </summary>
    public bool checkAnswer()
    {
        return finalAnswer.text == currentQuestion.options[currentQuestion.answer];
    }

    /// <summary>
    /// Show/hide submit button based on selected answer
    /// Update final answer card based on selected answer
    /// </summary>
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

    /// <summary>
    /// If time is up or question is answered wrongly,
    /// current question will be added to question list to be displayed later again
    /// </summary>
    public void requeueQuestion()
    {
        currentQuestions.Add(currentQuestion);
    }

    /// <summary>
    /// Display 1st question and its options in question list
    /// Removes displayed question from list
    /// </summary>
    public void popQuestion()
    {
        Debug.Log("popQuestion");
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

    /// <summary>
    /// Get number of questions for this battle
    /// </summary>
    public int returnQuestionNumber()
    {
        return currentQuestions.Count;
    }

    /// <summary>
    /// Hard code add questions to this battle
    /// </summary>
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

    /// <summary>
    /// Get questions from Firebase and add to battleQuestions
    /// </summary>
    private async void addQuestions(string worldName, string topicName, int dungeonRoomID)
    {
        var db = FirebaseFirestore.DefaultInstance;
        Query questions = db.Collection("question_bank").Document(worldName).Collection(topicName).Document("difficulty_" + (dungeonRoomID+1).ToString()).Collection("questions");
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
        Debug.Log("This should run");
    }

    /// <summary>
    /// Add hardcoded questions for testing purposes. Note this method is now deprecated
    /// </summary>
    private void addHardcodedQuestions(string worldName, string topicName, int dungeonRoomID)
    {
        switch ((worldName, topicName, dungeonRoomID))
        {
            case ("Computing", "Artificial Intelligence", 0):
                jsonDeserialize("Comp", "AI", "0", 3);
                break;
            case ("Computing", "Artificial Intelligence", 1):
                jsonDeserialize("Comp", "AI", "1", 3);
                break;
            case ("Computing", "Artificial Intelligence", 2):
                jsonDeserialize("Comp", "AI", "2", 4);
                break;
            case ("Computing", "Human Computer Interaction", 0):
                jsonDeserialize("Comp", "HCI", "0", 3);
                break;
            case ("Computing", "Human Computer Interaction", 1):
                jsonDeserialize("Comp", "HCI", "1", 4);
                break;
            case ("Computing", "Human Computer Interaction", 2):
                jsonDeserialize("Comp", "HCI", "2", 3);
                break;
            case ("Computing", "Software Engineering", 0):
                jsonDeserialize("Comp", "SE", "0", 4);
                break;
            case ("Computing", "Software Engineering", 1):
                jsonDeserialize("Comp", "SE", "1", 3);
                break;
            case ("Computing", "Software Engineering", 2):
                jsonDeserialize("Comp", "SE", "2", 3);
                break;
            case ("Ethics", "Deontology", 0):
                jsonDeserialize("Eth", "Deon", "0", 3);
                break;
            case ("Ethics", "Deontology", 1):
                jsonDeserialize("Eth", "Deon", "1", 3);
                break;
            case ("Ethics", "Deontology", 2):
                jsonDeserialize("Eth", "Deon", "2", 4);
                break;
            case ("Ethics", "General Ethics", 0):
                jsonDeserialize("Eth", "GE", "0", 3);
                break;
            case ("Ethics", "General Ethics", 1):
                jsonDeserialize("Eth", "GE", "1", 3);
                break;
            case ("Ethics", "General Ethics", 2):
                jsonDeserialize("Eth", "GE", "2", 4);
                break;
            case ("Ethics", "Research Ethics", 0):
                jsonDeserialize("Eth", "RE", "0", 3);
                break;
            case ("Ethics", "Research Ethics", 1):
                jsonDeserialize("Eth", "RE", "1", 4);
                break;
            case ("Ethics", "Research Ethics", 2):
                jsonDeserialize("Eth", "RE", "2", 3);
                break;
            case("Formal Writing", "Formal Writing I", 0):
                jsonDeserialize("FormWrit", "I", "0", 4);
                break;
            case("Formal Writing", "Formal Writing I", 1):
                jsonDeserialize("FormWrit", "I", "1", 3);
                break;
            case("Formal Writing", "Formal Writing I", 2):
                jsonDeserialize("FormWrit", "I", "2", 3);
                break;
            case("Formal Writing", "Formal Writing II", 0):
                jsonDeserialize("FormWrit", "II", "0", 4);
                break;
            case("Formal Writing", "Formal Writing II", 1):
                jsonDeserialize("FormWrit", "II", "1", 3);
                break;
            case("Formal Writing", "Formal Writing II", 2):
                jsonDeserialize("FormWrit", "II", "2", 3);
                break;
        }
    }

    /// <summary>
    /// Get questions from JSON files and add to battleQuestions
    /// </summary>
    private void jsonDeserialize(string subject, string topic, string dungeonRoomID, int noQuestions)
    {
        for (int i = 0; i < noQuestions; i++)
        {
            string path = subject + "_" + topic + "_" + dungeonRoomID + "_" + (i+1).ToString();
            // Assets/Resources/QuestionFiles/Comp_AI_0_1.json
            // TextAsset qnJsonData = (TextAsset)Resources.Load("QuestionFiles/Comp_AI_0_1");
            TextAsset qnJsonData = (TextAsset)Resources.Load("QuestionFiles/" + path);
            string strJson = qnJsonData.text;

            // string path = Application.persistentDataPath + "/" + subject + "_" + topic + "_" + dungeonRoomID + "_" + (i+1).ToString() + ".json";

            var singleQuestion = QuestionInfo.CreateFromJSON(strJson);

            battleQuestions.Add(new Question(
                singleQuestion.question,
                new string[4] {
                    singleQuestion.opt1,
                    singleQuestion.opt2,
                    singleQuestion.opt3,
                    singleQuestion.opt4},
                singleQuestion.correctOpt-1
            ));
        }
    }    
}
