using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carrotTurretShooting : MonoBehaviour
{

    public GameObject carrotPrefab;

    public float spawnRateMin = 1.0f;
    public float spawnRateMax = 5.0f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    soundmanage sm;

    public RawImage coolDownBar;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = spawnRateMin;
        target = FindObjectOfType<PlayerMove>().transform;
        sm = FindObjectOfType<soundmanage>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownBar.transform.localScale = new Vector3((spawnRate - timeAfterSpawn) / spawnRate, 1, 1);

        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn >= spawnRate)
        {
            
            timeAfterSpawn = 0;
            int ranNum = Random.Range(1, 3);
            for(int i = 0;  i < ranNum; i++)
            {
                GameObject carrot = Instantiate(carrotPrefab, transform.position, transform.rotation);
                carrot.transform.LookAt(target);
            }
            sm.playShootCarrot();
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        }
        

    }
}
