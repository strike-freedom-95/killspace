using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    float thrust = 0;
    bool isEngineStarted = false;

    [SerializeField] float turnSpeed = 2f;
    [SerializeField] float topSpeed = 200;
    [SerializeField] float acceleration = 20f;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] Slider thrustSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        thrustSlider.maxValue = topSpeed;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if(!isEngineStarted)
            {
                isEngineStarted = true;
                animator.SetBool("Engine start", true);
            }
        }
        speedText.text = Mathf.Round(GetComponent<Rigidbody>().velocity.magnitude).ToString("F2");
        thrustSlider.value = thrust;
    }

    private void FixedUpdate()
    {
        // speedMultiplier = Input.mouseScrollDelta.x;
        
        if(isEngineStarted)
        {
            thrust += Input.GetAxis("Mouse ScrollWheel") * acceleration;
            thrust = Mathf.Clamp(thrust, 0, topSpeed);
            // Debug.Log(thrust);
            rb.AddRelativeForce(Vector3.forward *  thrust * 60);

            float pitch = Input.GetAxisRaw("Vertical") * turnSpeed;
            float yaw = Input.GetAxisRaw("Horizontal") * -turnSpeed;

            rb.AddRelativeTorque(new Vector3(pitch, 0, yaw));

            // float sidewaysMovement = Input.GetAxisRaw("Horizontal") * turnSpeed;
            // rb.velocity = new Vector3(sidewaysMovement, rb.velocity.y, rb.velocity.z);

            // rb.AddForce(new Vector3(sidewaysMovement, 0, thrust));

        }
    }
}
