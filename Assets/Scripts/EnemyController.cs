using System.Numerics;
using Unity.AppUI.Core;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 5f;
    public Transform target;
    public Transform spriteTransform;

    public float health = 100f;
    XPManager xpManager;
    PlayerLife playerLife;
    SpriteRenderer spriteRenderer;
    public GameObject skeletonPrefab;

    private bool isDead = false;

    void Start()
    {
        // ensure references
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) target = player.transform;

        xpManager = FindFirstObjectByType<XPManager>();
        playerLife = player.GetComponent<PlayerLife>();
        spriteRenderer = spriteTransform.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            if (rb == null || target == null) return;
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * movementSpeed;   
        }
        else
        {
            rb.linearVelocity = Vector2.zero; 
        }
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
        isDead = true;
        spriteRenderer.color = Color.white;
        // Destroy(this.gameObject);
    }

    public void Resurrect()
    {
        // resurrect vfx and sfx
        Destroy(this.gameObject);
    }
}
