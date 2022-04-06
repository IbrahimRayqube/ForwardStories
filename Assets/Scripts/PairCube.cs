using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PairCube : MonoBehaviour
{
    private Animator animator;
    public VideoPlayer videoPlayer;
    public RawImage videoTexture;
    public Vector3 videoPlayerLeftPosition;
    public Vector3 videoPlayerRightPosition;
    public bool isPlayingVideo;
    public VideoData currentVideo;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingVideo)
        {
            Debug.Log(isPlayingVideo);
            if (!videoPlayer.isPlaying)
            {
                if(videoPlayer.isPlaying)
                isPlayingVideo = false;
                if (currentVideo != null)
                {
                    Debug.Log(currentVideo);
                    currentVideo.isPlaying = false;
                    SceneController.Instance.videoEnded();
                    currentVideo = null;
                }
            }
        }

    }

    public void slideLeft()
    {
        if (animator != null)
        {
            animator.SetTrigger("stretchleft");
        }
    }

    public void slideRight()
    {
        if (animator != null)
        {
            animator.SetTrigger("stretchright");
        }
    }

    public void shiftToIdle()
    {
        if (animator != null)
        {
            animator.SetTrigger("idle");
        }
    }

    public void playVideo()
    {
        int ran = Random.Range(0, 2);
        if (ran == 0)
        {
            slideLeft();
            videoPlayer.transform.position = videoPlayerLeftPosition;
        }
        else
        {
            slideRight();
            videoPlayer.transform.position = videoPlayerRightPosition;
        }
        StartCoroutine(startVideoWithDelay());
    }

    IEnumerator startVideoWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        //videoPlayer.enabled = true;
        videoPlayer.clip = currentVideo.video;
        currentVideo.isPlaying = true;
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        isPlayingVideo = true;
    }


}
