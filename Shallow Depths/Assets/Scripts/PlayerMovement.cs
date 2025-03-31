using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _canMove = true; // Control movement during interaction

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_canMove)
        {
            _rb.linearVelocity = Vector2.zero; // Stop movement
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 0);
            return;
        }

        _rb.linearVelocity = _movement * _moveSpeed;

        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat("LastHorizontal", _movement.x);
            _animator.SetFloat("LastVertical", _movement.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void EnableMovement(bool enable)
    {
        _canMove = enable;
        if (!enable) _rb.linearVelocity = Vector2.zero; // Ensures player stops immediately
    }
}
