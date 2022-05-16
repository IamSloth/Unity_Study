using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerAttack : MonoBehaviour
{
    float timer;
    LineRenderer line;
    Transform shootPoint;

    void Shoot()
    {
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Shootable")))
        {
            sEnemyHealth e = hit.collider.GetComponent<sEnemyHealth>();
            if(e != null)
            {
                e.Damage(50);
            }
            line.enabled = true;
            line.SetPosition(0, shootPoint.position);
            line.SetPosition(1, hit.point);
        }

        else
        {
            line.enabled = true;
            line.SetPosition(0, shootPoint.position);
            line.SetPosition(1, shootPoint.position + ray.direction * 10);
        }

        
    }
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        shootPoint = transform.Find("ShootPoint");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetMouseButton(0) && timer >= 0.2f)
        {
            timer = 0;
            Shoot();
        }

        if(timer > 0.05f)
        {
            line.enabled = false;
        }
        
    }
}
