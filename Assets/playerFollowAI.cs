using UnityEngine;
using UnityEngine.AI;

public class playerFollowAI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination =  player.position;
        
    }
}
