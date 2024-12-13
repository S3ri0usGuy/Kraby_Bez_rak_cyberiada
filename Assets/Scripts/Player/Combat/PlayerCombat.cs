using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    private bool _isReloading;

    private Animator _animator;
    private int _bulletsInMagazine;
    private InputProvider _inputProvider;

    [SerializeField]
    private PlayerRotation playerRotation;

    [SerializeField]
    private int magazineCapacity = 8;

    [SerializeField]
    private Transform bulletsOrigin;
    [SerializeField]
    private Transform trailsOrigin;
    [SerializeField]
    private LayerMask shootingLayerMask = -1;

    [SerializeField]
    private Grenade grenadePrefab;
    [SerializeField]
    private Transform grenadeOrigin;
    [SerializeField]
    private Transform grenadePosition;
    [SerializeField]
    private ShootingParams shootingParams;

    [SerializeField]
    private Vector2 recoil;

    /// <summary>
    /// Gets/sets a current number of bullets in the magazine.
    /// </summary>
    public int BulletsInMagazine
    {
        get => _bulletsInMagazine;
        set
        {
            _bulletsInMagazine = value;
            BulletsChanged?.Invoke(this);
        }
    }

    /// <summary>
    /// Gets a maximum number of bullets in the magazine.
    /// </summary>
    public int MagazineCapacity => magazineCapacity;

    public event Action<PlayerCombat> BulletsChanged;

    private void Awake()
    {
        _inputProvider = GetComponentInParent<InputProvider>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _isReloading = false;
        BulletsInMagazine = magazineCapacity;

        _inputProvider.PlayerActions.Attack.performed += OnAttackPerformed;
        _inputProvider.PlayerActions.Reload.performed += OnReloadPerformed;
        _inputProvider.PlayerActions.Grenade.performed += OnGrenadeThrowPerformed;
    }

    private void StartReloading()
    {
        if (_isReloading || BulletsInMagazine >= magazineCapacity) return;

        _isReloading = true;
        _animator.SetTrigger("reload");
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        StartReloading();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (_isReloading) return;

        if (BulletsInMagazine <= 0)
        {
            StartReloading();
            return;
        }

        _animator.SetTrigger("shoot");
    }

    private void OnGrenadeThrowPerformed(InputAction.CallbackContext obj)
    {
        if (_isReloading) return;

        _animator.SetTrigger("grenade");
    }

    // Called by animation
    public void Shoot()
    {
        CombatUtils.Shoot(bulletsOrigin.position, bulletsOrigin.forward,
            shootingParams, shootingLayerMask, trailsOrigin.position); 

        playerRotation.AddRecoil(recoil.x, recoil.y);

        BulletsInMagazine--;
    }

    // Called by animation
    public void Reload()
    {
        _isReloading = false;
        BulletsInMagazine = magazineCapacity;
    }

    // Called by animation
    public void ThrowGrenade()
    {
        Grenade grenade = Instantiate(grenadePrefab,
            grenadePosition.position, grenadePosition.rotation);
        grenade.Throw(grenadeOrigin.forward);
    }
}
