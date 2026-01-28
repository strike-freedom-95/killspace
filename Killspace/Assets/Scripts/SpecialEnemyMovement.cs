using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float distance = 3f;

    [SerializeField] GameObject shipSprite;
    [SerializeField] float rotationSpeed = 360f; 

    private Vector3 startPos;
    Transform target;
    bool isReady = false;

    // STRING CONTSANT
    const string SHIP_TAG = "Human";

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag(SHIP_TAG) != null)
        {
            target = GameObject.FindGameObjectWithTag(SHIP_TAG).transform;
        }

        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    

    void Update()
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        Vector2 direction = target.position - shipSprite.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        shipSprite.transform.rotation = Quaternion.RotateTowards(
            shipSprite.transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void FixedUpdate()
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        if (!isReady)
        {
            rb.MovePosition(startPos);
            if(transform.position.x < 1 || transform.position.x > -1)
            {
                isReady = true;
            }
        }        

        if (isReady)
        {
            float x = Mathf.PingPong(Time.time * moveSpeed, distance * 2) - distance;
            rb.MovePosition(new Vector2(startPos.x + x, rb.position.y));
        }
    }
}
