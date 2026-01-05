using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int playerXP;
    int playerLvl;
    int xpForNextLevel;
    int score = 0;

    [SerializeField] TextMeshProUGUI xpCountertext;
    [SerializeField] TextMeshProUGUI lvlCounterText;

    private void Awake()
    {
        if(FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void IncreaseXP(int xp)
    {
        playerXP += xp;
        xpCountertext.text = playerXP.ToString();

        if (playerXP > xpForNextLevel) 
        {
            xpForNextLevel *= 2;
            IncreaseLvl();
        }
    }

    public void IncreaseLvl() 
    {
        playerLvl++;
        lvlCounterText.text = playerLvl.ToString();
    }

    public void ResetScore()
    {
        playerXP = 0;
        playerLvl = 1;
        xpForNextLevel = 50;

        xpCountertext.text = playerXP.ToString();
        lvlCounterText.text = playerLvl.ToString();
    }

    public int GetCurrentLevel()
    {
        return playerLvl;
    }

    public void IncreaseScore(int newScore)
    {
        score += newScore;
        FindObjectOfType<ScoreUIScript>().UpdateScoreText(score);
    }

    public int GetCurrentScore()
    {
        return score;
    }
}
