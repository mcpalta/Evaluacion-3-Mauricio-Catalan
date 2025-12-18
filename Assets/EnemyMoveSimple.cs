// EnemyAI.cs
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;

    [Header("Vision")]
    public Transform eyePoint;
    public float detectionDistance = 8f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    [Header("Rotation")]
    public float rotationSpeed = 5f;

    public bool PlayerDetected { get; private set; }
    public bool IsAttacking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent != null)
            agent.updateRotation = false;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (IsAttacking) return;

        DetectPlayer();

        if (PlayerDetected && player != null)
        {
            // Actualiza destino solo si se mueve suficiente
            if (Vector3.Distance(agent.destination, player.position) > 0.5f)
                SetAgentDestination(player.position);

            agent.speed = 4.5f;
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            agent.speed = 2f;
            Patrol();

            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", agent.velocity.magnitude > 0.1f);
        }

        RotateTowardsMovement();
    }

    void DetectPlayer()
    {
        if (eyePoint == null || player == null)
        {
            PlayerDetected = false;
            return;
        }

        Vector3 origin = eyePoint.position;
        Vector3 toPlayer = (player.position - origin);
        float distance = toPlayer.magnitude;

        if (distance <= detectionDistance)
        {
            int layerMask = LayerMask.GetMask("Player");
            if (Physics.Raycast(origin, toPlayer.normalized, out RaycastHit hit, detectionDistance, layerMask))
            {
                if (hit.transform == player || hit.transform.IsChildOf(player))
                {
                    // Detecta solo si el jugador está vivo
                    Health playerHealth = player.GetComponent<Health>();
                    if (playerHealth != null && playerHealth.currentHealth <= 0)
                    {
                        PlayerDetected = false;
                    }
                    else
                    {
                        PlayerDetected = true;
                    }

                    Debug.DrawRay(origin, toPlayer.normalized * detectionDistance, Color.green);
                    return;
                }
            }
        }

        PlayerDetected = false;
        Debug.DrawRay(origin, transform.forward * detectionDistance, Color.red);
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GoToNextPatrolPoint();
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        Vector3 target = patrolPoints[currentPatrolIndex].position;

        if (SetAgentDestination(target))
        {
            // Incrementa solo si llegó al punto
            if (agent.remainingDistance <= agent.stoppingDistance + 0.1f)
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    bool SetAgentDestination(Vector3 target)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, 2.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            return true;
        }
        return false;
    }

    public void ReturnToPatrol()
    {
        PlayerDetected = false;
        IsAttacking = false;

        animator.SetBool("IsAttacking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", true);

        agent.isStopped = false;
        agent.ResetPath();

        GoToNextPatrolPoint();
    }

    void RotateTowardsMovement()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            Vector3 lookDirection = agent.velocity.normalized;
            lookDirection.y = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (eyePoint == null) return;

        Gizmos.color = PlayerDetected ? Color.green : Color.red;
        Vector3 direction = transform.forward;
        Gizmos.DrawLine(eyePoint.position, eyePoint.position + direction * detectionDistance);
    }
}
