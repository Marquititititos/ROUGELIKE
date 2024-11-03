using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrPathFinding : MonoBehaviour
{

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = GetComponent<scrEnemigoBase>().spd;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        if (agent.transform.position.x == player.transform.position.x)
        {
            agent.transform.position = new Vector2(agent.transform.position.x + 0.1f, agent.transform.position.y);
        }
    }
}
