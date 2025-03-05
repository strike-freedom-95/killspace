using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] asteroidPrefab;
    [SerializeField] float minTime, maxTime;
    [SerializeField] float minDistance, maxDistance;


    private void Start()
    {
        StartCoroutine(SpawnAsteroidsWithDelay());
    }

    IEnumerator SpawnAsteroidsWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2;
            Instantiate(asteroidPrefab[Random.Range(0, asteroidPrefab.Length)], 
                transform.position + new Vector3(Random.Range(-screenWidth/2, screenWidth/2), 0, 0), 
                Quaternion.identity);
        }        
    }
}
