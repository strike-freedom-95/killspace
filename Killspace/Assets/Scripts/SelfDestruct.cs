using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    private void Awake()
    {
        Destroy(gameObject, delay);
    }
}
