using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static float timeCurrent;
    public float timeMax = 40f;
    bool isEnded;
    TextMeshProUGUI timerText;

    void CheckTimer()
    {
        if(0 < timeCurrent)
        {
            timeCurrent -= Time.deltaTime;
            timerText.text = timeCurrent.ToString("F0");

        }

        else if (!isEnded)
        {
            EndTimer();
        }
    }

    void EndTimer()
    {
        timeCurrent = 0;
        timerText.text = timeCurrent.ToString("F0");
        isEnded = true;
    }

    void ResetTimer()
    {
        timeCurrent = timeMax;
        timerText.text = timeCurrent.ToString("F0");
        isEnded=false;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();   
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(Starting.isGameStart == true)
        {
            if (isEnded)
                return;

            CheckTimer();
        }
        
    }
}
