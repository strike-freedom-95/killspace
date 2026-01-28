using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] ParticleSystem gun;
    [SerializeField] float firingDelay = 1f;
    [SerializeField] GameObject explosion;

    [SerializeField] GameObject shipSprite;
    [SerializeField] GameObject shipCollider;
    [SerializeField] ParticleSystem thruster;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] Color hitColor;

    AudioSource gunSound;
    bool isFiring = false;
    bool isDead = false;

    CinemachineImpulseSource impulseSrc;

    int health = 5;
    Color normalColor;
    int gunIndex = 0;

    IEnumerator damagerHandler;

    // STRING CONSTANTS
    const string ENEMY_TAG = "Enemy";

    private void Awake()
    {
        normalColor = Color.white;
        damagerHandler = DamageEffect();        
    }

    int LevelCheck()
    {
        if(DataHandlerScript.instance == null)
        {
            return 0;
        }

        int level = DataHandlerScript.instance.diffLevel;
        switch (level)
        {
            case 0: return 5; 
            case 1: return 3; 
            case 2: return 1;
            default: return 0;
        }
    }

    private void Start()
    {
        impulseSrc = GetComponent<CinemachineImpulseSource>();
        gunSound = GetComponent<AudioSource>();
        FindObjectOfType<SceneHandler>().CreateTransition(false); // To trigger the transistion animation

        if (FindObjectOfType<MusicScript>() != null)
            FindObjectOfType<MusicScript>().PlayMusic();
        
        FindObjectOfType<GameManager>().ResetScore();

        health = LevelCheck();
        DataHandlerScript.instance.livesLeft = health;
        FindObjectOfType<GameManager>().UpdateLivesCount();
    }

    public void FireGun()
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if(isDead)
            return;

        if (!gun.isEmitting && !isFiring)
        {
            isFiring = true;
            gun.Emit(1);
            gunSound.Play();
            StartCoroutine(ReloadDelay());
        }
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(firingDelay);
        isFiring = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (collision.gameObject.CompareTag(ENEMY_TAG) && !isDead)
        {
             DeathSequence();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crate"))
        {
            IncreaseHealth();
            Destroy(collision.gameObject);
        }
    }

    private void DeathSequence()
    {
        StopCoroutine(damagerHandler);
        // FindObjectOfType<SceneHandler>().CreateTransition(true); // To trigger the transistion animation

        isDead = true;
        // FindObjectOfType<SceneHandler>().ResetGame();
        GetComponentInChildren<CircleCollider2D>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);

        shipSprite.SetActive(false);
        shipCollider.SetActive(false);
        thruster.Stop();

        // FindObjectOfType<GameManager>().ResetScore();
        FindObjectOfType<GameControls>().StopGame();

        if(!DataHandlerScript.instance.isGameStopped)
            DataHandlerScript.instance.isGameStopped = true;

        // Destroy(gameObject, 3);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (other.CompareTag(ENEMY_TAG))
        {
            health--;

            StartCoroutine(DamageEffect());

            if (health < 0)
                DeathSequence();
        }
    }

    public bool IsPlayerDead()
    {
        return isDead;
    }

    IEnumerator DamageEffect()
    {
        if (isDead)
            yield break;

        DataHandlerScript.instance.livesLeft = health;
        FindObjectOfType<GameManager>().UpdateLivesCount();

        if (Application.isMobilePlatform)
        {
            Handheld.Vibrate();
        }

        Instantiate(hitParticles, transform.position, Quaternion.identity);
        shipSprite.GetComponent<SpriteRenderer>().color = hitColor;
        yield return new WaitForSeconds(0.05f);
        shipSprite.GetComponent<SpriteRenderer>().color = normalColor;
        FindObjectOfType<ScoreKeeper>().PlayDamageEffect();
        impulseSrc.GenerateImpulse();
    }

    void IncreaseHealth()
    {
        health = Mathf.Clamp(health + 1, 0, 5);
        DataHandlerScript.instance.livesLeft = health;
        FindObjectOfType<GameManager>().UpdateLivesCount();
        FindObjectOfType<ScoreKeeper>().DisplayVoiceText("+1 Extra Life!!");
    }

    void UpgradeGun()
    {

    }
}
