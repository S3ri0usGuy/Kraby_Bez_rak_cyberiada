using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private InputProvider _inputProvider;
    private float _headEulerX;

    [SerializeField]
    private Transform head;
    [SerializeField]
    private float minAngleX = -75f;
    [SerializeField]
    private float maxAngleX = 75f;
    [SerializeField]
    private float horizontalSensitivity = 75f;
    [SerializeField]
    private float verticalSensitivity = 65f;

    private void Awake()
    {
        _inputProvider = GetComponentInParent<InputProvider>();
    }

    private void OnEnable()
    {
        _headEulerX = 0f;
        SetEulerX(_headEulerX);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 axis = _inputProvider.PlayerActions.Look.ReadValue<Vector2>();

        float dAngleHorizontal = horizontalSensitivity * axis.x;
        transform.Rotate(Vector3.up * dAngleHorizontal);

        float dAngleVertical = verticalSensitivity * axis.y;
        _headEulerX += dAngleVertical;
        _headEulerX = Mathf.Clamp(_headEulerX, minAngleX, maxAngleX);

        SetEulerX(_headEulerX);
    }

    private void SetEulerX(float eulerX)
    {
        Vector3 headEuler = head.eulerAngles;
        headEuler.x = eulerX;
        head.eulerAngles = headEuler;
    }

    public void AddRecoil(float horizontal, float vertical)
    {
        float xAngle = Random.Range(-vertical, -vertical * 0.2f);
        float yAngle = Random.Range(-horizontal, horizontal);

        _headEulerX += xAngle;
        transform.Rotate(Vector3.up * yAngle);

    }
}
