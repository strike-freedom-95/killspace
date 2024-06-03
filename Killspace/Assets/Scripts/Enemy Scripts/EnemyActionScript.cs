using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionScript : MonoBehaviour
{
    GameObject player;

    [SerializeField] ParticleSystem[] guns;
    [SerializeField] float range = 50;

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
            if(Vector3.Distance(transform.position, player.transform.position) < range)
            {
                foreach(var item in guns)
                {
                    if(!item.isEmitting)
                    {
                        item.Play();
                    }
                }
            }
        }
    }
}
