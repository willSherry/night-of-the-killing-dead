using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public float health = 100f;

    void Update()
    {
        if (health <= 0)
        {
            Die();
            health = 0;
        }
    }

    public void damage(float amount)
    {
        health -= amount;
        Debug.Log("Player took damage: " + amount + ", Current health: " + health);
        // SFX, VFX, etc. can be added here
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // Add death handling logic here (e.g., respawn, game over screen)
        gameObject.SetActive(false);
    }
}
