using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameover = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI recordText;
    public GameObject gameoverUI;
    public GameObject gameoverUICanvas;

    private int score = 0;

    public GameObject[] background;
    int backgroundIndex = 0;

    public AudioClip[] bgm;
    AudioSource AS;
    int bgmIndex = 0;

    private void Awake()
    {

        Application.targetFrameRate = 60;

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }

        backgroundIndex = Random.Range(0, background.Length);
        background[backgroundIndex].SetActive(true);

        bgmIndex = Random.Range(0, bgm.Length);
        AS = this.GetComponent<AudioSource>();
        AS.clip = bgm[bgmIndex];
        AS.Play();

    }


    public void AddScore(int newScore)
    {
        if(!isGameover)
        {
            score += newScore;
            scoreText.text = ""+score;
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
        gameoverUICanvas.SetActive(true);
        gameoverUICanvas.transform.GetChild(0).gameObject.SetActive(false);
        gameoverUICanvas.transform.GetChild(1).gameObject.SetActive(false);

        float bestScore = PlayerPrefs.GetFloat("BestScore");

        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        recordText.text = "Best Score: " + (int)bestScore;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
