using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private InputProvider _inputProvider;
    private CharacterController _controller;

    private float _coyoteTimeLeft;
    private float _velocityY;
    private InputBuffer _buffer;

    private bool _isInSprint = false;

    [SerializeField, Min(0f)]
    private float moveSpeed;
    [SerializeField, Min(1f)]
    private float sprintBoost = 1.5f;
    [SerializeField, Min(0f)]
    private float gravity = 9.8f;
    [SerializeField, Min(0f)]
    private float maxVelocityY = 20f;
    [SerializeField, Min(0f)]
    private float coyoteTime = 0.12f;
    [SerializeField, Min(0f)]
    private float jumpForce = 14f;
    [SerializeField, Min(0f)]
    private float jumpBufferTime = 0.2f;

    public bool isGroundedCoyoteTime => _controller.isGrounded || _coyoteTimeLeft > 0f;

    private void Awake()
    {
        _inputProvider = GetComponentInParent<InputProvider>();
        _controller = GetComponent<CharacterController>();

        _buffer = new InputBuffer(_ => TryJump(), this, jumpBufferTime);

        _inputProvider.PlayerActions.Jump.performed += _buffer.PerformedListener;

        _inputProvider.PlayerActions.Sprint.started += OnSprintStarted;
        _inputProvider.PlayerActions.Sprint.canceled += OnSprintEnded;
    }

    private void OnEnable()
    {
        _isInSprint = false;
        _coyoteTimeLeft = coyoteTime;
    }

    private void Update()
    {
        bool isGrounded = _controller.isGrounded;

        Vector2 axis = _inputProvider.PlayerActions.Move.ReadValue<Vector2>();

        Vector3 velocity = new(axis.x, 0f, axis.y);
        velocity *= moveSpeed * Time.deltaTime;

        if (_isInSprint && isGroundedCoyoteTime && axis.y > 0f)
        {
            velocity.z *= sprintBoost;
        }

        if (!isGrounded)
        {
            if (_coyoteTimeLeft > 0f && _velocityY <= 0e-5f)
            {
                _coyoteTimeLeft -= Time.deltaTime;
            }
            else
            {
                // Make sure that acceleration is the same for different frame rates
                float halfGravity = gravity * Time.deltaTime * 0.5f;

                _velocityY -= halfGravity;
                velocity.y = _velocityY * Time.deltaTime;
                _velocityY -= halfGravity;
            }
        }
        else
        {
            _coyoteTimeLeft = coyoteTime;
            _velocityY = Mathf.Max(0f, _velocityY);
        }
        _velocityY = Mathf.Min(_velocityY, maxVelocityY);

        Vector3 transformedDirection = transform.TransformDirection(velocity);
        var collisionFlags = _controller.Move(transformedDirection);

        // Reset gravity if collides something above
        if ((collisionFlags & CollisionFlags.CollidedAbove) != 0)
        {
            _velocityY = 0f;
        }
    }

    public void SetJumpForce(float force)
    {
        _velocityY = force;
    }

    private void OnSprintStarted(InputAction.CallbackContext context)
    {
        _isInSprint = true;
    }

    private void OnSprintEnded(InputAction.CallbackContext context)
    {
        _isInSprint = false;
    }

    private bool TryJump()
    {
        if (isGroundedCoyoteTime)
        {
            _coyoteTimeLeft = 0f;
            SetJumpForce(jumpForce);
            return true;
        }
        return false;
    }
}
