using NUnit.Framework;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    bool isAlive = true;
    public float health = 100f;
    public float speed = 2f;
    public float followRange = 10f;
    public float attackRange = 0.5f;
    private Transform player;
    private Rigidbody2D rbdy;

    void Start()
    {
        rbdy = GetComponent<Rigidbody2D>();
        if (player == null)
        { 
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }

    void Update()
    {

        if (health <= 0 && isAlive)
        {
            isAlive = false;
            killEnemy();
        }

        if (isAlive && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= followRange && distanceToPlayer > attackRange)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rbdy.linearVelocity = direction * speed;
            }
            else if (distanceToPlayer <= followRange && distanceToPlayer <= attackRange)
            {
                rbdy.linearVelocity = Vector2.zero;
            }
            else
            {
                rbdy.linearVelocity = Vector2.zero;
            }
        }

    }

    public void takeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy took damage, current health: " + health);
    }

    public void killEnemy()
    {
        // Death Animation
        // Death Sound
        Destroy(gameObject);
    }
}
