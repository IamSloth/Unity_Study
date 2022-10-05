using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;
    // Start is called before the first frame update

    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(36f, 0);
        transform.position = offset;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -26.5f)
        {
            Reposition();
        }
    }
}
