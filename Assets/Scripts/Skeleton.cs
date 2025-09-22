using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    public float health = 50f;
    public float speed = 5f; // Default value, will be set in Start()
    public float followCutoff;
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float damage = 5f;
    private Transform player;
    private Rigidbody2D rbdy;
    private SkeletonManager skeletonManager;
    private bool isAlive = true;
    private bool followPlayer = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rbdy = GetComponent<Rigidbody2D>();
        followCutoff = Random.Range(1f, 7f);
        skeletonManager = SkeletonManager.instance;
    }

    void Update()
    {
        if (player != null && isAlive && followPlayer)
        {
            PlayerMovement playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            speed = playerMovement.movementSpeed;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer >= followCutoff)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rbdy.linearVelocity = direction * speed;
            }
            else
            {
                rbdy.linearVelocity = Vector2.zero;
            }
        }

        if (rbdy.linearVelocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rbdy.linearVelocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void FixedUpdate()
    {
        Collider2D[] nearbySkeletons = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D col in nearbySkeletons)
        {
            if (col.gameObject != gameObject && col.CompareTag("Skeleton"))
            {
                Vector2 away = (transform.position - col.transform.position).normalized;
                rbdy.AddForce(away * 0.5f);
            }
        }

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, skeletonManager.enemyDetectionRadius);
        foreach (Collider2D col in nearbyEnemies)
        {
            if (col.gameObject != gameObject && col.CompareTag("Enemy"))
            {
                Vector2 towardsEnemy = (transform.position - col.transform.position).normalized;
                rbdy.AddForce(towardsEnemy * 0.5f);
            }

            if (col.gameObject != gameObject && col.CompareTag("Enemy") && Vector2.Distance(transform.position, col.transform.position) <= skeletonManager.skeletonDistance)
            {
                followPlayer = false;
                if (Vector2.Distance(transform.position, col.transform.position) <= attackRange)
                {
                    rbdy.linearVelocity = Vector2.zero;
                    if (Time.time >= attackRate)
                    {
                        Attack();
                        col.GetComponent<Enemy>().takeDamage(damage);
                        attackRate = Time.time + 1f;
                    }
                }
            }
            else
            {
                followPlayer = true;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Skeleton attacks for " + damage + " damage.");
    }

}
