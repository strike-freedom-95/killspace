using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShipScript : MonoBehaviour
{
    [SerializeField] float shipSpeed = 10f;
    // Start is cal led before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * shipSpeed);
    }
}
