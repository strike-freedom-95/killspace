using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackerScript : MonoBehaviour
{
    GameObject player;

    [SerializeField] float distanceOffset = 0;

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(0, 0, player.transform.position.z + distanceOffset);
        }
    }
}
