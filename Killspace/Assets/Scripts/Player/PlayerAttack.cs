using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem[] guns;
    [SerializeField] int selection = 0;
    [SerializeField] AudioSource gunSound;

    bool isFiring = false;

    private void Start()
    {
        DisableAllGuns();
        GunSelect(selection);
    }

    private void Update()
    {
        if (isFiring)
        {
            if (!GetComponent<PlayerStatus>().GetPlayerLiveStatus())
            {
                if (!gunSound.isPlaying) 
                {
                    foreach (var p in guns)
                    {
                        if (!p.isEmitting)
                        {
                            p.Play();
                            gunSound.Play();
                        }
                    }
                }                
            }
        }
    }

    public void OnFireButtonPressed()
    {
        isFiring = true;
    }

    public void OnFireButtonReleased()
    {
        isFiring = false;
    }

    public void GunSelect(int sel)
    {
        DisableAllGuns();
        switch (sel)
        {
            case 0:
                guns[0].gameObject.SetActive(true);
                break;
            case 1:
                guns[1].gameObject.SetActive(true);
                guns[2].gameObject.SetActive(true);
                break;
            case 2:
                guns[0].gameObject.SetActive(true);
                guns[1].gameObject.SetActive(true);
                guns[2].gameObject.SetActive(true);
                break;
            case 3:
                guns[3].gameObject.SetActive(true);
                guns[4].gameObject.SetActive(true);
                guns[5].gameObject.SetActive(true);
                guns[6].gameObject.SetActive(true);
                break;
            case 4:
                guns[0].gameObject.SetActive(true);
                guns[3].gameObject.SetActive(true);
                guns[4].gameObject.SetActive(true);
                guns[5].gameObject.SetActive(true);
                guns[6].gameObject.SetActive(true);
                break;
            default: break;
        }
    }

    private void DisableAllGuns()
    {
        foreach (var p in guns)
        {
            p.gameObject.SetActive(false);
        }
    }
}
