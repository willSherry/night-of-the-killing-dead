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
        speed = player.GetComponent<PlayerMovement>().movementSpeed;
        rbdy = GetComponent<Rigidbody2D>();
        followCutoff = Random.Range(1f, 7f);
        skeletonManager = SkeletonManager.instance;

        skeletonManager.numberOfSkeletons += 1;
    }

    void Update()
    {
        if (health <= 0 && isAlive)
        {
            Die();
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
        bool enemyNearby = false;
        Collider2D closestEnemy = null;
        float closestEnemyDistance = float.MaxValue;

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
            if (col.gameObject != gameObject && col.CompareTag("Enemy") && col.GetComponent<Enemy>().isAlive)
            {
                float dist = Vector2.Distance(transform.position, col.transform.position);
                if (dist < closestEnemyDistance)
                {
                    closestEnemyDistance = dist;
                    closestEnemy = col;
                }
            }

            if (closestEnemy != null)
            {
                enemyNearby = true;
                if (closestEnemyDistance > attackRange)
                {
                    Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
                    rbdy.linearVelocity = direction * speed;
                }
                else
                {
                    rbdy.linearVelocity = Vector2.zero;
                    if (Time.time >= attackRate)
                    {
                        Attack();
                        attackRate = Time.time + 1f;
                        closestEnemy.GetComponent<Enemy>().takeDamage(damage);
                    }
                }
            }

            if (!enemyNearby && player != null && isAlive)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer >= followCutoff)
                {
                    Vector2 direction = (player.position - transform.position).normalized;
                    rbdy.linearVelocity = direction * speed;
                }
                else
                {
                    rbdy.linearVelocity = Vector2.zero;
                    // future idle roaming behavior can be added here
                }
            }
        }
    }

    void Attack()
    {
        Debug.Log("Skeleton attacks for " + damage + " damage.");
    }
    
    void Die()
    {
        isAlive = false;
        skeletonManager.numberOfSkeletons -= 1;
        Debug.Log("Skeleton has died.");
        // Add death handling logic here (e.g., drop loot, play animation)
        Destroy(gameObject);
    }

}
