using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;
    public BoxCollider2D upperCollider;
    public BoxCollider2D lowerCollider;

    void Start()
    {
        AdjustColliders();
    }

    void AdjustColliders()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
        float screenHeight = Camera.main.orthographicSize * 2f;
        float colliderWidth = 1f; // Set the desired width of the colliders (For Vertical Colliders)
        float colliderHeight = 1f; // Set the desired height of the colliders (For Horizontal Colliders)

        // Adjust Left Collider
        leftCollider.size = new Vector2(colliderWidth, Camera.main.orthographicSize * 2);
        leftCollider.offset = new Vector2(-screenWidth / 2 + colliderWidth / 2, 0);

        // Adjust Right Collider
        rightCollider.size = new Vector2(colliderWidth, Camera.main.orthographicSize * 2);
        rightCollider.offset = new Vector2(screenWidth / 2 - colliderWidth / 2, 0);

        // Adjust Upper Collider
        upperCollider.size = new Vector2(Camera.main.orthographicSize * 2, colliderHeight);
        upperCollider.offset = new Vector2(0, -screenHeight / 2 + colliderHeight / 2);

        // Adjust Lower Collider
        lowerCollider.size = new Vector2(Camera.main.orthographicSize * 2, colliderHeight);
        lowerCollider.offset = new Vector2(0, screenHeight / 2 - colliderHeight / 2);
    }
}
