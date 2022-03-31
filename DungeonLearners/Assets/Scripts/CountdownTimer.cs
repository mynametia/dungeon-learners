using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages countdown timer
/// </summary>
public class CountdownTimer : MonoBehaviour
{
    public GameObject battleControl;
    float currentTime = 0f;
    float startingTime = 15f;

    bool countDown = false;

    [SerializeField] Text countdownText;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
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

    //// <summary>
    /// Start countdown
    /// </summary>
    public void startCountdown()
    {
        currentTime = startingTime;
        countDown = true;
    }

    /// <summary>
    /// Pause countdown timer
    /// </summary>
    public void pause()
    {
        countDown = false;
    }
}
