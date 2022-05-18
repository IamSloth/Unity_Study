using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoveRandom : MonoBehaviour
{

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float h = Random.Range(-10f, 10f);
        float v = Random.Range(-10f, 10f);

        if(h != 0 || v!= 0)
        {
            Vector3 dir = h * Vector3.right + v * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
