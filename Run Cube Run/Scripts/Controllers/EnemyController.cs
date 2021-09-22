using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Manager manager;
    PlayerController player;
    NavMeshAgent agent;

    void Start()
    {
        manager = Manager.GetInstance();
        player = manager.player;
        agent = GetComponent<NavMeshAgent>();
    }

    void LateUpdate()
    {
        agent.SetDestination(player.transform.position); //Chase Player
        if (Vector3.Distance(transform.position, player.transform.position) < 0.85f) //Enemy caught player, Game Over!
        {
            manager.ChangeScene(false); //Player Has To Play Same Scene 
        }
    }
}