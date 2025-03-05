using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    float health = 100f;
    bool isPlayerDead = false;

    public float GetShipHealth()
    {
        return health;
    }

    public void SetShipHealth(float health)
    {
        this.health = health;
    }

    public bool GetPlayerLiveStatus()
    {
        return isPlayerDead;
    }

    public void SetPlayerStatus(bool status)
    {
        this.isPlayerDead = status;
    }
}
