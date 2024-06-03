using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    bool isTakeOff = false;
    [SerializeField] ParticleSystem explosionFX;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ship Unlock")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            isTakeOff = true;
            // GetComponent<PlayerActionScript>().ToggleTopdownView();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTakeOff)
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
