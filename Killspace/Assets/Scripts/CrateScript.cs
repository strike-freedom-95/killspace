using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    [SerializeField] float minSpeed, maxSpeed;

    float actuslSpeed;

    private void Start()
    {
        actuslSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * actuslSpeed);
        if(transform.position.y < -10)
            Destroy(gameObject);
    }
}
