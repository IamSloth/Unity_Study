using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float timer;

    LineRenderer line;
    Transform shootPoint;
    public Material attackRangeColor;

    void Shoot()
    {
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 20, LayerMask.GetMask("Shootable")))
        {
            EnemyHealth e = hit.collider.GetComponent<EnemyHealth>();
            if (e != null)
            {
                if(e.isBoss == false)
                {
                    e.Damage(50);
                    Score.score += 10;
                }

                else
                {
                    e.Damage(30);
                    Score.score += 30;
                }
                
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


    //밀리어텍 구현해야됨

    BoxCollider hitBox;

    void MeleeAttack()
    {

        Collider[] hitEnemies = Physics.OverlapBox(hitBox.transform.position, hitBox.size*0.5f,hitBox.transform.rotation,LayerMask.GetMask("Shootable"));
        foreach (Collider c in hitEnemies)
        {
            EnemyHealth enemyHealth = c.GetComponent<EnemyHealth>();

            if (enemyHealth != null && enemyHealth.isBoss == false)
            {
                enemyHealth.Damage(50);
                Score.score += 10;
            }

            else
            {
                enemyHealth.Damage(50);
                Score.score += 50;
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        shootPoint = transform.Find("ShootPoint");
        hitBox = GameObject.Find("AttackRange").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f && Input.GetMouseButton(0) && GetComponent<Animator>().GetBool("ClawAttack") == false)
        {
            timer = 0;      
            GetComponent<Animator>().SetBool("ClawAttack",true);
            attackRangeColor.color = new Color(1,0,0,1);
        }

        else if(timer >= 0.7f && (GetComponent<Animator>().GetBool("ClawAttack") == true))
        {
            timer = 0;
            MeleeAttack();
            //Shoot();
            GetComponent<Animator>().SetBool("ClawAttack", false);
        }

        if(timer > 0.05f)
        {
            line.enabled = false;
        }

        attackRangeColor.color = Color.Lerp(attackRangeColor.color, Color.clear, 10 * Time.deltaTime);

    }
}
