using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysScript : MonoBehaviour
{
    [SerializeField] GameObject ship;

    float cruisingSpeed = 0.3f;

    private void Start()
    {
        GameObject inst = Instantiate(
                    ship,
                    transform.position,
                    Quaternion.identity,
                    transform);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * cruisingSpeed * Time.deltaTime);
    }
}
