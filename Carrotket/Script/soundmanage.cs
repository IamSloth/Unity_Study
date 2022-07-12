using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanage : MonoBehaviour
{
    public AudioClip playerHurtClip;
    public AudioClip eatCarrotClip;
    public AudioClip shootMissileClip;
    public AudioClip shootCarrotClip;
    public AudioClip explosionMissileClip;

    public void playPlayerHurt()
    {
        GetComponent<AudioSource>().PlayOneShot(playerHurtClip);
    }

    public void playEatCarrot()
    {
        GetComponent<AudioSource>().PlayOneShot(eatCarrotClip);
    }

    public void playShootMissile()
    {
        GetComponent<AudioSource>().PlayOneShot(shootMissileClip);
    }

    public void playShootCarrot()
    {
        GetComponent<AudioSource>().PlayOneShot(shootCarrotClip);
    }

    public void playExplosionMissile()
    {
        GetComponent <AudioSource>().PlayOneShot(explosionMissileClip);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
