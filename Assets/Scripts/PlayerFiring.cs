using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    Vector2 mousePos;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;  
    public GameObject projectile;

    void Update()
    {
        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            fire();
        }
    }

    void fire()
    {
        // Limit the fire rate
        if (Time.time < nextFire)
            return;
        else
        {
            nextFire = Time.time + fireRate;
            Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(mousePos.y - objectPos.y, mousePos.x - objectPos.x) * Mathf.Rad2Deg - 90;

            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
            SoundManager.PlaySound(SoundType.PlayerShoot);
        }

    }
}
