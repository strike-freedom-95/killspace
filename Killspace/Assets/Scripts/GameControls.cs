using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    GameObject player;
    float hMovement;
    Rigidbody2D rb;
    bool isTriggerPressed = false;
    bool isGameQuit = false;

    [SerializeField] Joystick controlStick;
    [SerializeField] float moveSpeed = 1f;

    private void Awake()
    {
        if (!Application.isMobilePlatform)
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    private void Start()
    {
        Debug.LogError("PLAYER COUNT : " + GameObject.FindGameObjectsWithTag("Human").Length);

        if(GameObject.FindGameObjectWithTag("Human") != null)
        {
            player = GameObject.FindGameObjectWithTag("Human");
            rb = player.GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if(player == null)
            return;

        if (Application.isMobilePlatform)
        { 
            hMovement = controlStick.Horizontal;
            Shoot();
        }
        else
        {
            hMovement = Input.GetAxisRaw("Horizontal");
            KMShoot();
            if (Input.GetKeyUp(KeyCode.Escape) && !isGameQuit)
            {
                isGameQuit = true;
                QuitGameSequence();
            }
        }

    }

    private void KMShoot()
    {
        if (Input.GetButton("Fire1"))
        {
            player.GetComponent<PlayerControls>().FireGun();
        }
    }

    void Shoot()
    {
        if (isTriggerPressed)
        {
            if (player != null)
            {
                player.GetComponent<PlayerControls>().FireGun();
            }
        }
    }

    private void FixedUpdate()
    {
        // Debug.LogError("HMOVEMENT : " + hMovement + " CONRES : " + controlStick.Horizontal + "FORCE : " + Vector2.right * hMovement * moveSpeed);
        // rb.AddForce(Vector2.right * hMovement * moveSpeed);

        if (rb != null) 
        {
            // rb.velocity = Vector2.right * hMovement * moveSpeed;
            // Debug.LogError("HMOVEMENT : " + hMovement + " CONRES : " + controlStick.Horizontal + "FORCE : " + Vector2.right * hMovement * moveSpeed);
            rb.AddForce(Vector2.right * hMovement * moveSpeed);
        }
    }

    public void OnFireButtonPressed()
    {
        isTriggerPressed = true;
    }

    public void OnFireButtonReleased()
    {
        isTriggerPressed = false;
    }

    public void OnQuitButtonPressed()
    {
        QuitGameSequence();
    }

    void QuitGameSequence()
    {
        FindObjectOfType<GameManager>().ResetScore();
        FindObjectOfType<MusicScript>().StopMusic();
        FindObjectOfType<SceneHandler>().QuitToMenu();
    }
}
