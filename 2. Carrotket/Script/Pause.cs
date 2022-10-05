using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseButton;

    public GameObject pauseMenu;
    public GameObject cancelButton;
    public GameObject restartButton;
    public GameObject exitButton;

    public static bool isPused = false;

    public void PauseOn()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        gameObject.SetActive(false);
        isPused = true;
        Time.timeScale = 0f;
    }

    public void CancelOn()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        gameObject.SetActive(true);
        isPused = false;
        Time.timeScale = 1f;
    }

    public void ExitOn()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log("isQuit?");
    #else
                    Application.Quit();                           
    #endif
    
    }

    public void RestartOn()
    {
        SceneManager.LoadScene(0);
        CancelOn();
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
