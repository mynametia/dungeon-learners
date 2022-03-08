using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
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
            }
        }
    }

    public void startCountdown()
    {
        currentTime = startingTime;
        countDown = true;
    }
}
