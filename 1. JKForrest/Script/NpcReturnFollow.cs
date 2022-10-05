using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcReturnFollow : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    Vector3 posReturn;

    public float maxDistance = 5;
    public float minDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        posReturn = transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        
        if (dist > maxDistance)
        {
            nav.SetDestination(posReturn);

            if(Vector3.Distance(transform.position, posReturn) >= 3.1)
            {
                GetComponent<Animator>().SetBool("bMove", true);
                
            }

            else
            {
                GetComponent<Animator>().SetBool("bMove", false);
            }
        }

        else if(dist > minDistance)
        {
            nav.SetDestination(player.position);
            GetComponent<Animator>().SetBool("bMove", true);
        }

        else
        {
            nav.SetDestination(transform.position);
            GetComponent<Animator>().SetBool("bMove", false);
        }
    }
}
