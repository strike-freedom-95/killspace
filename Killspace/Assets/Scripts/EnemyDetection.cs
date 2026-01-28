using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // STRING CONSTANTS
    const string ENEMY_TAG = "Enemy";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            // FindObjectOfType<GameManager>().ResetScore();
            // FindObjectOfType<SceneHandler>().ResetGame();
            Destroy(collision.gameObject, 1);
        }
    }
}
