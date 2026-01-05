using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    private void Start()
    {
        StartCoroutine(SpawnEnemyAfterDelay());
    }

    IEnumerator SpawnEnemyAfterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            Instantiate(enemy, transform.position + (Vector3)new Vector2(Random.Range(-7, 7), 0), Quaternion.identity);
        }
    }
}
