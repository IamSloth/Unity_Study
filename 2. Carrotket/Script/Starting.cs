using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Starting : MonoBehaviour
{
    public static bool isGameStart = false;
    public static bool isGameEnd = false;

    GameObject timer;
    GameObject score;
    GameObject control;
    GameObject pauseButton;
    GameObject pauseMenu;
    GameObject title;
    GameObject introText;
    GameObject player;
    GameObject turret;

    GameObject opCam;
    public GameObject mainCam;

    public GameObject opCamPos;
    public GameObject opPlayerPos;



    void Opning()
    {
        isGameStart = false;
        isGameEnd = true;

        timer.SetActive(false);
        score.SetActive(false);
        control.GetComponent<Canvas>().enabled = false;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        turret.SetActive(false);
        mainCam.SetActive(false);

        opCam.SetActive(true);
        title.SetActive(true);
        introText.SetActive(true);
    }

    void GameStart()
    {
        isGameStart = true;
        isGameEnd = false;

        timer.SetActive(true);
        score.SetActive(false);
        control.GetComponent<Canvas>().enabled = true;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        turret.SetActive(true);
        mainCam.SetActive(true);
        player.transform.position = new Vector3(0, 0, 0);

        opCam.SetActive(false);
        title.SetActive(false);
        introText.SetActive(false);
    }

    void GameEnd()
    {

        Debug.Log("isWoking?");

        isGameStart = false;
        isGameEnd = true;

        player.transform.position = opPlayerPos.transform.position;
        player.transform.rotation = opPlayerPos.transform.rotation;

        mainCam.transform.position = opCamPos.transform.position;
        mainCam.transform.rotation = opCamPos.transform.rotation;

        mainCam.GetComponent<Camera>().fieldOfView = 85;

        timer.SetActive(false);
        score.SetActive(false);
        control.GetComponent<Canvas>().enabled = false;
        turret.SetActive(false);

        player.GetComponent<PlayerMove>().rb.velocity = Vector3.zero;
        player.GetComponent<Animator>().SetBool("Run",false);
        player.GetComponent<Animator>().speed = 0.3f;

        title.SetActive(true);
        introText.SetActive(true);
        introText.GetComponent<TextMeshProUGUI>().text = "You Got " + Score.score + " Carrot :)";
        

    }

    void Start()
    {
        timer = GameObject.Find("Timer");
        score = GameObject.Find("Score");
        control = GameObject.Find("MobileSingleStickControl");
        pauseButton = GameObject.Find("Button_Pause");
        pauseMenu = GameObject.Find("PauseMenu");
        title = GameObject.Find("myTitle2");
        introText = GameObject.Find("Intro");
        player = GameObject.Find("Player");
        turret = GameObject.Find("TurretGroup");
        opCam = GameObject.Find("OpningCamera");

        Opning();


    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && isGameStart == false && isGameEnd == true)
        {
            Debug.Log("start?");
            GameStart();
        }

        if (Timer.timeCurrent <= 0 && isGameEnd == false && isGameStart == true)
        {
            GameEnd();
            isGameStart = true;
            isGameEnd = true;
        }

    }
}
