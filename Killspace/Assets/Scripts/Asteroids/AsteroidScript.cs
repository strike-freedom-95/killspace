using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] float rotValue = 45;
    [SerializeField] float moveSpeed  = 1f;
    [SerializeField] float asteroidHealth = 10f;
    [SerializeField] ParticleSystem explosionFX;
    [SerializeField] GameObject[] asteroids;
    [SerializeField] bool isLarge = false;
    [SerializeField] float explosiveForce = 5f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(moveSpeed, moveSpeed * 3);
        direction = Random.Range(-1, 2);
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotValue * Time.deltaTime * direction);
        if(transform.position.y < -9f)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(0, -moveSpeed), ForceMode2D.Force);
    }

    private void OnParticleCollision(GameObject other)
    {
        asteroidHealth--;
        if (asteroidHealth < 0)
        {
            DestructionSequence();
        }
    }

    private void DestructionSequence()
    {
        var exp = Instantiate(explosionFX, transform.position, Quaternion.identity);
        if (isLarge)
        {
            for (int i = 0; i < Random.Range(2, 4); i++)
            {
                var inst = Instantiate(asteroids[Random.Range(0, asteroids.Length)], transform.position, Quaternion.identity);
                inst.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-8, 8), Random.Range(-explosiveForce, explosiveForce)), ForceMode2D.Impulse);
                exp.transform.localScale = new Vector2(2, 2);
            }
        }
        Destroy(gameObject);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            asteroidHealth--;
            if (asteroidHealth < 0)
            {
                DestructionSequence();
            }
        }
    }*/
}
