using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private InputProvider _inputProvider;
    private CharacterController _controller;

    private int _currentJumpCount;
    private float _velocityY;

    [SerializeField, Min(0f)]
    private float moveSpeed;
    [SerializeField, Min(0f)]
    private float gravity = 9.8f;
    [SerializeField, Min(0f)]
    private float maxVelocityY = 20f;
    [SerializeField, Min(0f)]
    private float jumpForce = 14f;
    [SerializeField, Min(0f)]
    [Tooltip("How many times player can jump in a row without landing.")]
    private int maxJumpCount = 2;

    private void Awake()
    {
        _inputProvider = GetComponentInParent<InputProvider>();
        _controller = GetComponent<CharacterController>();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        _currentJumpCount = 0;
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

            _currentJumpCount = Mathf.Min(1, 0);
        }
        else
        {
            _velocityY = 0f;
            _currentJumpCount = 0;
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
}
