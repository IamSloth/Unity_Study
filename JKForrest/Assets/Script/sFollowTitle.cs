using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sFollowTitle : MonoBehaviour
{
    Transform p;

    SpriteRenderer sr;
    public GameObject go;
    private float timer;
    bool isTitle;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        p = GameObject.Find("Player").transform;
        sr = go.GetComponent<SpriteRenderer>();
        isTitle = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = p.position + new Vector3(0, 6, 5);

        timer += Time.deltaTime;
        if ((timer >= 2) && (isTitle == false))
        {
            StartCoroutine("FadeOut");
            isTitle = true;
            //Destroy(gameObject);
        }   
    }

    
    IEnumerator FadeOut()
    {
        for (int i = 10; i>= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}


