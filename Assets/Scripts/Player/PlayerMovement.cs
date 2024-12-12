using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private InputProvider _inputProvider;
    private CharacterController _controller;

    private float _velocityY;
    private InputBuffer _buffer;

    [SerializeField, Min(0f)]
    private float moveSpeed;
    [SerializeField, Min(0f)]
    private float gravity = 9.8f;
    [SerializeField, Min(0f)]
    private float maxVelocityY = 20f;
    [SerializeField, Min(0f)]
    private float jumpForce = 14f;
    [SerializeField, Min(0f)]
    private float jumpBufferTime = 0.2f;

    private void Awake()
    {
        _inputProvider = GetComponentInParent<InputProvider>();
        _controller = GetComponent<CharacterController>();

        _buffer = new InputBuffer(_ => TryJump(), this, jumpBufferTime);

        _inputProvider.PlayerActions.Jump.performed += _buffer.PerformedListener;
    }

    private void Update()
    {
        bool isGrounded = _controller.isGrounded;

        Vector2 axis = _inputProvider.PlayerActions.Move.ReadValue<Vector2>();

        Vector3 velocity = new(axis.x, 0f, axis.y);
        velocity *= moveSpeed * Time.deltaTime;

        // Make sure that acceleration is the same for different frame rates
        if (!isGrounded)
        {
            float halfGravity = gravity * Time.deltaTime * 0.5f;

            _velocityY -= halfGravity;
            velocity.y = _velocityY * Time.deltaTime;
            _velocityY -= halfGravity;
        }
        else
        {
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

    private bool TryJump()
    {
        if (_controller.isGrounded)
        {
            SetJumpForce(jumpForce);
            return true;
        }
        return false;
    }
}
