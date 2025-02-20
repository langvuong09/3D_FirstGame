using UnityEngine;
using UnityEngine.AI;
[AddComponentMenu("TienCuong/AI")]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }
    private void Update()
    {
        destination = target.position;
        agent.destination = destination;
    }
}
