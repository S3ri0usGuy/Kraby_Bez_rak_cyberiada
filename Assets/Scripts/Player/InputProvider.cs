using UnityEngine;

public sealed class InputProvider : MonoBehaviour
{
    private InputSystem_Actions _actions;

    public InputSystem_Actions Actions => _actions;

    public InputSystem_Actions.PlayerActions PlayerActions => _actions.Player;

    private void Awake()
    {
        _actions = new();
    }

    private void OnDestroy()
    {
        _actions.Disable();
    }

    private void OnEnable()
    {
        _actions.Disable();
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
}
