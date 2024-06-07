using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void ResetGame(float delay)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(SceneChangeWithDelay(delay, currentSceneIndex));
    }

    IEnumerator SceneChangeWithDelay(float delay, int buildIndex)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildIndex);
    }
}
