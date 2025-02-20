using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("TienCuong/EnemyAI")]
public class EnemyAI : MonoBehaviour
{
    public enum StateEnemy
    {
        Patrol,
        Chaser,
        Attack
    }
    [Header("Patrol")]
    public Transform[] wayPoint;
    private NavMeshAgent egent;
    public float speedAgentWoking = 1.5f;
    private int currentWayPointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        egent = GetComponent<NavMeshAgent>();
        egent.speed = speedAgentWoking;
        GotoNextWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(egent.pathPending && egent.remainingDistance < 0.3f)
        {
            GotoNextWayPoint();
        }
    }
    private void GotoNextWayPoint()
    {
        if (wayPoint.Length == 0) return;
        egent.destination = wayPoint[currentWayPointIndex].position;
        currentWayPointIndex = (currentWayPointIndex+1)%wayPoint.Length;
    }
}
