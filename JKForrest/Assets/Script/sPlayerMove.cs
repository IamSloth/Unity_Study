using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerMove : MonoBehaviour
{

    public float speed = 2;
    public float jumpForce = 5f;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 dir = h * Vector3.right + v * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            GetComponent<Animator>().SetBool("Walk", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 20;
        }

        else
            speed = 2;

    }
}
