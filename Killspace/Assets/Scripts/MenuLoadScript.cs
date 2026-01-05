using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoadScript : MonoBehaviour
{
    [SerializeField] AudioClip startSFX;

    private void Start()
    {
        
        FindObjectOfType<SceneHandler>().CreateTransition(false);
    }

    public void OnStartButtonClicked()
    {
        AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
        FindObjectOfType<SceneHandler>().OnStartButtonPressed();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
