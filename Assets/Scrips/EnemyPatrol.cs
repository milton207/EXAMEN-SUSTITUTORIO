using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    public Transform player;
    public float chaseRange = 7f; // Distancia a la que detecta al jugador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Buscar al jugador automáticamente si no está asignado
        if (player == null)
        {
            GameObject pObj = GameObject.FindGameObjectWithTag("Player");
            if (pObj != null) player = pObj.transform;
        }

        // Ir al primer waypoint
        GotoNextWaypoint();
    }

    void GotoNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void Update()
    {
        // 1. Si el jugador está cerca, perseguirlo
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= chaseRange)
            {
                agent.destination = player.position;
                return; // Salir para que no patrulle mientras persigue
            }
        }

        // 2. Si llegó al waypoint actual, pasar al siguiente
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextWaypoint();
        }
    }

    // Detectar si atrapa al jugador para activar el GameOver
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}