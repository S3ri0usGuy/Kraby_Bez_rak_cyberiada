using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMovable, ITriggerCheckable
{//interface tutaj daja cechy ppprzeciwnikowi bo nie kazdy przeciwnik musi miec wszystkie
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody RB { get; set; }
    public bool isAggroed { get ; set ; }
    public bool isWithinStrikingDistance { get; set; }


    #region enemy state variables
    public EnemyStateMachine stateMachine { set; get; }
    public EnemyIdleState idleState { set; get; }
    public EnemyChaseState chaseState { set; get; }
    public EnemyAttackState attackState { set; get; }
    
    #endregion

    #region enemy idle state variables
    public float randomMovementRange = 5f;
    public float randomMovementSpeed = 1f;
    //tu s¹ zmienne dotycz¹ce stanu idle które s¹ wykorzystywane przez niego do poruszania
    #endregion

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        // tu dodajecie nowe stany
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

    }
    public void Start()
    {
        CurrentHealth = MaxHealth;
        RB = GetComponent<Rigidbody>();

        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        stateMachine.CurrentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.CurrentEnemyState.PhiscsUpdate();
    }
    public void Damage(float damageAmount)// system hp i damage
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth >= 0f)
        {
            Die();
        }
    }

    public void Die()// œmieræ
    {
        Destroy(gameObject);
    }

    #region Movement Functions
    //TODO dodaæ patrzenie w kierunku gracza lub w losowym kierunku zarówno tu jak i w interface IEnemy movable
    public void EnemyLookAt(Vector3 lookat)
    {
        lookat.y = transform.position.y;
        transform.LookAt(lookat);
    }
    public void MoveEnemy(Vector3 velocity)// funkcja która porusza rigid body które jest w enemy poprzez nadanie mu velocity, kiedy chcesz ruszyæ przeciwnikiem calluj t¹ funkcje
    {
        RB.linearVelocity = velocity;
    }

    #endregion

    #region distnance checks

    public void setAggroStatus(bool isAggroed)
    {
        this.isAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        this.isWithinStrikingDistance = isWithinStrikingDistance;
    }

    #endregion

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        stateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType); // umo¿liwia wykonywanie eventów i rzeczy na podstawie animacji
    }


    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

    #endregion
}