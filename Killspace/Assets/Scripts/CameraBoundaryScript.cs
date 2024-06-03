using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaryScript : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void Update()
    {
        if(player != null)
        {
            transform.position = player.transform.position;
        }
    }
}
