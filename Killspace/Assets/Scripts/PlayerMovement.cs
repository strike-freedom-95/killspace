using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    bool hMoveLock = true;
    bool vMoveLock = true;
    float hMovement = 0f;
    float vMovement = 0f;

    [SerializeField] float bankingSpeed = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!hMoveLock)
        {
            MoveHorizontally();
        }
        if (!vMoveLock)
        {
            MoveVertically();
        }
    }

    public void LockHMovement(bool status)
    {
        hMoveLock = status;
    }

    public void LockVMovement(bool status)
    {
        vMoveLock = status;
    }

    void MoveHorizontally()
    {
        hMovement = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector3.right * hMovement * bankingSpeed);
    }

    void MoveVertically()
    {
        vMovement = Input.GetAxisRaw("Vertical");
        rb.AddForce(Vector3.up * vMovement * bankingSpeed);
    }
}
