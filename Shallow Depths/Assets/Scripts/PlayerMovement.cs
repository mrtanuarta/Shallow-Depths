using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.linearVelocity = movement * moveSpeed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>(); // Reads movement input
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}
