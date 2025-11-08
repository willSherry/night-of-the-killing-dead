using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 5f;
    public Transform target;

    public float health = 100f;

    void Start()
    {
        // ensure references
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) target = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (rb == null || target == null) return;
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * movementSpeed;
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
        Destroy(gameObject);
    }
}
