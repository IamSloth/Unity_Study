using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public GameObject obj0;
    public GameObject obj1;
    public GameObject obj2;

    private RawImage rawImage0;
    private RawImage rawImage1;
    private RawImage rawImage2;

    public float fadeTime = 2f;
    float accumTime = 0f;

    private Coroutine fadeCor;

    AudioSource audioSource;

    public GameObject bgmObject;

    // Start is called before the first frame update
    private void Awake()
    {
        rawImage0 = obj0.GetComponent<RawImage>();
        rawImage1 = obj1.GetComponent<RawImage>();
        rawImage2 = obj2.GetComponent<RawImage>();
        audioSource = GetComponent<AudioSource>();
    }

    bool fading = false;

    private void OnEnable()
    {
        StartFadeIn();
        Invoke("Yorosiku", 0.5f);
        Invoke("StartFadeOut", fadeTime);

    }

    public void Yorosiku()
    {
        audioSource.Play();
    }

    public void GotoMain()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(bgmObject);
        bgmObject.GetComponent<AudioSource>().Play();
    }

    public void StartFadeIn()
    {
        if(fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine("FadeIn");
        fading = true;
    }

    public void StartFadeOut()
    {
        if(fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine("FadeOut");
    }

    private IEnumerator FadeIn()
    {
        accumTime = 0f;
        Color c0 = rawImage0.color;

        while(accumTime < fadeTime)
        {
            c0.a = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            rawImage0.color = c0;
            rawImage1.color = c0;
            rawImage2.color = c0;

            yield return 0;
            accumTime += Time.deltaTime;
            Debug.Log("isWOrking? " + accumTime);
        }
        c0.a = 1f;
        Debug.Log("isWOrking? " + accumTime);
    }

    private IEnumerator FadeOut()
    {
        accumTime = 0f;
        Color c0 = rawImage0.color;

        while(accumTime < fadeTime)
        {
            c0.a = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            rawImage0.color = c0;
            rawImage1.color = c0;
            rawImage2.color = c0;

            yield return 0;
            accumTime += Time.deltaTime;
        }
        c0.a = 0f;
        GotoMain();
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
