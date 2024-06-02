using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera externalCamera;
    [SerializeField] CinemachineVirtualCamera topCamera;
    [SerializeField] CinemachineVirtualCamera sideCamera;

    private void Start()
    {
        EnableDefaultCamera();
        StartCoroutine(CameraToggle());
    }

    IEnumerator CameraToggle()
    {
        while (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            yield return new WaitForSeconds(Random.Range(10, 20));
            int current = (int)Random.Range(0, 2);
            switch (current)
            {
                case 0: EnableTopDownCamera();
                    player.GetComponent<PlayerMovement>().LockHMovement(false);
                    player.GetComponent<PlayerMovement>().LockVMovement(true); 
                    break;
                case 1: EnableSidewaysCamera();
                    player.GetComponent<PlayerMovement>().LockHMovement(true);
                    player.GetComponent<PlayerMovement>().LockVMovement(false); 
                    break;
                default: break;
            }
        }
    }

    public void EnableDefaultCamera()
    {
        externalCamera.Priority = 1;
        topCamera.Priority = 0;
        sideCamera.Priority = 0;
    }

    public void EnableTopDownCamera()
    {
        externalCamera.Priority = 0;
        topCamera.Priority = 1;
        sideCamera.Priority = 0;
    }

    public void EnableSidewaysCamera()
    {
        externalCamera.Priority = 0;
        topCamera.Priority = 0;
        sideCamera.Priority = 1;
    }
}
