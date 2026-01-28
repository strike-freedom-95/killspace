using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    type01,
    type02,
    type03,
    type04,
    type05,
    type06,
    type07
}

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float firingChance = 0.7f;
    [SerializeField] float firingDelay = 0;
    [SerializeField] ParticleSystem[] gun;
    // [SerializeField] float moveSpeed = 2f;
    // [SerializeField] float thrust = 20f;

    Coroutine firingSystem;
    EnemyData eData;

    [SerializeField] EnemyType type;

    // STRING CONSTANTS
    const string SHOOT_ANIMATION = "isShooting";

    private void Awake()
    {        
        eData = GetComponent<EnemyData>();
        // StartCoroutine(ChangeDirection());
        // direction = (Random.Range(0, 2) == 0 ? -1 : 1);
        firingSystem = StartCoroutine(RandomGunFire());
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator RandomGunFire()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));

        while (!eData.IsShipDestroyed())
        {
            yield return new WaitForSeconds(Random.Range(1, 7 - DataHandlerScript.instance.diffLevel));
            if (Random.value < firingChance)
            {
                // Debug.Log("FIRING");
                // gun.Emit(1);
                if(type == EnemyType.type01)
                {
                    foreach (var firing in gun)
                    {
                        yield return new WaitForSeconds(firingDelay);
                        GetComponent<Animator>().SetTrigger(SHOOT_ANIMATION);
                        firing.Emit(1);
                    }
                }
                if (type == EnemyType.type02)
                {
                    foreach (var firing in gun)
                    {
                        yield return new WaitForSeconds(firingDelay);
                        GetComponent<Animator>().SetTrigger(SHOOT_ANIMATION);
                        firing.Emit(1);
                    }
                }
            }
        }
    }

    public void StopGuns()
    {
        if (firingSystem != null)
        {
            StopCoroutine(firingSystem);
            firingSystem = null;
        }
    }
}
