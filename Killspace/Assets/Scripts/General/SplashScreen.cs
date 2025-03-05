using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] float delay = 0.1f;
    [SerializeField] Slider progressBar;

    private void Start()
    {
        StartCoroutine(Refresh());
    }

    IEnumerator Refresh()
    {
        while (progressBar.value < 20) 
        {
            yield return new WaitForSeconds(delay + Random.Range(0, 2));
            progressBar.value ++;
            if (progressBar.value >= 10)
            {
                GameObject.FindObjectOfType<SceneHandler>().ChangeSceneTo(1, 0);
            }
        }
    }
}
