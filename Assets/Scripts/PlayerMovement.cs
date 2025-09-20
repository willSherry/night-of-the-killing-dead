using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 inputVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0f;
        // If gravity isn’t wanted:
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1f);

        rb.linearVelocity = inputVector * movementSpeed;
    }
}
