using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Vector2 inputVector;
    private Vector2 mousePos;
    private Vector2 objectPos;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isMoving;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isMoving = false;
    }

    void FixedUpdate()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        transform.position += moveInput.normalized * movementSpeed * Time.fixedDeltaTime;
        isMoving = moveInput != Vector3.zero;

        animator.SetBool("isRunning", isMoving);

        if (isMoving)
        {
            // play running animation
        }

        mousePos = Input.mousePosition;
        objectPos = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < objectPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

}
