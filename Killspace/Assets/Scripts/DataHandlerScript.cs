using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandlerScript : MonoBehaviour
{
    public static DataHandlerScript instance;

    public int diffLevel = 0;
    public int livesLeft = 0;

    public bool isGameStopped = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
