using System.Collections;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public GameObject startGameUI;
    public GameObject loseUI;
    public GameObject winUI;
    public GameObject timer;
    public GameObject questions;
    public GameObject health;
    public GameObject boss;

    public GameObject SceneController;

    public bool endGame = false;

    [SerializeField] private float healthDecrement;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        yield return new WaitForSeconds(2.5f);

        startGameUI.SetActive(false);
        healthDecrement = 1f/(float) questions.GetComponent<BattleQuestionController>().returnQuestionNumber();
        nextQuestion();

        yield return null;
    }

    public void nextQuestion()
    {
        if (!endGame)
        {
            timer.GetComponent<CountdownTimer>().startCountdown();
            questions.GetComponent<BattleQuestionController>().popQuestion();
            questions.GetComponent<BattleQuestionController>().updateFinalAnswer(null);
            questions.GetComponent<SelectCard>().enableSelect = true;
        }
    }

    public void submitAnswer()
    {
        questions.GetComponent<SelectCard>().UnhighlightCard();
        questions.GetComponent<SelectCard>().enableSelect = false;
        if (!questions.GetComponent<BattleQuestionController>().checkAnswer())
        {
            StartCoroutine(wrongAnswer());
        }
        else
        {
            StartCoroutine(rightAnswer());
        }
    }

    private IEnumerator wrongAnswer()
    {
        questions.GetComponent<BattleQuestionController>().requeueQuestion();
        StartCoroutine(health.GetComponent<BattleHealthController>().reducePlayerHealth(healthDecrement));
        questions.GetComponent<BattleQuestionController>().wrongAns();
        timer.GetComponent<CountdownTimer>().pause();

        yield return new WaitForSeconds(1.5f);

        nextQuestion();

        yield return null;
    }

    private IEnumerator rightAnswer()
    {
        StartCoroutine(health.GetComponent<BattleHealthController>().reduceBossHealth(healthDecrement));
        questions.GetComponent<BattleQuestionController>().correctAns();
        timer.GetComponent<CountdownTimer>().pause();

        yield return new WaitForSeconds(1.5f);

        nextQuestion();

        yield return null;
    }

    public IEnumerator timesUp()
    {
        questions.GetComponent<SelectCard>().UnhighlightCard();
        questions.GetComponent<SelectCard>().enableSelect = false;
        questions.GetComponent<BattleQuestionController>().requeueQuestion();
        StartCoroutine(health.GetComponent<BattleHealthController>().reducePlayerHealth(healthDecrement));
        questions.GetComponent<BattleQuestionController>().timesUp();

        yield return new WaitForSeconds(1.5f);

        nextQuestion();

        yield return null;
    }

    public IEnumerator win()
    {
        boss.GetComponent<Animator>().SetBool("Defeated", true);
        yield return new WaitForSeconds(1f);

        winUI.SetActive(true);
        questions.GetComponent<SelectCard>().deactivateChildren();

        yield return new WaitForSeconds(1f);

        SceneController.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");

        yield return null;
    }

    public IEnumerator lose()
    {
        loseUI.SetActive(true);
        questions.GetComponent<SelectCard>().deactivateChildren();

        yield return new WaitForSeconds(1f);

        SceneController.GetComponent<FadeTransitionController>().FadeToBlack("DungeonRoom");

        yield return null;
    }
}
