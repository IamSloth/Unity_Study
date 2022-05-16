using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class sEnemyHealth : MonoBehaviour
{

    public int hp = 100;
    public void Damage(int amount)
    {
        if (hp <= 0)
            return;

        hp -= amount;
        if(hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<NavMeshAgent>().enabled = false;

            Destroy(gameObject, 2);
            GameObject.Find("GameManager").GetComponent<sSpawn>().count--;
            GameObject.Find("GameManager").GetComponent<sSpawn>().killCount++;

            if (GameObject.Find("GameManager").GetComponent<sSpawn>().killCount == 1)
            {
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<sSpawn>().kill1;
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.Play();
            }

            else if(GameObject.Find("GameManager").GetComponent<sSpawn>().killCount == 2)
            {
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<sSpawn>().kill2;
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.Play();
            }

            else if (GameObject.Find("GameManager").GetComponent<sSpawn>().killCount == 3)
            {
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<sSpawn>().kill3;
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.Play();
            }

            else if (GameObject.Find("GameManager").GetComponent<sSpawn>().killCount == 4)
            {
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<sSpawn>().kill4;
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.Play();
            }

            else if (GameObject.Find("GameManager").GetComponent<sSpawn>().killCount == 5)
            {
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<sSpawn>().kill5;
                GameObject.Find("GameManager").GetComponent<sSpawn>().audioSource.Play();
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
