using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DynamicFov : MonoBehaviour
{
    private Camera _camera;

    private float _defaultFov;
    private float _targetFov;

    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private float speedThreshold;
    [SerializeField]
    private float highSpeedFov;
    [SerializeField]
    private float blendSpeed;
    [SerializeField]
    private float negativeBlendSpeed;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _defaultFov = _targetFov = _camera.fieldOfView;
    }

    private void LateUpdate()
    {
        Vector3 velocityXZ = movement.CurrentVelocity;
        velocityXZ.y = 0f;

        if (velocityXZ.magnitude > speedThreshold)
        {
            _targetFov = highSpeedFov;
        }
        else
        {
            _targetFov = _defaultFov;
        }

        float blend = _targetFov > _camera.fieldOfView ? blendSpeed : negativeBlendSpeed;
        _camera.fieldOfView = Mathf.MoveTowards(
            _camera.fieldOfView, _targetFov, blend * Time.deltaTime);
    }
}
