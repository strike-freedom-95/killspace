using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadronScript : MonoBehaviour
{
    [SerializeField] GameObject[] enemyShips;

    GameObject player;

    bool isFirstWaveLaunched = false;

    int difficultyFactor = 0;
    bool isSpawningComplete = false;
    GameManager gameManager;

    // STRING CONTSANT
    const string SHIP_TAG = "Human";
    const string ENEMY_TAG = "Enemy";

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (GameObject.FindGameObjectWithTag(SHIP_TAG) != null)
            player = GameObject.FindGameObjectWithTag(SHIP_TAG);

        StartCoroutine(SpawnFormation(3, true, 3));
    }

    IEnumerator EnemyCountCheck()
    {
        if (DataHandlerScript.instance.isGameStopped)
            yield break;

        while (!player.GetComponent<PlayerControls>().IsPlayerDead()) 
        {
            yield return new WaitForSeconds(0.5f);
            if(GameObject.FindGameObjectsWithTag(ENEMY_TAG).Length == 0 && !isSpawningComplete)
            {
                isSpawningComplete = true;
                if(Random.value > 0.3f)
                    difficultyFactor++;

                if (Random.value < 0.2f)
                    difficultyFactor += 2;

                difficultyFactor += DataHandlerScript.instance.diffLevel;

                if (Random.value < 0.2f && gameManager.GetCurrentLevel() > 5)
                    difficultyFactor = Random.Range(10, 15);

                StartCoroutine (SpawnFormation(3 + difficultyFactor));
            }
        }
    }

    IEnumerator SpawnFormation(int count, bool isInitial = false, float initialWait = 0)
    {
        if (DataHandlerScript.instance.isGameStopped)
            yield break;

        yield return new WaitForSeconds(initialWait);

        yield return new WaitForSeconds(Random.Range(0, 2));
        int space = 4;
        int index = 0;

        for (int i = 0; i < count; i++) 
        {
            index++;
            yield return new WaitForSeconds(0.3f);

            if (index > 5)
                index = 0;

            Vector2 offset = new Vector2(index * space, 3);

            if (gameManager.GetCurrentLevel() > 3)
            {
                Instantiate(enemyShips[Random.Range(0, enemyShips.Length)], transform.position + (Vector3)offset, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyShips[0], transform.position + (Vector3)offset, Quaternion.identity);
            }
        }

        if (!isFirstWaveLaunched)
        {
            isFirstWaveLaunched = true;
            StartCoroutine(EnemyCountCheck());
        }

        isSpawningComplete = false;
    }
}