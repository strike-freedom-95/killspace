using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionFX;
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
