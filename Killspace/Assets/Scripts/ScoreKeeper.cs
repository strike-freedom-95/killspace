using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI xpCountertext;
    [SerializeField] TextMeshProUGUI lvlCounterText;
    [SerializeField] GameObject[] playerLives;
    [SerializeField] GameObject damageIndicator;
    [SerializeField] GameObject playerLivesPanel;
    [SerializeField] GameObject levelPanel;
    [SerializeField] GameObject waveWarning;

    [Header("UI Color Configuration")]
    [SerializeField] Color defaultColor;
    [SerializeField] Color damageColor;
    [SerializeField] Color levelUpColor;

    bool isDamageEffectPlaying = false;

    private void Start()
    {
        SetLives();
        StartCoroutine(DisplayWaveWarning(1));
    }

    IEnumerator DisplayWaveWarning(float delay)
    {
        yield return new WaitForSeconds(delay);
        Createnotification();
    }

    public void SetLives()
    {
        foreach (var child in playerLives)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void UpdateLivesCount()
    {
        // StartCoroutine(Flash(playerLivesPanel.GetComponent<Image>(), 0.2f, defaultColor, damageColor));

        int currentLife = DataHandlerScript.instance.livesLeft;
        foreach (var child in playerLives)
        {
            child.SetActive(false);
        }

        for (int i = 0; i < playerLives.Length; i++)
        {
            if (i < currentLife)
            {
                playerLives[i].SetActive(true);
            }
        }
    }

    public void UpdateXPUI(int playerXP, int xpForNextLevel)
    {
        xpCountertext.text = playerXP.ToString() + " / " + xpForNextLevel.ToString();
    }

    public void UpdateLevelUI(int playerLvl) 
    {
        StartCoroutine(Flash(levelPanel.GetComponent<Image>(), 0.5f, defaultColor, levelUpColor));

        lvlCounterText.text = playerLvl.ToString();
    }

    public void PlayDamageEffect()
    {
        if (!isDamageEffectPlaying)
        {
            isDamageEffectPlaying = true;
            StartCoroutine(Fade(damageIndicator.GetComponent<CanvasGroup>(), 0.5f));
        }
    }

    IEnumerator Fade(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Sin((t / duration) * Mathf.PI);
            yield return null;
        }

        cg.alpha = 0f;
        isDamageEffectPlaying = false;
    }

    IEnumerator Flash(Image sr, float duration, Color defaultColor, Color shiftColor)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = Mathf.Sin((t / duration) * Mathf.PI);
            sr.color = Color.Lerp(defaultColor, shiftColor, lerp);
            yield return null;
        }

        sr.color = defaultColor;
    }

    public void DisplayVoiceText(string message) 
    {
        var inst = Createnotification();
        inst.GetComponent<MessageScript>().PrintMessage(message);
        // inst.GetComponent<MessageScript>().PlayComms();
    }

    GameObject Createnotification()
    {
        foreach(var alerts in GameObject.FindGameObjectsWithTag("Notification"))
        {
            Destroy(alerts);
        }

        return Instantiate(waveWarning);
    }

}
