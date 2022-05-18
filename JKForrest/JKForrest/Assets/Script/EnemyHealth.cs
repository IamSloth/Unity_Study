using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public int hp = 100;
    public RawImage imgBar;
    public void Damage(int amount)
    {
        if (hp <= 0)
            return;

        hp -= amount;
        imgBar.transform.localScale = new Vector3(hp/100.0f, 1 , 1);



        if(hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<NavMeshAgent>().isStopped = true;

            Destroy(gameObject, 2);
            GameObject.Find("GameManager").GetComponent<Spawn>().count--;
            GameObject.Find("GameManager").GetComponent<Spawn>().killCount++;

            if (GameObject.Find("GameManager").GetComponent<Spawn>().killCount == 1)
            {
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<Spawn>().kill1;
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.Play();
            }

            else if(GameObject.Find("GameManager").GetComponent<Spawn>().killCount == 2)
            {
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<Spawn>().kill2;
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.Play();
            }

            else if (GameObject.Find("GameManager").GetComponent<Spawn>().killCount == 3)
            {
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<Spawn>().kill3;
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.Play();
            }

            else if (GameObject.Find("GameManager").GetComponent<Spawn>().killCount == 4)
            {
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<Spawn>().kill4;
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.Play();
            }

            else if (GameObject.Find("GameManager").GetComponent<Spawn>().killCount == 5)
            {
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.clip = GameObject.Find("GameManager").GetComponent<Spawn>().kill5;
                GameObject.Find("GameManager").GetComponent<Spawn>().audioSource.Play();
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
