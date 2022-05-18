using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{

    public int hp = 100;
    Vector3 posRespawn;
    public void Damage(int amount)
    {
        if (hp <= 0)
            return;

        hp -= amount;

        if(hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;

            Invoke("Respawn", 3);
        }
    }

    public void Respawn()
    {
        hp = 100;

        transform.position = posRespawn;
        GetComponent<Animator>().SetTrigger("Respawn");
        GetComponent<PlayerMove>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;

    }


    // Start is called before the first frame update
    void Start()
    {
        posRespawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
