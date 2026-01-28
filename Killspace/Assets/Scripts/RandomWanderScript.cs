using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWanderScript : MonoBehaviour
{
    public Vector2 areaCenter;
    public Vector2 areaSize;
    public float forceStrength = 5f;

    Rigidbody2D rb;
    Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PickTarget();
    }

    void FixedUpdate()
    {
        if (DataHandlerScript.instance.isGameStopped)
            return;

        Vector2 direction = (target - rb.position).normalized;
        rb.AddForce(direction * forceStrength);

        if (Vector2.Distance(rb.position, target) < 0.5f)
            PickTarget();
    }

    void PickTarget()
    {
        target = new Vector2(
            Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
            Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2)
        );
    }
}
