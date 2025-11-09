using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rbdy;

    void Start()
    {
        rbdy = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbdy.linearVelocity = transform.up * speed; // Physics-based movement
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
            DestroyProjectile();
    }

    void DestroyProjectile()
    {
        spriteRenderer.enabled = false;
        Debug.Log("Projectile destroyed");
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile hit: " + collision.gameObject.name);

        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(25f);
        }
        DestroyProjectile();
    }
}
