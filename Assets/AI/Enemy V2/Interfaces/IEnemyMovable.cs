using UnityEngine;

public interface IEnemyMovable 
{
    Rigidbody RB { set; get; }

    void MoveEnemy(Vector3 velocity);

    //void EnemyLookAt(Vector3 lookat);

}
