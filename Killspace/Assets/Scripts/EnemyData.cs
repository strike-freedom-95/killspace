using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] float baseHealth = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject shipSprite;
    [SerializeField] GameObject shipCollider;
    [SerializeField] ParticleSystem[] thruster;
    [SerializeField] int baseXP = 10;
    [SerializeField] Color hitColor;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool isSideMover = false;
    [SerializeField] int unlockLevel = 1;
    [SerializeField] int rewardScore = 100;
    [SerializeField] GameObject mysteryCrate;

    bool isDead = false;
    
    Color normalColor;
    GameManager manager;

    // STRING CONSTANTS
    const string PLAYER_TAG = "Player";

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        normalColor = Color.white;

        shipSprite.GetComponent<SpriteRenderer>().color = normalColor;
    }

    private void Start()
    {
        StartCoroutine(StartAllThrusters(0.5f));
    }

    private void OnParticleCollision(GameObject other)
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (other.CompareTag(PLAYER_TAG))
        {
            StartCoroutine(PlayHitEffect());
            baseHealth--;

            if (DataHandlerScript.instance.diffLevel == 0)
                baseHealth = -2;

            if (baseHealth < 0 && !isDead)
            {
                isDead = true;
                FindObjectOfType<GameManager>().IncreaseScore(rewardScore);
                var inst = Instantiate(explosion, transform.position + new Vector3(0, 0, -5), Quaternion.identity);
                // inst.GetComponent<ExplosionScript>().PlayRandomSFX();
                shipSprite.SetActive(false);
                // shipCollider.SetActive(false);
                Destroy(shipCollider);
                // thruster.Stop();

                StopAllThrusters();
                if (Random.value <= 0.1f && !manager.isCrateReset)
                {
                    manager.RelaxCrateSpawning();
                    Instantiate(mysteryCrate, transform.position, Quaternion.identity); 
                }

                FindObjectOfType<GameManager>().IncreaseXP(baseXP);
                GetComponent<EnemyBehaviour>().StopGuns();
                Destroy(gameObject, 5);
            }
        }        
    }

    IEnumerator PlayHitEffect()
    {
        if(isDead)
            yield break;

        Instantiate(hitParticles, transform.position, Quaternion.identity);
        shipSprite.GetComponent<SpriteRenderer>().color = hitColor;
        yield return new WaitForSeconds(0.05f);
        shipSprite.GetComponent<SpriteRenderer>().color = normalColor;
    }

    void StopAllThrusters()
    {
        foreach(var unit in thruster)
        {
            unit.Stop();
        }
    }

    IEnumerator StartAllThrusters(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (thruster != null)
        {
            foreach (var unit in thruster)
            {
                unit.Play();
            }
        }
    }

    public int GetXP()
    {
        return baseXP;
    }

    public int GetUnlockLevel()
    {
        return unlockLevel;
    }

    public bool IsShipDestroyed()
    {
        return isDead;
    }
}
