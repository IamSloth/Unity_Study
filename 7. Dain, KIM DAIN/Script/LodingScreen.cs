using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LodingScreen : MonoBehaviour
{
    
    public GameObject loding;
        
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LodingOut());
        
    }
    
    IEnumerator LodingOut()
    {
        Image lodingImage = loding.GetComponent<Image>();
        yield return new WaitForSeconds(5f);
        float alphaColor = 1;
        while (alphaColor >= 0f)
        {
            Debug.Log(alphaColor);
            alphaColor -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            lodingImage.color = new Color(255, 255, 255, alphaColor);
        }
        loding.SetActive(false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
