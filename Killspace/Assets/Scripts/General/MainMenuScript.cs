using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        FindObjectOfType<SceneHandler>().ChangeSceneTo(2, 0);
    }
}
