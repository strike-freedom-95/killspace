using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    SceneHandler sceneHandler;

    [SerializeField] ParticleSystem explosionFX;

    private void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            GetComponent<PlayerStatus>().SetPlayerStatus(true);
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponentInChildren<PolygonCollider2D>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            foreach (var p in GetComponentsInChildren<ParticleSystem>())
            {
                p.Stop();
            }
            
            sceneHandler.ChangeSceneTo(sceneHandler.GetCurrentScene(), 1);
        }
    }
}
