using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // FindObjectOfType<GameManager>().ResetScore();
            // FindObjectOfType<SceneHandler>().ResetGame();
            Destroy(collision.gameObject, 10);
        }
    }
}
