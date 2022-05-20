using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score = 0;
    Text myScore;
    // Start is called before the first frame update
    void Start()
    {
        myScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myScore.text = "Score : " + score;
    }
}
