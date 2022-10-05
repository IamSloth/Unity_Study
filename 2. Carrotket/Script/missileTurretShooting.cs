using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missileTurretShooting : MonoBehaviour
{
    public GameObject missilePrefab;
    public int count;
    
    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;
    soundmanage sm;

    public RawImage coolDownBar;
    

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = 3f;
        target = FindObjectOfType<PlayerMove>().transform;
        sm = FindObjectOfType<soundmanage>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDownBar.transform.localScale = new Vector3((spawnRate - timeAfterSpawn) / spawnRate, 1, 1);

        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;
            GameObject missile = Instantiate(missilePrefab, transform.position, transform.rotation);
            missile.transform.LookAt(target);
            sm.playShootMissile();
            count++;
        }
    }
}
