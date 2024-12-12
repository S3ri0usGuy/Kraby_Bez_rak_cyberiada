using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    private float t;

    private float _defaultX;
    private float _defaultHeight;

    private Vector3 _defaultPosition;
    private Vector3 _targetPosition;

    [SerializeField]
    private PlayerMovement movement;

    [SerializeField, Min(0f)]
    private float amplitudeX = 2f;
    [SerializeField]
    private float amplitudeY;

    // Movement speed constraints for other min/max parameters
    [SerializeField]
    private float minSpeed = 7f;
    [SerializeField]
    private float maxSpeed = 12f;

    // How the bob speed changes with movement speed
    [SerializeField, Min(0f)]
    private float minBobSpeed = 0.2f;
    [SerializeField, Min(0f)]
    private float maxBobSpeed = 1f;

    // How the amplitude changes with movement speed
    [SerializeField, Min(0f)]
    private float minAmplitudeModifier = 1f;
    [SerializeField, Min(0f)]
    private float maxAmplitudeModifier = 2f;
    [SerializeField]
    private float followTargetSpeed = 4f;
    [SerializeField]
    private float recoverySpeed = 4f;

    [SerializeField]
    private float groundedVelocityThreshold = 0.1f;

    private void Start()
    {
        _defaultX = transform.localPosition.x;
        _defaultHeight = transform.localPosition.y;

        _defaultPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        Vector3 velocity = movement.CurrentVelocity;
        bool isGrounded = Mathf.Abs(velocity.y) < groundedVelocityThreshold;

        Vector3 xzVelocity = velocity;
        xzVelocity.y = 0f;

        float speed = xzVelocity.magnitude;
        float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, speed);
        float targetSpeed;

        float bobSpeed = Mathf.Lerp(minBobSpeed, maxBobSpeed, t);
        float amplitudeModifier = Mathf.Lerp(minAmplitudeModifier, maxAmplitudeModifier, t);
        if (Mathf.Approximately(normalizedSpeed, 0f) || !isGrounded)
        {
            // Disable bob when midair or not moving
            t = 0f;
            _targetPosition = _defaultPosition;
            targetSpeed = recoverySpeed;
        }
        else
        {
            // No time to explain
            t += Time.deltaTime;

            float x = bobSpeed * t;
            float offsetX = Mathf.Sin(x) * amplitudeX * amplitudeModifier;
            float offsetY = Mathf.Abs(offsetX) / amplitudeY * amplitudeY;

            _targetPosition = transform.localPosition;
            _targetPosition.x = _defaultX + offsetX;
            _targetPosition.y = _defaultHeight - offsetY;

            targetSpeed = followTargetSpeed;
        }

        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, _targetPosition,
            targetSpeed * Time.deltaTime);
    }
}
