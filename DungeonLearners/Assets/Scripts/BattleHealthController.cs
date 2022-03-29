using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleHealthController : MonoBehaviour
{
    // Both boss and player health range from 0 to 1
    public GameObject bossHealth;
    public GameObject playerHealth;

    public GameObject battleControl;

    public GameObject DungeonControl;

    private float roundUp = 10000f;

    // Reduce boss health
    public IEnumerator reduceBossHealth(float value)
    {
        Slider health = bossHealth.GetComponent<Slider>();
        // Need to round up decrement or else health bar will not reach 0 properly
        float decrement = Mathf.Round((value / 20f)*roundUp)/roundUp;
        float period = 1f / 20f;
        while (value > 0)
        {
            health.value -= decrement;
            value -= decrement;
            yield return new WaitForSeconds(period);
        }
        if (health.value <= 0.0001)
        {
            battleControl.GetComponent<BattleController>().endGame = true;
            bossHealth.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            DungeonControl = GameObject.FindWithTag("DungeonController");
            DungeonControl.GetComponent<DungeonController>().UpdateRoomScore(playerHealth.GetComponent<Slider>().value);

            StartCoroutine(battleControl.GetComponent<BattleController>().win());
        }
        yield return null;
    }

    // Reduce player health
    public IEnumerator reducePlayerHealth(float value)
    {
        Slider health = playerHealth.GetComponent<Slider>();
        // Need to round up decrement or else health bar will not reach 0 properly
        float decrement = Mathf.Round((value / 20f) * roundUp) / roundUp;
        float period = 1f / 20f;
        while (value > 0)
        {
            health.value -= decrement;
            value -= decrement;
            yield return new WaitForSeconds(period);
        }
        if (health.value <= 0.0001)
        {
            battleControl.GetComponent<BattleController>().endGame = true;
            playerHealth.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(battleControl.GetComponent<BattleController>().lose());
        }
        yield return null;
    }
}
