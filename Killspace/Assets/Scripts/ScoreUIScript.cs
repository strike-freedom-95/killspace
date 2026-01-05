using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject scoreUI;

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
        scoreUI.GetComponent<Animator>().SetTrigger("scored");
    }
}
