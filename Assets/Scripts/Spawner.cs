using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _timer;

    private bool _inside;
    private Damagable _currentEnemy;

    [SerializeField]
    private Damagable enemyPrefab;

    [SerializeField]
    private float respawnTime = 9f;

    private void SpawnEnemy()
    {
        _currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        _currentEnemy.Died += OnEnemyDied;

        _timer = -1f;
    }

    private void OnEnemyDied(Damagable damagable)
    {
        _timer = respawnTime;

        if (_currentEnemy)
        {
            _currentEnemy.Died -= OnEnemyDied;
        }

        _currentEnemy = null;
    }

    private void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        if (_timer <= 0f && _inside && !_currentEnemy)
        {
            SpawnEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _inside = true;
        if (other.CompareTag("Player") && !_currentEnemy && _timer <= 0f)
        {
            SpawnEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _inside = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.2f);
        Gizmos.DrawSphere(transform.position, 0.2f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
