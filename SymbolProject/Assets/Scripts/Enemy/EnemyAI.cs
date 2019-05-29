using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    //public Animator anim; 敵のアニメーションを作ってから、ここから設定する

    //敵の動きのリスト
    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking
    };

   //UnityからcurrentState, waitAtPoint, chaseRange, attackRange, timebetweenAttacks　設定できる。敵のスピードはNav Mesh Agentから。
    public AIState currentState;
    public float waitAtPoint = 2f;
    private float waitCounter;
    public float chaseRange;
    public float attackRange = 1f;
    public float timeBetweenAttacks = 2f;
    private float attackCounter;

    void Start()
    {
        waitCounter = waitAtPoint;
    }

     void Update()
    {
        float distancetoPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        {
            //敵の動きはswitchで設定
            switch (currentState)
            {
                case AIState.isIdle:
                    //anim.SetBool("IsMoving", false); 敵のアニメーションを作ってから、ここから設定する

                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                    }
                    else
                    {
                        currentState = AIState.isPatrolling;
                        agent.SetDestination(PatrolPoints[currentPatrolPoint].position);
                    }

                    if (distancetoPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                        //anim.SetBool("isMoving", true);敵のアニメーションを作ってから、ここから設定する
                    }

                    break;

                case AIState.isPatrolling:

                    agent.SetDestination(PatrolPoints[currentPatrolPoint].position);

                    if (agent.remainingDistance <= .2f)
                    {
                        currentPatrolPoint++;
                        if (currentPatrolPoint >= PatrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                       
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;
                    }

                    if (distancetoPlayer <= chaseRange)
                    {
                        currentState = AIState.isChasing;
                    }

                    //anim.SetBool("IsMoving", true);敵のアニメーションを作ってから、ここから設定する

                    break;

                case AIState.isChasing:

                    agent.SetDestination(PlayerController.instance.transform.position);

                    if (distancetoPlayer <= attackRange)
                    {
                        currentState = AIState.isAttacking;
                        //anim.SetTrigger("Attack");敵のアニメーションを作ってから、ここから設定する
                        //anim.SetBool("IsMoving", false);敵のアニメーションを作ってから、ここから設定する

                        agent.velocity = Vector3.zero;
                        agent.isStopped = true;

                        attackCounter = timeBetweenAttacks;
                    }

                    if (distancetoPlayer > chaseRange)
                    {
                        currentState = AIState.isIdle;
                        waitCounter = waitAtPoint;

                        agent.velocity = Vector3.zero;
                        agent.SetDestination(transform.position);
                    }

                    break;

                case AIState.isAttacking:

                    transform.LookAt(PlayerController.instance.transform, Vector3.up);
                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                    attackCounter -= Time.deltaTime;
                    if (attackCounter <= 0)
                    {
                        if (distancetoPlayer < attackRange)
                        {
                            //anim.SetTrigger("Attack");敵のアニメーションを作ってから、ここから設定する
                            attackCounter = timeBetweenAttacks;
                        }
                        else
                        {
                            currentState = AIState.isIdle;
                            waitCounter = waitAtPoint;

                            agent.isStopped = false;
                        }

                    }

                    break;
            }
        }
    }

}
    