using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject enemy;

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        
        while(player != null)
        {
            yield return new WaitForSeconds(Random.Range(2, 7));
            if(GameObject.FindGameObjectsWithTag("Enemy").Length < 5)
            {
                Instantiate(enemy,
                new Vector3(Random.Range(-200, 200),
                Random.Range(-200, 200),
                Random.Range(1000, 1500)),
                Quaternion.identity);
            }            
        }
    }
}
