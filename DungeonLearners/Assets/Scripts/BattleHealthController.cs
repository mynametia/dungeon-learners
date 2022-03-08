using UnityEngine;
using UnityEngine.UI;

public class BattleHealthController : MonoBehaviour
{
    public GameObject bossHealth;
    public GameObject playerHealth;

    public GameObject battleControl;

    public void reduceBossHealth(float value)
    {
        Slider health = bossHealth.GetComponent<Slider>();
        health.value -= value;
        if (health.value <= 0)
        {
            bossHealth.SetActive(false);
            StartCoroutine(battleControl.GetComponent<BattleController>().win());
        }
    }

    public void reducePlayerHealth(float value)
    {
        Slider health = playerHealth.GetComponent<Slider>();
        health.value -= value;
        if (health.value <= 0)
        {
            playerHealth.SetActive(false);
            StartCoroutine(battleControl.GetComponent<BattleController>().lose());
        }
    }
}
