using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] GameObject transistion;

    const string TRANSITION_BOOL = "isSceneExit";

    private void Awake()
    {
        if(FindObjectsOfType<SceneHandler>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetGame()
    {
        StartCoroutine(ChangeSceneAfterDelay(SceneManager.GetActiveScene().buildIndex, 1));
    }

    public void QuitToMenu()
    {
        StartCoroutine(ChangeSceneAfterDelay(0, 1));
    }

    IEnumerator ChangeSceneAfterDelay(int buildIndex, float delay)
    {
        CreateTransition(true);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildIndex);
    }

    public void CreateTransition(bool state)
    {
        foreach(var effect in GameObject.FindGameObjectsWithTag("Transistion"))
        {
            Destroy(effect);
        }

        var newTran = Instantiate(transistion, Vector2.zero, Quaternion.identity);
        newTran.GetComponent<Animator>().SetBool(TRANSITION_BOOL, state);
    }

    public void OnStartButtonPressed()
    {
        StartGame();
    }

    public void StartGame()
    {
        // FindObjectOfType<MusicScript>().PlayMusic();
        StartCoroutine(ChangeSceneAfterDelay(1, 2));
    }
}
