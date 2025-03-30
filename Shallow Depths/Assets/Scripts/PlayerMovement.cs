using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _canMove = true; // Control movement during interaction

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_canMove)
        {
            _rb.linearVelocity = Vector2.zero; // Stop movement
            _animator.SetFloat(_horizontal, 0);
            _animator.SetFloat(_vertical, 0);
            return;
        }

        _movement = InputManager.Movement; // Get input from InputManager
        _rb.linearVelocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }
    }

    public void EnableMovement(bool enable)
    {
        _canMove = enable;
        if (!enable) _rb.linearVelocity = Vector2.zero; // Ensures player stops immediately
    }
}
