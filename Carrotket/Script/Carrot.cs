using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float carrotSpeed = 3.0f;
    Rigidbody rb;
    Transform playerT;
    PlayerMove playerS;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerS.eatCarrot();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerT = GameObject.Find("Player").transform;
        playerS = GameObject.Find("Player").GetComponent<PlayerMove>();
        rb.velocity = transform.forward * carrotSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
