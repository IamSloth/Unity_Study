using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SNSShare : MonoBehaviour
{
    public GameObject canvas;
    public AudioClip clip;
    private AudioSource audioSource;
    private Image image;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();
    }


    public void Share()
    {
         audioSource.PlayOneShot(clip);
         canvas.SetActive(false);
         image.enabled = false;
         Invoke("CoroutineShare", 0.2f);
         Invoke("MyCanvasOn", 0.5f);

    }

    public void MyCanvasOn()
    {
        canvas.SetActive(true);
        image.enabled = true;
    }

    public void CoroutineShare()
    {
        StartCoroutine(TakeScreenShotAndShare());
    }

    // Start is called before the first frame update
    private IEnumerator TakeScreenShotAndShare()
    {
        
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = System.IO.Path.Combine(Application.temporaryCachePath, "share.png");
        System.IO.File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("").SetText("").SetUrl("")
            .SetCallback((res, target) => Debug.Log($"result {res}, target app: {target}"))
            .Share();

    }
}
