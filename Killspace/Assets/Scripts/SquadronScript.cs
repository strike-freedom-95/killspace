using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadronScript : MonoBehaviour
{ 
    [SerializeField] GameObject[] enemyShip;
    [SerializeField] GameObject waveWarning;

    // [SerializeField] int minShips, maxShips;
    bool isSpawned = false;
    Vector2 initialPos;
    float cruisingSpeed = 0;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        cruisingSpeed = 0.2f;
        StartCoroutine(SpawnSquadron());
        StartCoroutine(CheckSquadronCount());
        initialPos = transform.position;
    }

    private void Update()
    {
        if (transform.position.y > -10)
        {
            transform.Translate(Vector2.down * cruisingSpeed * Time.deltaTime);
        }
    }

    /*GameObject GetShip()
    {
        GameObject ship = enemyShip[Random.Range(0, enemyShip.Length)];

        if(ship.GetComponent<EnemyData>().GetUnlockLevel() > gameManager.GetCurrentLevel())
        {
            ship = enemyShip[0];
        }
        return ship;
    }*/

    GameObject GetShip()
    {
        int currentLevel = gameManager.GetCurrentLevel();

        List<GameObject> unlockedShips = new List<GameObject>();

        foreach (GameObject ship in enemyShip)
        {
            EnemyData data = ship.GetComponent<EnemyData>();
            if (data != null && data.GetUnlockLevel() <= currentLevel)
            {
                unlockedShips.Add(ship);
            }
        }

        if (unlockedShips.Count == 0)
            return enemyShip[0]; // or null, depending on design

        return unlockedShips[Random.Range(0, unlockedShips.Count)];
    }


    IEnumerator SpawnSquadron()
    {
        // int rows = Random.Range(1, 10);

        // yield return new WaitForSeconds(2);

        Instantiate(waveWarning, Vector2.zero, Quaternion.identity);

        int rows = (int)Mathf.Clamp(FindObjectOfType<GameManager>().GetCurrentLevel(), 1, 4);
        int columns = 6;
        float hSpacing = 1.5f;
        float vSpacing = 3f;

        Vector2 startPos = transform.position;

        for (int row = 0; row < rows; row++)
        {
            GameObject spawnShip = GetShip();

            for (int col = 0; col < columns; col++)
            {
                Vector2 spawnPos = startPos + new Vector2(
                    col * vSpacing,
                    row * hSpacing
                );

                GameObject inst = Instantiate(
                    spawnShip,
                    spawnPos,
                    Quaternion.identity,
                    transform   // parent immediately
                );

                yield return null; // spawn one per frame
            }

            yield return null ;
        }
        isSpawned = false;
    }


    IEnumerator CheckSquadronCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !isSpawned)
            {
                isSpawned = true;
                transform.position = initialPos;
                StartCoroutine(SpawnSquadron());
            }
        }
    }
}
