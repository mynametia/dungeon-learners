using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score, coins;
    
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("EXP");
        coins = PlayerPrefs.GetInt("Coins");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
