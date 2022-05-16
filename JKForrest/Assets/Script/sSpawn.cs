using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sSpawn : MonoBehaviour
{

    public GameObject[] prefab;
    public float time;
    public Transform[] point;

    public int max;
    public int count;

    public int killCount;

    public AudioClip kill1;
    public AudioClip kill2;
    public AudioClip kill3;
    public AudioClip kill4;
    public AudioClip kill5;
    public AudioSource audioSource;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();   
    }

    void Create()
    {
        if(count >= max)
        {
            return;
        }

        count++;
        int i = Random.Range(0, point.Length);
        int j = Random.Range(0, prefab.Length);
        Instantiate(prefab[j], point[i]);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Create", time, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
