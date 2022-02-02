using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    public int EnemyHealth = 100;
    public GameObject[] debris;
    public float debrisSpeed = 100f;
    public int points = 100;
    bool gotDestroyed = false;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float attackForce = 32f;
    public float upwardAttackForce = 8f;
    public Transform attackPoint;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //animations
    public Animation[] walkAnimations;
    public string[] walkAnimName;
    public Animation[] attackAnimations;
    public string[] attackAnimName;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(EnemyHealth<=0)
            EnemyDestroying();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInSightRange && !playerInAttackRange && !gotDestroyed)
        {
            Patrolling();
            walkAnim();
        }
        else if (playerInSightRange && !playerInAttackRange && !gotDestroyed)
        {
            ChasePlayer();
            walkAnim();
        }
        else if (playerInAttackRange && !gotDestroyed)
        {
            AttackPlayer();
            attackAnim();
        }
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        else 
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);
            rb.AddForce(transform.up * upwardAttackForce, ForceMode.Impulse);
            
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(int dmg)
    {
        EnemyHealth -= dmg;
    }
    private void EnemyDestroying()
    {
        gotDestroyed = true;
        spawnDebris();
        player.GetComponent<PlayerScore>().plusCombo(points);
        Destroy(gameObject);
    }
    private void spawnDebris()
    {
        GameObject debri;
        float random;
        for(int i=0; i<debris.Length; ++i)
        {
            debri = Instantiate(debris[i], transform.position, Quaternion.identity);
            if (debri.GetComponent<Rigidbody>() == null)
            {
                print("Aaa where is rigidbody component aaa");
                continue;
            }
            random = Random.Range(-1f, 1f);
            //debri.GetComponent<Rigidbody>().AddExplosionForce(debrisSpeed, Vector3.up * debrisSpeed, 10f);
            debri.GetComponent<Rigidbody>().AddTorque(new Vector3(random, random, random) * debrisSpeed);
            debri.GetComponent<Rigidbody>().AddForce(new Vector3(random, random, random) * debrisSpeed, ForceMode.Impulse);
            
            debri.GetComponent<Rigidbody>().AddForce(Vector3.up * debrisSpeed, ForceMode.Impulse);
        }

    }
    private void walkAnim()
    {
        for (int i = 0; i <walkAnimations.Length; ++i)
            walkAnimations[i].Play(walkAnimName[i]);
    }
    private void attackAnim()
    {
        for (int i = 0; i < attackAnimations.Length; ++i)
            attackAnimations[i].Play(attackAnimName[i]);
    }
}
