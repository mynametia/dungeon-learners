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
        yield return new WaitForSeconds(3f);

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
        if (!questions.GetComponent<BattleQuestionController>().checkAnswer())
        {
            wrongAnswer();
        }
        else
        {
            rightAnswer();
        }
        nextQuestion();

    }

    private void wrongAnswer()
    {
        questions.GetComponent<BattleQuestionController>().requeueQuestion();
        health.GetComponent<BattleHealthController>().reducePlayerHealth(healthDecrement);
    }

    private void rightAnswer()
    {
        health.GetComponent<BattleHealthController>().reduceBossHealth(healthDecrement);
    }

    private void timesUp()
    {
        questions.GetComponent<BattleQuestionController>().requeueQuestion();
    }
}
