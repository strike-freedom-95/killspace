using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] float baseHealth = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] ParticleSystem[] gun;
    [SerializeField] GameObject shipSprite;
    [SerializeField] GameObject shipCollider;
    [SerializeField] ParticleSystem thruster;
    [SerializeField] int baseXP = 10;
    [SerializeField] Color hitColor;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] bool isSideMover = false;
    [SerializeField] int unlockLevel = 1;
    [SerializeField] float firingChance = 0.7f;
    [SerializeField] float firingDelay = 0;
    [SerializeField] int rewardScore = 100;

    Rigidbody2D rb;
    int direction = 1;
    bool isDead = false;
    
    Color normalColor;

    Coroutine firingSystem;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        firingSystem = StartCoroutine(RandomGunFire());

        normalColor = Color.white;

        shipSprite.GetComponent<SpriteRenderer>().color = normalColor;
    }

    private void Update()
    {
        // transform.Translate(Vector2.down * shipSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (!isSideMover)
        {
            Vector2 force = Vector2.right * direction * Mathf.Sin(Time.time);
            rb.velocity = force / 2;
        }        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayHitEffect());
            baseHealth--;
            if (baseHealth < 0 && !isDead)
            {
                isDead = true;
                FindObjectOfType<GameManager>().IncreaseScore(rewardScore);
                var inst = Instantiate(explosion, transform.position, Quaternion.identity);
                // inst.GetComponent<ExplosionScript>().PlayRandomSFX();
                shipSprite.SetActive(false);
                shipCollider.SetActive(false);
                thruster.Stop();
                FindObjectOfType<GameManager>().IncreaseXP(baseXP);

                if(firingSystem != null)
                {
                    StopCoroutine(firingSystem);
                    firingSystem = null;
                }

                Destroy(gameObject, 6);
            }
        }        
    }

    IEnumerator PlayHitEffect()
    {
        Instantiate(hitParticles, transform.position, Quaternion.identity);
        shipSprite.GetComponent<SpriteRenderer>().color = hitColor;
        yield return new WaitForSeconds(0.05f);
        shipSprite.GetComponent<SpriteRenderer>().color = normalColor;
    }

    IEnumerator RandomGunFire()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            if(Random.value < firingChance)
            {
                // gun.Emit(1);
                foreach(var firing in gun)
                {
                    yield return new WaitForSeconds(firingDelay);
                    firing.Emit(1);
                }
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
}
