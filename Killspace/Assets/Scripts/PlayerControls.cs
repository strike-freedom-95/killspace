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

    AudioSource gunSound;
    bool isFiring = false;
    bool isDead = false;

    private void Start()
    {
        gunSound = GetComponent<AudioSource>();
        FindObjectOfType<SceneHandler>().CreateTransition(false); // To trigger the transistion animation
        FindObjectOfType<MusicScript>().PlayMusic();
        FindObjectOfType<GameManager>().ResetScore();
    }

    public void FireGun()
    {
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
        if (collision.gameObject.CompareTag("Enemy") && !isDead)
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        FindObjectOfType<SceneHandler>().CreateTransition(true); // To trigger the transistion animation

        isDead = true;
        FindObjectOfType<SceneHandler>().ResetGame();
        GetComponentInChildren<CircleCollider2D>().enabled = false;
        Instantiate(explosion, transform.position, Quaternion.identity);

        shipSprite.SetActive(false);
        shipCollider.SetActive(false);
        thruster.Stop();

        FindObjectOfType<GameManager>().ResetScore();

        // Destroy(gameObject, 3);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            DeathSequence();
        }
    }
}
