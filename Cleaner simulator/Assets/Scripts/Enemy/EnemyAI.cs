using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private List<GameObject> players;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private GameObject currentTarget;

    [SerializeField] private string searchingTags;
    [SerializeField] private float SightRange;

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRange;
    [SerializeField] private bool alreadyAttacked;

    [SerializeField] private bool playerInAttackRange, playerInSightRange;

    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    [SerializeField] private float walkPointReachingDistance = 1f;

    void Start()
    {

        agent = transform.GetComponent<NavMeshAgent>();
        UpdatePlayersArray();
    }

    private void Update()
    {
        UpdatePlayersArray();
        FindTargets();
        SetCurrentTarget();
        playerInSightRange = currentTarget != null;
        playerInAttackRange = playerInSightRange && Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange;

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void UpdatePlayersArray()
    {
        players = GameObject.FindGameObjectsWithTag(searchingTags).ToList();
    }

    private void FindTargets()
    {
        targets.Clear();
        foreach (GameObject p in players)
        {
            GameObject player = p;
            if (Vector3.Distance(transform.position, p.transform.position) <= SightRange)
            {
                targets.Add(player);
            }
        }
    }

    private void SetCurrentTarget()
    {
        if (targets.Count != 0)
            currentTarget = targets[Random.Range(0, targets.Count)];
        else
            currentTarget = null;
    }

    private void Patroling() 
    {
        if (!walkPointSet) 
            SearchWalkPoint();
        if (walkPointSet) 
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            SearchWalkPoint();
        }

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // луч перпендикулярно вниз от полученных координат для проверки, есть ли там земля
        walkPointSet = Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround);

    }

    private void ChasePlayer()
    {
        agent.SetDestination(currentTarget.transform.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("Атака");
        agent.SetDestination(currentTarget.transform.position);
        transform.LookAt(currentTarget.transform);

        if (!alreadyAttacked)
        {
            // тут будет код атаки
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


}
