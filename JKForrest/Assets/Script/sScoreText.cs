using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sScoreText : MonoBehaviour
{
    public Text myText;
    
    

    // Start is called before the first frame update
    void Start()
    {
        //myText.text = "Kill Score : 0";

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<sSpawn>().killCount >= 5)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            myText.text = "½£À» ÁöÄ×½À´Ï´Ù!";
        }

        else
        {
            int killCount = GameObject.Find("GameManager").GetComponent<sSpawn>().killCount;
            myText.text = "Kill Score : " + killCount.ToString();
        }

        
    }
}
