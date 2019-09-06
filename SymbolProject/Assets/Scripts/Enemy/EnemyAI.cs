using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;

    //敵の動きのリスト
    public enum AIState
    {
        isIdle, isPatrolling, isChasing, isAttacking
    };

   //UnityからcurrentState, waitAtPoint, chaseRange, attackRange, timebetweenAttacks　設定できる。敵のスピードはNav Mesh Agentから。
    public AIState currentState;
    public float waitAtPoint = 2f;
    private float waitCounter;
    public float chaseRange  = 4f;
    public float attackRange = 2.7f;
    public float timeBetweenAttacks = 3f;
    private float attackCounter;

    private float distancetoPlayer;

    private Animator anim;
    private string key_IsMoving = "IsMoving";
    private string key_Attack = "Attack";
    private string key_SAttack = "SAttack";

    private float attackAnimTime;
    private float resetAttackTime = 0.17f;
    private bool isAttackAnim;

    void Start()
    {
        currentState = AIState.isIdle;
        waitCounter = waitAtPoint;

        anim = GetComponent<Animator>();
        attackAnimTime = resetAttackTime;
    }

     void Update()
    {
        if (isAttackAnim)
        {
            attackAnimTime -= Time.deltaTime; ;
        }
        if (attackAnimTime <= 0)
        {
            isAttackAnim = false;
            attackAnimTime = resetAttackTime;
            int attack = GetComponent<EnemyController>().GetAttack;
            PlayerController.instance.GetComponent<PlayerStatus>().DownHP(attack);
            if (gameObject.name == "Boss")
            {
                Enemy_SoundManager.instance.PlaySE_enemy(5);
            }
            else
            {
                Enemy_SoundManager.instance.PlaySE_enemy(2);
            }
        }

            distancetoPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
       
            //敵の動きはswitchで設定
            switch (currentState)
            {
                case AIState.isIdle:
                    anim.SetBool(key_IsMoving, false);

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
                        anim.SetBool(key_IsMoving, true);
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

                    anim.SetBool(key_IsMoving, true);
                    break;

                case AIState.isChasing:
                
                    agent.SetDestination(PlayerController.instance.transform.position);
                    
                    if (distancetoPlayer <= attackRange)
                    {
                        currentState = AIState.isAttacking;
                        //anim.SetTrigger(key_Attack);
                        anim.SetBool(key_IsMoving, false);


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
                            anim.SetTrigger(key_Attack);
                            attackCounter = timeBetweenAttacks;
                            isAttackAnim = true;

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
    