using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;
    TextMeshProUGUI myScore;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        myScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        myScore.text = "Score : " + score;
    }
}
