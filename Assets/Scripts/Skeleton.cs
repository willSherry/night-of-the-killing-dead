using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    public float health = 50f;
    public float speed = 3f;
    public float followCutoff;
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float damage = 5f;
    private Transform player;
    private Rigidbody2D rbdy;
    private bool isAlive = true;
    private bool followPlayer = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rbdy = GetComponent<Rigidbody2D>();
        followCutoff = Random.Range(1f, 4f);
    }

    void Update()
    {
        if (player != null && isAlive && followPlayer)
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
                rbdy.AddForce(away * 0.5f); // tweak force strength
            }
        }
    }

}
