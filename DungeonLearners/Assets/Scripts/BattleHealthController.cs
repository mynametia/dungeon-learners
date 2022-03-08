using UnityEngine;
using UnityEngine.UI;

public class BattleHealthController : MonoBehaviour
{
    public Slider bossHealth;
    public Slider playerHealth;

    public void reduceBossHealth(float value)
    {
        bossHealth.value -= value;
        if (bossHealth.value <= 0)
        { 
        
        }
    }

    public void reducePlayerHealth(float value)
    {
        playerHealth.value -= value;
        if (playerHealth.value <= 0)
        { 
        
        }
    }
}
