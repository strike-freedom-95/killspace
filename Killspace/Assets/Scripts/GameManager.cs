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

    public bool isCrateReset = false;

    // [SerializeField] TextMeshProUGUI xpCountertext;
    // [SerializeField] TextMeshProUGUI lvlCounterText;
    // [SerializeField] GameObject[] playerLives;

    private void Awake()
    {
        if(FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        FindObjectOfType<ScoreKeeper>().UpdateXPUI(playerXP, xpForNextLevel);
        FindObjectOfType<ScoreKeeper>().UpdateLevelUI(playerLvl);
        FindObjectOfType<ScoreKeeper>().UpdateLivesCount();
    }

    /*private void SetLives()
    {
        foreach (var child in playerLives)
        {
            child.gameObject.SetActive(false);
        }
    }*/

    public void UpdateLivesCount()
    {
        /*int currentLife = DataHandlerScript.instance.livesLeft;
        foreach(var child in playerLives)
        {
            child.gameObject.SetActive(false);
        }

        for(int i = 0; i < playerLives.Length; i++)
        {
            if(i < currentLife)
            {
                playerLives[i].SetActive(true);
            }
        }*/

        FindObjectOfType<ScoreKeeper>().UpdateLivesCount();
    }

    public void IncreaseXP(int xp)
    {
        playerXP += xp;
        // xpCountertext.text = playerXP.ToString() + " / " + xpForNextLevel.ToString();
        FindObjectOfType<ScoreKeeper>().UpdateXPUI(playerXP, xpForNextLevel);

        if (playerXP > xpForNextLevel) 
        {
            xpForNextLevel *= 2;
            IncreaseLvl();
        }
    }

    public void IncreaseLvl() 
    {
        playerLvl++;
        // lvlCounterText.text =  "Lvl " + playerLvl.ToString();
        FindObjectOfType<ScoreKeeper>().UpdateLevelUI(playerLvl);
    }

    public void ResetScore()
    {
        playerXP = 0;
        playerLvl = 1;
        xpForNextLevel = 50;
        score = 0;

        // xpCountertext.text = playerXP.ToString();
        // lvlCounterText.text = playerLvl.ToString();
        FindObjectOfType<ScoreKeeper>().UpdateXPUI(playerXP, xpForNextLevel);
        FindObjectOfType<ScoreKeeper>().UpdateLevelUI(playerLvl);

        FindObjectOfType<ScoreUIScript>().UpdateScoreText(score);
    }

    public int GetCurrentLevel()
    {
        return playerLvl;
    }

    public void IncreaseScore(int newScore)
    {
        if(DataHandlerScript.instance.diffLevel > 0)
        {
            score += (newScore * DataHandlerScript.instance.diffLevel);
        }
        else
        {
            score += newScore;
        }
        
        FindObjectOfType<ScoreUIScript>().UpdateScoreText(score);
    }

    public int GetCurrentScore()
    {
        return score;
    }

    public void RelaxCrateSpawning()
    {
        StartCoroutine(ResetCrateSpawnTime((DataHandlerScript.instance.diffLevel + 1) * Random.Range(3, 6)));
    }

    IEnumerator ResetCrateSpawnTime(float delay)
    {
        isCrateReset = true;
        yield return new WaitForSeconds(delay);
        isCrateReset = false;
    }
}
