using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject coinPrefab;
    public int count = 3;

    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;

    private float timeBetSpawn;

    public float yMin = -3.5f;
    public float yMax = 1.5f;
    private float xPos = 30f;

    private GameObject[] platforms;
    private GameObject[] coin;
    private int currentIndex = 0;

    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[count];
        coin = new GameObject[count]; 

        for(int i = 0; i< count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
            coin[i] = Instantiate(coinPrefab, poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameover)
        {
            return;
        }

        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            float yPos = Random.Range(yMin, yMax);

            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            coin[currentIndex].SetActive(false);
            coin[currentIndex].SetActive(true);
            for(int i = 0; i < 5; i++)
            {
                coin[currentIndex].transform.GetChild(i).gameObject.SetActive(true);
            }
            

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            coin[currentIndex].transform.position = new Vector2(xPos+Random.Range(0,10), yPos+ Random.Range(0, 5));
            currentIndex++;

            if(currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
