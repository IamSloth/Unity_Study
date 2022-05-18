using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    GameObject player;
    float time;
    bool bInRange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            bInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            bInRange= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 0.5f && bInRange)
        {
            time = 0;
            player.GetComponent<PlayerHealth>().Damage(50);
            
        }

        if (player.GetComponent<PlayerHealth>().hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("PlayerDeath");
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        else
        {
            GetComponent<Animator>().SetTrigger("PlayerRespawn");
            if(GetComponent<EnemyHealth>().hp > 0)
            {
                GetComponent<NavMeshAgent>().isStopped = false;
            }
            
        }
    }
}
