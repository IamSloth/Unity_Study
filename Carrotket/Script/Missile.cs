using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject myMissile;
    public GameObject missileObj;
    public GameObject explosion;
    Rigidbody rb;
    GameObject player;
    int millisCount;

    soundmanage sm;

    // Start is called before the first frame update
    void Start()
    {
        millisCount = FindObjectOfType<missileTurretShooting>().count;
        rb = GetComponent<Rigidbody>();
        speed += millisCount/2;
        rb.velocity = transform.forward * speed;
        player = GameObject.Find("Player");
        sm = FindObjectOfType<soundmanage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Carrot")
        {
            Destroy(other.gameObject);
        }

        else if(other.tag == "Fence")
        {
            missileObj.SetActive(false);
            explosion.SetActive(true);
            rb.velocity = Vector3.zero;
            sm.playExplosionMissile();
            Destroy(myMissile,1);
        }

        else if(other.tag == "Player" && player.GetComponent<PlayerMove>().isAttacked == false)
        {
            missileObj.SetActive(false);
            explosion.SetActive(true);
            rb.velocity = Vector3.zero;

            player.transform.LookAt(transform);
            player.GetComponent<PlayerMove>().Attacked();

            Destroy(myMissile,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
