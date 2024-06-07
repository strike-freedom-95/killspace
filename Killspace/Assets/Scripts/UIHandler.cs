using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject gameoverWindow;
    public void ShowGameOverWindow()
    {
        Instantiate(gameoverWindow, Vector3.zero, Quaternion.identity);
    }
}
