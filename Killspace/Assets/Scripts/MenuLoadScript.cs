using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoadScript : MonoBehaviour
{
    [SerializeField] AudioClip startSFX;
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip clickSFX;

    [SerializeField] GameObject difficultyPage;

    Animator difficultyPageAC;

    bool isButtonClicked = false;

    // STRING CONST
    const string CLOSE_TEXT = "isClosed";

    private void Start()
    {
        difficultyPage.SetActive(false);
        FindObjectOfType<SceneHandler>().CreateTransition(false);
        difficultyPageAC = difficultyPage.GetComponent<Animator>();
        FindObjectOfType<MusicScript>().PlayMusic();
    }

    public void OnStartButtonClicked()
    {
        // AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
        // FindObjectOfType<SceneHandler>().OnStartButtonPressed();

        if (DataHandlerScript.instance.isGameStopped)
            DataHandlerScript.instance.isGameStopped = false;

        difficultyPage.SetActive(true);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnEasyButtonPressed()
    {
        if (isButtonClicked)
            return;

        isButtonClicked = true;

        DataHandlerScript.instance.diffLevel = 0;
        AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
        FindObjectOfType<SceneHandler>().OnStartButtonPressed();
        FindObjectOfType<MusicScript>().StopMusic();
    }
    public void OnNormalButtonPressed()
    {
        if (isButtonClicked)
            return;

        isButtonClicked = true;

        DataHandlerScript.instance.diffLevel = 1;
        AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
        FindObjectOfType<SceneHandler>().OnStartButtonPressed();
        FindObjectOfType<MusicScript>().StopMusic();
    }

    public void OnHardButtonPressed()
    {
        if (isButtonClicked)
            return;

        isButtonClicked = true;

        DataHandlerScript.instance.diffLevel = 2;
        AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
        FindObjectOfType<SceneHandler>().OnStartButtonPressed();
        FindObjectOfType<MusicScript>().StopMusic();
    }

    public void OnDifficultyPromptCloseButtonClicked()
    {
        if (isButtonClicked)
            return;

        isButtonClicked = true;

        if (difficultyPage.activeInHierarchy)
        {
            difficultyPageAC.SetBool(CLOSE_TEXT, true);
            StartCoroutine(DelayedUIClose(difficultyPage, 0.5f));
        }
    }

    IEnumerator DelayedUIClose(GameObject UI, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (UI.activeInHierarchy)
        {
            UI.SetActive(false);
        }
        isButtonClicked = false;
    }

    public void PlayHoverSound()
    {
        AudioSource.PlayClipAtPoint(hoverSFX, Camera.main.transform.position);
    }

    public void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(clickSFX, Camera.main.transform.position);
    }
}
