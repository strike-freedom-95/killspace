using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionScript : MonoBehaviour
{
    [SerializeField] ParticleSystem speedEffect;
    [SerializeField] ParticleSystem[] thrustEffect;
    [SerializeField] ParticleSystem[] guns;

    [SerializeField] CinemachineVirtualCamera externalCamera;
    [SerializeField] CinemachineVirtualCamera internalCamera;
    [SerializeField] CinemachineVirtualCamera topCamera;

    private void Update()
    {
        JetControl();
        GenerateSpeedEffect();
        FireControl();
        CameraControl();
    }

    private void FireControl()
    {
        if (Input.GetButton("Fire1"))
        {
            foreach (ParticleSystem gun in guns)
            {
                if (!gun.isEmitting)
                {
                    gun.Play();
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse();
                }
            }
        }
    }

    private void CameraControl()
    {
        if (Input.GetButton("Fire2"))
        {
            externalCamera.Priority = 0;
            // topCamera.Priority = 0;
            internalCamera.Priority = 1;
        }
        else
        {
            externalCamera.Priority = 1;
            // topCamera.Priority = 1;
            internalCamera.Priority = 0;
        }
    }

    private void JetControl()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 20)
        {
            for (int i = 0; i < thrustEffect.Length; i++)
            {
                if (!thrustEffect[i].isEmitting)
                {
                    thrustEffect[i].Play();
                }
            }
        }
        else
        {
            for (int i = 0; i < thrustEffect.Length; i++)
            {
                thrustEffect[i].Stop();
            }
        }
    }

    private void GenerateSpeedEffect()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 45 && !speedEffect.isEmitting)
        {
            speedEffect.Play();
        }
        else
        {
            speedEffect.Stop();
        }
    }

    public void ToggleTopdownView()
    {
        externalCamera.Priority = 0;
        topCamera.Priority = 1;
    }
}
