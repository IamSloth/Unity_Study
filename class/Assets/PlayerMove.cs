using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()    
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
        {
            Vector3 dir = h * Vector3.right +
                            v * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.Translate(Vector3.forward
                                        * Time.deltaTime * speed);

            GetComponent<Animator>().SetBool("bMove", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("bMove", false);

        }
    }


}
