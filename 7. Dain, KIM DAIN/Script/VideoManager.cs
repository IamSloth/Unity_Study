using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{

    public VideoPlayer player;
    public Image progress;
    public GameManager manager;
    public GameObject loding;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(VideoStart());
    }

    IEnumerator VideoStart()
    {
        player.enabled = true;
        player.SetDirectAudioMute(0, true);
        yield return new WaitForSeconds(5f);
        player.SetDirectAudioMute(0, false);
        player.enabled = false;
        
    }
  
    void Update()
    {
        if (player.frameCount > 0)
            progress.fillAmount = (float)player.frame / (float)player.frameCount;
    }

    public void VideoPlayerPause()
    {
        if (player != null)
        {
            player.Pause();
        }
            
    }
    public void VideoPlayerPlay()
    {
        if (player != null)
        {
            if (manager.currentVideo != null)
            {
                manager.currentVideo.Pause();
            }
            player.enabled = true;
            player.Play();
            manager.currentVideo = player;

        }
    }
  
}

