using System;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetPos;
    private Vector3 _direction;
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy: idlestate");
        //_targetPos = GetRandomPointInSphere();
       
    }


    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (enemy.isAggroed)
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
        }// jeœli nie jest agro bêdzie ³azi³ w losowe miejsca pathfinding do zajebania ale to bardziej w chase,

<<<<<<< Updated upstream
        /*_direction = (_targetPos - enemy.transform.position).normalized;
=======
        _direction = (_targetPos - enemy.transform.position).normalized;
        _direction.y = 0;
>>>>>>> Stashed changes

        enemy.MoveEnemy(_direction * enemy.randomMovementSpeed);
        if ((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInSphere();
            Debug.Log("Enemy : changed pos, destination reached");
<<<<<<< Updated upstream
        }*/
=======
        }

>>>>>>> Stashed changes
    }

    public override void PhiscsUpdate()
    {
        base.PhiscsUpdate();
    }
    /*private Vector3 GetRandomPointInSphere()
    {
<<<<<<< Updated upstream
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitSphere * enemy.randomMovementRange;
    }*/
=======
        return  (Vector3)UnityEngine.Random.insideUnitCircle * enemy.randomMovementRange;
    }
>>>>>>> Stashed changes
}
