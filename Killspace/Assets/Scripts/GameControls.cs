using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    GameObject player;
    float hMovement;
    Rigidbody2D rb;
    bool isTriggerPressed = false;
    bool isGameQuit = false;
    bool isGameOver = false;
    bool isQuitActionStarted = false;

    [SerializeField] Joystick controlStick;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI quitMessage;
    [SerializeField] AudioClip gameQuitSFX;

    // STRING CONSTANT
    const string SHIP_TAG = "Human";
    const string HVALUE = "Horizontal";
    const string FIRE_BUTTON = "Fire1";
    const string ANIM_GAMEOVER = "isGameOver";
    const string QUIT_TOUCH = "TOUCH SCREEN TO QUIT TO MAIN MENU";
    const string QUIT_BUTTON = "PRESS ANY KEY TO QUIT TO MAIN MENU";


    private void Awake()
    {
        Cursor.visible = false;
        if (!Application.isMobilePlatform)
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    private void Start()
    {
        // Debug.LogError("PLAYER COUNT : " + GameObject.FindGameObjectsWithTag("Human").Length);
        gameOverCanvas.GetComponent<CanvasGroup>().alpha = 0;

        if(GameObject.FindGameObjectWithTag(SHIP_TAG) != null)
        {
            player = GameObject.FindGameObjectWithTag(SHIP_TAG);
            rb = player.GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.anyKey && !isQuitActionStarted)
            {
                isQuitActionStarted = true;
                QuitGameSequence();
            }
        }

        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (player == null)
            return;        

        if (Application.isMobilePlatform)
        { 
            hMovement = controlStick.Horizontal;
            // Debug.LogError("HMOVEMETN : " + (hMovement));
            Shoot();
        }
        else
        {
            hMovement = Input.GetAxisRaw(HVALUE);
            KMShoot();
            if (Input.GetKeyUp(KeyCode.Escape) && !isGameQuit)
            {
                isGameQuit = true;
                StopGame();
            }
        }

    }

    private void KMShoot()
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (Input.GetButton(FIRE_BUTTON))
        {
            player.GetComponent<PlayerControls>().FireGun();
        }
    }

    void Shoot()
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

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
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (rb != null) 
        {
            // rb.velocity = Vector2.right * hMovement * moveSpeed;
            
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

    public void StopGame()
    {
        if (!DataHandlerScript.instance.isGameStopped)
            DataHandlerScript.instance.isGameStopped = true;

        FindObjectOfType<MusicScript>().StopMusic();
        GetComponent<CanvasGroup>().alpha = 0;
        scoreText.text = FindObjectOfType<GameManager>().GetCurrentScore().ToString();
        // gameOverCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameOverCanvas.GetComponent<Animator>().SetTrigger(ANIM_GAMEOVER);
        gameOverCanvas.GetComponent<AudioSource>().Play();
        StartCoroutine(SetStatusAfterDelay(1));
    }

    IEnumerator SetStatusAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isGameOver = true;

        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            quitMessage.text = "";
            yield return new WaitForSeconds(0.5f);
            if (Application.isMobilePlatform)
            {
                quitMessage.text = QUIT_TOUCH;
            }
            else
            {
                quitMessage.text = QUIT_BUTTON;
            }
        }
    }

    public void OnQuitButtonPressed()
    {
        QuitGameSequence();
    }

    void QuitGameSequence()
    {
        AudioSource.PlayClipAtPoint(gameQuitSFX, Camera.main.transform.position);
        FindObjectOfType<GameManager>().ResetScore();
        // FindObjectOfType<MusicScript>().StopMusic();
        FindObjectOfType<SceneHandler>().QuitToMenu();
    }
}
