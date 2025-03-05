using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float hMovement, vMovement;

    [SerializeField ]Joystick joystick;
    [SerializeField] float speedMultiplier = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // hMovement = Input.GetAxisRaw("Horizontal");
        // vMovement = Input.GetAxisRaw("Vertical");

        hMovement = joystick.Horizontal;
        vMovement = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(hMovement, vMovement) * speedMultiplier, ForceMode2D.Impulse);
    }
}
