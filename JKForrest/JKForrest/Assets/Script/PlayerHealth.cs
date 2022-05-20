using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public int hp = 100;
    public int life = 5;

    Vector3 posRespawn;

    bool bDamage;
    public RawImage imgDamage;
    public RawImage imgBar;
    public Slider sliderHP;

    public TextMeshProUGUI resultText;

    public void Damage(int amount)
    {
        if (hp <= 0)
            return;

        hp -= amount;

        bDamage = true;
        imgBar.transform.localScale = new Vector3(hp / 100.0f, 1, 1);
        sliderHP.value = hp;

        if(hp <= 0)
        {
            life--;

            if (life >= 0)
            {
                Invoke("Respawn", 3);
            }

            else
            {
                
            }

            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
        }
    }

    bool bHealing;

    public void Healing(int amount)
    {
        hp += amount;

        if (hp >= 100)
        {
            hp = 100;
        }

        bHealing = true;
        imgBar.transform.localScale = new Vector3(hp / 100.0f, 1, 1);
        sliderHP.value = hp;
    }

    public void Respawn()
    {
        hp = 100;

        transform.position = posRespawn;
        GetComponent<Animator>().SetTrigger("Respawn");
        GetComponent<PlayerMove>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;

        imgBar.transform.localScale = new Vector3(1, 1, 1);
        sliderHP.value=hp;

    }


    // Start is called before the first frame update
    void Start()
    {
        posRespawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(bDamage == true)
        {
            imgDamage.color = new Color(1, 0, 0, 1);
        }
        
        else if (bHealing == true)
        {
            imgDamage.color = new Color(0, 1, 0, 1);
        }

        else
        {
            imgDamage.color = Color.Lerp(imgDamage.color, Color.clear, 10 * Time.deltaTime);
        }

        bDamage = false;
        bHealing = false;
    }
}
