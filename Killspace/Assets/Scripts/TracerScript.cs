using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        transform.position = new Vector3(0, 0, player.transform.position.z);
    }
}
