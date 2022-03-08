using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public GameObject startGameUI;
    public GameObject timer;
    public GameObject questions;
    public GameObject health;

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
        timer.GetComponent<CountdownTimer>().startCountdown();
        questions.GetComponent<BattleQuestionController>().popQuestion();
        questions.GetComponent<BattleQuestionController>().updateFinalAnswer(null);
    }

    public void submitAnswer()
    {
        questions.GetComponent<SelectCard>().UnhighlightCard();
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
        health.GetComponent<BattleHealthController>().reducePlayerHealth(healthDecrement);
        questions.GetComponent<BattleQuestionController>().wrongAns();

        yield return new WaitForSeconds(1.5f);

        nextQuestion();

        yield return null;
    }

    private IEnumerator rightAnswer()
    {
        health.GetComponent<BattleHealthController>().reduceBossHealth(healthDecrement);
        questions.GetComponent<BattleQuestionController>().correctAns();

        yield return new WaitForSeconds(1.5f);

        nextQuestion();

        yield return null;
    }

    public IEnumerator timesUp()
    {
        questions.GetComponent<BattleQuestionController>().requeueQuestion();
        health.GetComponent<BattleHealthController>().reducePlayerHealth(healthDecrement);
        questions.GetComponent<BattleQuestionController>().timesUp();

        yield return new WaitForSeconds(1.5f);

        nextQuestion();

        yield return null;
    }

    public IEnumerator win()
    {
        yield return null;
    }

    public IEnumerator lose()
    {
        yield return null;
    }
}
