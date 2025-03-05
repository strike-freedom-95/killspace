using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    [SerializeField] float timeToLive = 1f;

    private void Awake()
    {
        Destroy(gameObject, timeToLive);
    }
}
