using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcHealing : MonoBehaviour
{

    GameObject player;
    float time;
    bool bInRange;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 1.0f && bInRange)
        {
            time = 0;
            player.GetComponent<PlayerHealth>().Healing(30);
        }
    }
}
