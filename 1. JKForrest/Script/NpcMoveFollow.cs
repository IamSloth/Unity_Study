using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcMoveFollow : MonoBehaviour
{

    Transform player;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerHealth>().hp >0)
        {
            nav.SetDestination(player.position);
        }
        
    }
}
