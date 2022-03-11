using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public GameObject battleControl;
    float currentTime = 0f;
    float startingTime = 15f;

    bool countDown = false;

    [SerializeField] Text countdownText;

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                countDown = false;
                StartCoroutine(battleControl.GetComponent<BattleController>().timesUp());
            }
        }
    }

    // Start countdown
    public void startCountdown()
    {
        currentTime = startingTime;
        countDown = true;
    }

    // Pause countdown timer
    public void pause()
    {
        countDown = false;
    }
}
