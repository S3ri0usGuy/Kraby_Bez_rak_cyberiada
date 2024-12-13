using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerTransform;

    private float _timer;
    private float _timeBetweenShoots = 0.4f;
    private float _minTimeBetweenShoots = 0.4f;
    private float _maxTimeBetweenShoots = 1f;

    private float _exitTimer;
    private float _tiemTillExit;
    private float _distanceToCountExit = 3f;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //state ataku 
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy: attackstate");
        _timeBetweenShoots = Random.Range(_minTimeBetweenShoots, _maxTimeBetweenShoots);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.EnemyLookAt(_playerTransform.position);
        enemy.MoveEnemy(Vector3.zero);

        if (_timer > _timeBetweenShoots)
        {
            _timer = 0f;
            _timeBetweenShoots = Random.Range(_minTimeBetweenShoots, _maxTimeBetweenShoots);

            Vector3 dir = (_playerTransform.position - enemy.transform.position).normalized;

            enemy.animator.SetTrigger("shoot");
        }

        if (Vector3.Distance(_playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;

            if (_exitTimer > _tiemTillExit)
            {
                enemy.stateMachine.ChangeState(enemy.chaseState);
            }
        }

        else
        {
            _exitTimer = 0f;
        }


        _timer += Time.deltaTime;
    }

    public override void PhiscsUpdate()
    {
        base.PhiscsUpdate();
    }
}
