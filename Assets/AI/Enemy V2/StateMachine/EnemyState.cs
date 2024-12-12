using UnityEngine;

public class EnemyState 
{
    protected Enemy enemy;
    protected EnemyStateMachine EnemyStateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.EnemyStateMachine = enemyStateMachine;

    }
    public virtual void EnterState() { }//KIedy zmienia stan wywo³ywana jest ta funkcja po zmianie zwykle uzywana do pobierania zmiennych i ustawiania rzeczy
    public virtual void ExitState() { }//kiedy wychiodzi ze stanu wykonuje siê ta funkca
    public virtual void FrameUpdate() { }// stanadardowy update co klatke dla stanu
    public virtual void PhiscsUpdate() { }// standardowy phisics update co czas dla stanu
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)// animation triggery
    {

    }

}
