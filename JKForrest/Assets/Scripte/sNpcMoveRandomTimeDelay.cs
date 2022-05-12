using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sNpcMoveRandomTimeDelay : MonoBehaviour
{

    public float speed = 5;
    
    private float timer;
    private float waitingTime;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        waitingTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime)
        {
            float h = Random.Range(-1000f, 1000f);
            float v = Random.Range(-1000f, 1000f);

            if (h != 0 || v != 0)
            {
                Vector3 dir = h * Vector3.right + v * Vector3.forward;
                transform.rotation = Quaternion.LookRotation(dir);
            }
            timer = 0;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
