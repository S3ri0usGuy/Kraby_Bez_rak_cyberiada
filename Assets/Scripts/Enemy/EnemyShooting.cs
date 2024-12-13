using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private float _accuracyTimer;
    private int _misses;

    private int _playerLayerMask;
    private Transform _player;
    private PlayerMovement _playerMovement;

    [SerializeField]
    private float accuracyCooldown = 5f;
    [SerializeField]
    private int minIntentionalMisses = 2;
    [SerializeField]
    private int maxIntentionalMisses = 3;

    [SerializeField]
    private float missSpread = 5f;

    [SerializeField]
    private float speedSpread = 0.1f;

    [SerializeField]
    private ShootingParams shootingParams;
    [SerializeField]
    private Transform bulletsOrigin;
    [SerializeField]
    private Transform trailsOrigin;

    [SerializeField]
    private LayerMask shootingLayerMask = -1;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _accuracyTimer = 0f;
        _misses = Random.Range(minIntentionalMisses, maxIntentionalMisses + 1);

        _playerLayerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (_accuracyTimer > 0f)
        {
            _accuracyTimer -= Time.deltaTime;

            if (_accuracyTimer <= 0f)
            {
                _misses = Random.Range(minIntentionalMisses, maxIntentionalMisses + 1);
            }
        }
    }

    public void Shoot()
    {
        ShootingParams @params = shootingParams;
        LayerMask mask = shootingLayerMask;
        if (_misses > 0)
        {
            _misses--;
            @params = @params.MultiplySpread(missSpread);

            // Intentionally ignore player
            mask &= ~_playerLayerMask;
        }

        float additionalSpread = speedSpread * _playerMovement.CurrentVelocity.magnitude;
        @params = @params.MultiplySpread(additionalSpread);

        Vector3 direction = _player.position - bulletsOrigin.position;

        CombatUtils.Shoot(bulletsOrigin.position, direction,
            @params, mask, trailsOrigin.position);

        _accuracyTimer = accuracyCooldown;
    }
}
