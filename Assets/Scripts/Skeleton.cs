using System;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float health = 50f;
    public float speed = 5f; // will be read from player movement at Start if available
    public float followCutoff;
    public float attackRange = 0.5f;
    public float attackRate = 1f;
    public float damage = 5f;

    // formation / separation tuning
    public float circleRadius = 2f;            // base radius of the inner ring around the player
    public float arrivalThreshold = 0.1f;      // how close to target position counts as "arrived"
    public float separationRadius = 0.8f;
    public float separationStrength = 1.5f;

    private Transform player;
    private Rigidbody2D rbdy;
    private SkeletonManager skeletonManager;
    private bool isAlive = true;

    private float nextAttackTime = 0f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            PlayerMovement pm = playerObj.GetComponent<PlayerMovement>();
            if (pm != null) speed = pm.movementSpeed;
        }

        rbdy = GetComponent<Rigidbody2D>();
        followCutoff = UnityEngine.Random.Range(1f, 7f);
        skeletonManager = SkeletonManager.instance;
        if (skeletonManager == null)
        {
            // safe defaults if manager missing
            circleRadius = Mathf.Max(circleRadius, 1.8f);
        }

        // increment count if manager exists (you already did this elsewhere previously)
        if (skeletonManager != null)
            skeletonManager.numberOfSkeletons += 1;
    }

    void FixedUpdate()
    {
        if (!isAlive) return;

        Vector2 currentPos = rbdy.position;

        // 1) find closest alive enemy (if any)
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, skeletonManager != null ? skeletonManager.enemyDetectionRadius : 5f);
        Collider2D closestEnemy = null;
        float closestEnemyDist = float.MaxValue;
        foreach (var col in nearbyEnemies)
        {
            if (!col.CompareTag("Enemy")) continue;
            Enemy e = col.GetComponent<Enemy>();
            if (e == null || !e.isAlive) continue;
            float d = Vector2.Distance(currentPos, col.transform.position);
            if (d < closestEnemyDist)
            {
                closestEnemyDist = d;
                closestEnemy = col;
            }
        }

        // 2) compute separation vector from other skeletons
        Vector2 separation = Vector2.zero;
        Collider2D[] nearbySkeletons = Physics2D.OverlapCircleAll(transform.position, separationRadius);
        foreach (var col in nearbySkeletons)
        {
            if (col.gameObject == gameObject) continue;
            if (!col.CompareTag("Skeleton")) continue;
            float d = Vector2.Distance(currentPos, col.transform.position);
            if (d > 0f && d < separationRadius)
            {
                float weight = (separationRadius - d) / separationRadius;
                Vector2 away = (currentPos - (Vector2)col.transform.position).normalized;
                separation += away * weight;
            }
        }
        separation *= separationStrength;

        // 3) decide target behavior: chase enemy if one exists, otherwise maintain circle around player
        Vector2 desiredVelocity = Vector2.zero;

        if (closestEnemy != null)
        {
            // chase enemy
            if (closestEnemyDist > attackRange)
            {
                Vector2 dir = (((Vector2)closestEnemy.transform.position) - currentPos).normalized;
                desiredVelocity = dir * speed;
            }
            else
            {
                // in attack range: stop and attack on cooldown
                desiredVelocity = Vector2.zero;
                if (Time.time >= nextAttackTime)
                {
                    Enemy en = closestEnemy.GetComponent<Enemy>();
                    if (en != null)
                    {
                        en.takeDamage(damage);
                        nextAttackTime = Time.time + attackRate;
                    }
                }
            }
        }
        else if (player != null)
        {
            // formation: compute concentric ring and slot for this skeleton based on stable ordering
            GameObject[] all = GameObject.FindGameObjectsWithTag("Skeleton");
            if (all.Length == 0) all = new GameObject[] { gameObject };
            Array.Sort(all, (a, b) => a.GetInstanceID().CompareTo(b.GetInstanceID()));
            int total = all.Length;
            int globalIndex = Array.IndexOf(all, gameObject);
            if (globalIndex < 0) globalIndex = 0;

            // ring packing parameters
            float baseRadius = (skeletonManager != null && skeletonManager.skeletonDistance > 0f) ? skeletonManager.skeletonDistance : circleRadius;
            float ringSpacing = Mathf.Max(0.9f * baseRadius, 0.8f); // distance between rings
            int remaining = globalIndex;
            int ring = 0;

            // capacity function grows with ring index so outer rings hold more skeletons
            Func<int, int> ringCapacity = r =>
            {
                if (r == 0) return 1;
                return 6 + (r - 1) * 6; // 1, 6, 12, 18, ... (hexagonal packing)
            };

            // find ring index for this skeleton
            while (remaining >= ringCapacity(ring))
            {
                remaining -= ringCapacity(ring);
                ring++;
            }

            // angle for this skeleton's slot in the ring
            float slotAngle = (360f / Mathf.Max(1, ringCapacity(ring))) * remaining;

            // convert polar coordinates (angle, radius) to cartesian position
            float slotRadius = baseRadius + ring * ringSpacing;
            Vector2 targetPos = (Vector2)player.position + new Vector2(Mathf.Cos(slotAngle * Mathf.Deg2Rad), Mathf.Sin(slotAngle * Mathf.Deg2Rad)) * slotRadius;

            // move toward formation slot
            Vector2 toTarget = targetPos - currentPos;
            float distToTarget = toTarget.magnitude;
            if (distToTarget > arrivalThreshold)
            {
                desiredVelocity = toTarget.normalized * speed;
            }
            else
            {
                Vector2 playerVel = Vector2.zero;
                if (player.TryGetComponent<Rigidbody2D>(out var prb))
                    playerVel = prb.linearVelocity;
                else if (player.TryGetComponent<PlayerMovement>(out var pm))
                {
                    var prb2 = pm.GetComponent<Rigidbody2D>();
                    playerVel = prb2 != null ? prb2.linearVelocity : Vector2.zero;
                }

                desiredVelocity = playerVel;
            }
        }

        desiredVelocity += separation;
        if (desiredVelocity.magnitude > speed)
            desiredVelocity = desiredVelocity.normalized * speed;

        Vector2 nextPos = currentPos + desiredVelocity * Time.fixedDeltaTime;
        rbdy.MovePosition(nextPos);
        rbdy.linearVelocity = (nextPos - currentPos) / Time.fixedDeltaTime; // use correct property
    }

    void Update()
    {
        if (health <= 0 && isAlive)
        {
            Die();
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector2 vel = rbdy != null ? rbdy.linearVelocity : Vector2.zero; // use correct property
        if (vel.x < -0.1f)
            sr.flipX = false;
        else if (vel.x > 0.1f)
            sr.flipX = true;
    }

    void Die()
    {
        isAlive = false;
        if (skeletonManager != null)
            skeletonManager.numberOfSkeletons = Mathf.Max(0, skeletonManager.numberOfSkeletons - 1);
        Destroy(gameObject);
    }
}
