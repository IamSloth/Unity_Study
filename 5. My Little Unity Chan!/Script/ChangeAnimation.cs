using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class ChangeAnimation : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        
    }

    public AnimationClip[] animations;
    Animator anim;

    public TMPro.TextMeshProUGUI poseText;
    public TMPro.TextMeshProUGUI faceText;

    public GameObject musicButton;
    private AudioSource bgm;

    private AudioSource unichanAudio;
    public AudioClip[] uniClips;

    public GameObject howtoCanvas;


    // Start is called before the first frame update
    static bool isFirstHow = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        unichanAudio = GetComponent<AudioSource>();
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();

        if(isFirstHow == false)
        {
            howtoCanvas.SetActive(true);
            isFirstHow = true;
        }
        
    }

    public static int faceNum = 0;
    public static int poseNum = 0;
    int poseNumMax = 31;

    public void NextFace()
    {
        faceNum++;
        if (faceNum >= animations.Length)
        {
            faceNum = 0;
        }
        anim.CrossFade(animations[faceNum].name, 0.2f);
        
        Debug.Log(faceNum);
        faceText.text = faceNum + 1 + "/17";
        UnitychanClipPlay();
    }

    public void PrevFace()
    {
        faceNum--;
        if(faceNum < 0)
        {
            faceNum = animations.Length - 1;
        }
        anim.CrossFade(animations[faceNum].name, 0.2f);
        
        Debug.Log(faceNum);
        faceText.text = faceNum + 1 + "/17";
        UnitychanClipPlay();
    }

    public void NextPoseOn()
    {
        
        if(anim.IsInTransition(0) == false)
        {
            anim.SetTrigger("Next");
            poseNum++;
            
        }
        
        if(poseNum >= poseNumMax)
        {
            poseNum = 0;
        }
        
        Debug.Log(poseNum);
        poseText.text = poseNum + 1 + "/31";
        UnitychanClipPlay();
    }


    public void PrevPoseOn()
    {
        
        if(anim.IsInTransition(0) == false)
        {
            anim.SetTrigger("Back");
            poseNum--;
            
        }        
        if(poseNum < 0)
        {
            poseNum = poseNumMax-1;
        }
        Debug.Log(poseNum);
        poseText.text = poseNum + 1 + "/31";
        UnitychanClipPlay();
    }

    public void ChangeCamera()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
            faceNum = 0;
            poseNum = 0;
            isHowToPlaying = false;
        }

        else
        {
            SceneManager.LoadScene(1);
            faceNum = 0;
            poseNum = 0;
            isHowToPlaying = false;
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 3);
        transform.eulerAngles = new Vector3(0, 180, 0);
        transform.localScale = Vector3.one;
    }

    static bool isMusicPlaying = true;

    public void MusicOnOff()
    {
        if(isMusicPlaying == false)
        {
            isMusicPlaying = true;
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicButton.transform.GetChild(1).gameObject.SetActive(false);
            Debug.Log("isWorking?" + isMusicPlaying);
            bgm.Play();
        }

        else
        {
            isMusicPlaying = false;
            musicButton.transform.GetChild(0).gameObject.SetActive(false);
            musicButton.transform.GetChild(1).gameObject.SetActive(true);
            Debug.Log("isWorking?" + isMusicPlaying);
            bgm.Stop();
        }
    }

    
    static bool isHowToPlaying = true;
    
    public void HowToPlayOnOff()
    {
        if(isHowToPlaying == false)
        {
            isHowToPlaying = true;
            howtoCanvas.SetActive(true);
        }

        else
        {
            isHowToPlaying = false;
            howtoCanvas.SetActive(false);
        }
    }

    public void UnitychanClipPlay()
    {
        int playTrack = Random.Range(0, uniClips.Length);
        unichanAudio.clip = uniClips[playTrack];
        unichanAudio.Play();
    }



    void Start()
    {
       if(isMusicPlaying == false)
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(false);
            musicButton.transform.GetChild(1).gameObject.SetActive(true);
        }

       else
        {
            musicButton.transform.GetChild(0).gameObject.SetActive(true);
            musicButton.transform.GetChild(1).gameObject.SetActive(false);
        }
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
