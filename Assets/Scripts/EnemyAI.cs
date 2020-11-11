using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
        // If enemy is close enough to player, enemy is provoked and will engage (chase) the player
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget < chaseRange) 
        {
            isProvoked = true;
            // navMeshAgent.SetDestination(target.position);
        }
    }

    private void EngageTarget()
        // Enemy will chase player until close enough, then will attack
    {
        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget(); 
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // Get global velocity from Nav Mesh Agent
        // Convert it to a local velocity of the character
        // pass local speed into value of blend tree

        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.SetFloat("forwardSpeed", speed);
        }
    }


private void AttackTarget()
    {
        Debug.Log(name + " has seeked and is destroying " + target.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
