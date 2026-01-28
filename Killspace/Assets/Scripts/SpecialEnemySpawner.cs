using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyShip;
    [SerializeField] int unlockLevel = 3;

    GameObject player;
    GameManager gameManager;

    IEnumerator spawnEnemy;

    // STRING CONTSANT
    const string SHIP_TAG = "Human";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (GameObject.FindGameObjectWithTag(SHIP_TAG) != null)
        { 
            player = GameObject.FindGameObjectWithTag(SHIP_TAG);
            spawnEnemy = RandomEnemySpawner();
            StartCoroutine(RandomEnemySpawner());
        }
    }

    IEnumerator RandomEnemySpawner()
    {
        if (DataHandlerScript.instance.isGameStopped)
            yield break;

        while (!player.GetComponent<PlayerControls>().IsPlayerDead()) 
        {
            yield return new WaitForSeconds(Random.Range(6, 9));
            if(gameManager.GetCurrentLevel() >= unlockLevel && Random.value < 0.2f)
            {
                Instantiate(enemyShip[Random.Range(0, enemyShip.Length)], transform.position + (Vector3)new Vector2(Random.Range(-8, 8), 0), Quaternion.identity);
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine(spawnEnemy);
    }
}
