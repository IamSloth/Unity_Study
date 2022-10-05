using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OpeningScene : MonoBehaviour
{
    public TextMeshProUGUI startText;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }

        if(time < 0.5f)
        {
            startText.color = new Color(1, 1, 1, 1 - time);
        }

        else
        {
            startText.color = new Color(1, 1, 1, time);
            if(time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;

    }
}
