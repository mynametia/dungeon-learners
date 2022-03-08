using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleHealthController : MonoBehaviour
{
    public GameObject bossHealth;
    public GameObject playerHealth;

    public GameObject battleControl;


    private float roundUp = 10000f;
    public IEnumerator reduceBossHealth(float value)
    {
        Slider health = bossHealth.GetComponent<Slider>();
        float decrement = Mathf.Round((value / 20f)*roundUp)/roundUp;
        float period = 1f / 20f;
        while (value > 0)
        {
            health.value -= decrement;
            value -= decrement;
            yield return new WaitForSeconds(period);
        }
        if (health.value <= 0)
        {
            battleControl.GetComponent<BattleController>().endGame = true;
            bossHealth.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(battleControl.GetComponent<BattleController>().win());
        }
        yield return null;
    }

    public IEnumerator reducePlayerHealth(float value)
    {
        Slider health = playerHealth.GetComponent<Slider>();
        float decrement = Mathf.Round((value / 20f) * roundUp) / roundUp;
        float period = 1f / 20f;
        while (value > 0)
        {
            health.value -= decrement;
            value -= decrement;
            yield return new WaitForSeconds(period);
        }
        if (health.value <= 0)
        {
            battleControl.GetComponent<BattleController>().endGame = true;
            playerHealth.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(battleControl.GetComponent<BattleController>().lose());
        }
        yield return null;
    }
}
