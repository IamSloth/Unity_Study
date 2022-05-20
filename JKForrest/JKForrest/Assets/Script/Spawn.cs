using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawn : MonoBehaviour
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
    public AudioClip kill6;
    public AudioSource audioSource;

    public TextMeshProUGUI resultText;
    public TextMeshProUGUI lifeText;
    GameObject player;

    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();   
    }

    void Create()
    {
        if (killCount == max && count == max )
        {
            Instantiate(prefab[2], point[1]);
            count = count + max + 1;
            return;
        }

        if (count >= max)
        {
            return;
        }

        count++;
        int i = Random.Range(0, point.Length-1);
        int j = Random.Range(0, prefab.Length-1);
        Instantiate(prefab[j], point[i]);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Create", time, time);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(killCount == 6)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "VICTORY...!";
        }

        else if(player.GetComponent<PlayerHealth>().life < 0)
        {
            resultText.gameObject.SetActive(true);
            lifeText.text = "LIFE : 0";
            resultText.text = "YOU LOSE...";
        }
    }
}
