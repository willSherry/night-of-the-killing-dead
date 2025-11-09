using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 5f;
    public Transform target;

    public float health = 100f;
    XPManager xpManager;
    PlayerLife playerLife;

    void Start()
    {
        // ensure references
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) target = player.transform;

        xpManager = FindFirstObjectByType<XPManager>();
        playerLife = player.GetComponent<PlayerLife>();
    }

    void FixedUpdate()
    {
        if (rb == null || target == null) return;
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * movementSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerLife.damage(5f);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");
        xpManager.AddXP(1);
        Destroy(this.gameObject);
    }
}
