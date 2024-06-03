using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;

    [SerializeField] float moveSpeed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        /* Vector3 relative = transform.InverseTransformPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
        float angle = Mathf.Atan2(relative.y, relative.z) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle); */

        
        if(player != null)
        {
            transform.LookAt(player.transform.position);
            rb.AddRelativeForce(Vector3.forward * moveSpeed, ForceMode.VelocityChange);
        }
        // rb.velocity = Vector3.forward * moveSpeed;
    }
}
