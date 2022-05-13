using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;

    Vector3 offset = new Vector3(0.3f, 0.3f, 0.3f);

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator enemyAnimator;
    bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        enemyAnimator = GetComponentInChildren<Animator>();

    }

    
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius )
        {

            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                    enemyAnimator.SetBool("canAttack",true);

                }


                FaceTarget();
            }
            else {
                enemyAnimator.SetBool("canAttack", false);

            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void FaceTarget()
    {

        Vector3 Direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0f, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}