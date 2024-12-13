using UnityEngine;

public class EnemyAnimatorSpeed : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private new Rigidbody rigidbody;

    private void Update()
    {
        animator.SetFloat("speed", rigidbody.linearVelocity.magnitude);
    }
}
