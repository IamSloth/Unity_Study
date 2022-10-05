using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody rb;
    Animator myAni;
    soundmanage sm;

    public bool isAttacked = false;
    float attackAfterTime = 0;

    // Start is called before the first frame update

    public void Attacked()
    {
        if (isAttacked)
        {
            return;
        }

        else if(!isAttacked)
        {
            isAttacked = true;
            attackAfterTime = 0;
            myAni.SetBool("Run", false);
            transform.Translate(Vector3.back * 0.8f);
            sm.playPlayerHurt();
        }
        
    }

    public void eatCarrot()
    {
        sm.playEatCarrot();
        Score.score++;
        transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        if (speed > 0.5f)
        {
            speed -= 0.02f;
        }

        else
        {
            speed = 0.5f;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAni = GetComponent<Animator>();
        sm = FindObjectOfType<soundmanage>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isAttacked == false && Starting.isGameStart == true && Starting.isGameEnd == false)
        {
            float xInput = CrossPlatformInputManager.GetAxis("Horizontal");
            float zInput = CrossPlatformInputManager.GetAxis("Vertical");

            float xKeyInput = Input.GetAxis("Horizontal");
            float zKeyInput = Input.GetAxis("Vertical");

            if (xInput != 0 || zInput != 0)
            {
                Vector3 newVelocity = new Vector3(xInput * speed, 0f, zInput * speed);
                transform.rotation = Quaternion.LookRotation(newVelocity);
                myAni.SetBool("Run", true);
                myAni.speed = speed;
                rb.velocity = newVelocity;

            }

            else if (xKeyInput != 0 || zKeyInput != 0)
            {
                Vector3 newVelocity = new Vector3(xKeyInput * speed, 0f, zKeyInput * speed);
                transform.rotation = Quaternion.LookRotation(newVelocity);
                myAni.SetBool("Run", true);
                myAni.speed = speed;
                rb.velocity = newVelocity;
            }

            else
            {
                myAni.SetBool("Run", false);
                rb.velocity = Vector3.zero;
            }

        }

        if(isAttacked == true)
        {
            attackAfterTime += Time.deltaTime;
            if (attackAfterTime >= 0.5f)
            {
                isAttacked = false;
            }
        }

    }

}
