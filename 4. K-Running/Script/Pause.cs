using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;

    public static bool isPause;
    public static bool isPauseBtn;

    public void myPause()
    {
        pauseCanvas.SetActive(true);
        isPause = true;
        Time.timeScale = 0f;
    }

    public void myContinue()
    {
        pauseCanvas.SetActive(false);
        isPause = false;
        Time.timeScale = 1f;
    }
    
    public void myQuit()
    {
        SceneManager.LoadScene(0);
        isPause = false;
        Time.timeScale = 1f;
    }

    public void onPauseBtn()
    {
        isPauseBtn = true;
    }

    public void offPauseBtn()
    {
        isPauseBtn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
