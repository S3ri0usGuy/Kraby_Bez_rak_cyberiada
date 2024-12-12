using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class that implements input buffering, allowing for actions to be 
/// performed within a specific time window after input.
/// </summary>
public class InputBuffer
{
    private Coroutine _coroutine;

    private readonly Func<InputAction.CallbackContext, bool> _perform;
    private readonly MonoBehaviour _owner;
    private readonly float _window;

    /// <summary>
    /// The default duration for input buffering window.
    /// </summary>
    public const float defaultWindow = 0.15f;

    public InputBuffer(Func<InputAction.CallbackContext, bool> perform, MonoBehaviour owner,
        float window = defaultWindow)
    {
        _perform = perform;
        _owner = owner;
        _window = window;
    }

    private IEnumerator BufferCoroutine(InputAction.CallbackContext context)
    {
        float time = _window;
        while (time > 0f)
        {
            yield return null;

            if (_perform(context)) break;

            time -= Time.deltaTime;
        }
    }

    public void BufferAction(InputAction.CallbackContext context)
    {
        if (_perform(context)) return;

        if (_coroutine != null) _owner.StopCoroutine(_coroutine);
        _coroutine = _owner.StartCoroutine(BufferCoroutine(context));
    }

    public void PerformedListener(InputAction.CallbackContext context)
    {
        BufferAction(context);
    }
}