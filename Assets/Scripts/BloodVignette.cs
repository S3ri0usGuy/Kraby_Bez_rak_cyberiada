using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// A component that controls the vignette effect intensity depending on
/// the HP.
/// </summary>
public class BloodVignette : MonoBehaviour
{
    private Vignette _vignette;

    private float _currentIntensity;
    private float CurrentIntensity
    {
        get => _currentIntensity;
        set
        {
            _vignette.intensity.@value = _currentIntensity = value;
        }
    }

    [SerializeField]
    private Volume volume;
    [SerializeField]
    private Damagable player;

    [SerializeField]
    private int oneHpThreshold = 10;

    [SerializeField, Min(0f)]
    private float transitionSpeed;

    [SerializeField, Min(0f)]
    private float damagedIntensity;
    [SerializeField, Min(0f)]
    private float damagedRecoverSpeed;
    [SerializeField, Min(0f)]
    private float damagedDelay;

    [SerializeField, Min(0f)]
    private float diedIntensity;

    [SerializeField, Min(0f)]
    private float oneHpIntensity;
    [SerializeField, Min(0f)]
    private float oneHpAmplitude;
    [SerializeField, Min(0f)]
    private float oneHpSpeed;

    private void Awake()
    {
        volume.profile.TryGet(out _vignette);
    }

    private void OnEnable()
    {
        CurrentIntensity = 0f;

        player.Damaged += OnDamaged;
        player.Died += OnDied;
        player.HpChanged += OnHpChanged;
    }

    private void OnDisable()
    {
        player.Damaged -= OnDamaged;
        player.Died -= OnDied;
        player.HpChanged -= OnHpChanged;
    }

    private void OnDied(Damagable Damagable)
    {
        StopAllCoroutines();
        StartCoroutine(Transition(diedIntensity));
    }

    private void OnDamaged(Damagable receiver, int dmg)
    {
        StopAllCoroutines();
        StartCoroutine(Damaged());
    }

    private void OnHpChanged(Damagable sender)
    {
        if (player.Hp == player.MaxHp)
        {
            StopAllCoroutines();
            StartCoroutine(Transition(0f));
        }
    }

    private IEnumerator Damaged()
    {
        yield return Transition(damagedIntensity);
        yield return new WaitForSeconds(damagedDelay);

        if (player.Hp <= oneHpThreshold)
        {
            yield return OneHpFlicker();
        }
        else
        {
            yield return Transition(0f, damagedRecoverSpeed);
        }
    }

    private IEnumerator Transition(float intensity)
    {
        return Transition(intensity, transitionSpeed);
    }

    private IEnumerator Transition(float intensity, float speed)
    {
        while (Mathf.Abs(CurrentIntensity - intensity) > 1e-3f)
        {
            yield return null;
            CurrentIntensity = Mathf.MoveTowards(
                CurrentIntensity, intensity,
                speed * Time.deltaTime);
        }
    }

    private IEnumerator OneHpFlicker()
    {
        yield return Transition(oneHpIntensity);

        while (player.Hp <= oneHpThreshold)
        {
            yield return null;
            float t = Time.time * oneHpSpeed;
            CurrentIntensity = oneHpIntensity +
                (Mathf.PingPong(t, oneHpAmplitude * 2f) - oneHpAmplitude);
        }
    }
}
