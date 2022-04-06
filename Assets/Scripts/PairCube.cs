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
        videoPlayer.loopPointReached += videoCompleted;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void slideLeft()
    {
        Debug.Log("slidingLeft");
        if (animator != null)
        {
            animator.SetTrigger("stretchleft");
        }
    }

    public void slideRight()
    {
        Debug.Log("slidingRight");
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
            Debug.Log("Slide Left");
            slideLeft();
            videoTexture.transform.localPosition = videoPlayerRightPosition;
        }
        else
        {
            Debug.Log("Slide Right");
            slideRight();
            videoTexture.transform.localPosition = videoPlayerLeftPosition;
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

    void videoCompleted(VideoPlayer vp)
    {
        isPlayingVideo = false;
        shiftToIdle();
        clearTextures(videoPlayer.targetTexture);
    }

    public void clearTextures(RenderTexture renderTexture)
    {
        Debug.Log("Clearing Textures");
        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;
        SceneController.Instance.clearTextures();
    }
}
