using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void ChangeSceneTo(int buildIndex, float delay)
    {
        StartCoroutine(ChangeScene(buildIndex, delay));
    }

    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator ChangeScene(int buildIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(buildIndex);
    }
}
