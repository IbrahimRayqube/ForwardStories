using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;


[Serializable]
public class VideoData
{
    public VideoClip video;
    public bool isPlaying = false;
}
public class SceneController : Singleton<SceneController>
{
    public VideoData[] allVideos;
    public BGCubeController cubeController;
    private PairCube tempCubePair;
    public int currentPlayingVideosCount = 0;
    public int currentPendingRequests = 0;
    public RenderTexture[] allRenderTextures;
    public RenderTexture defaultTexture;
    public WebCamTexture webCamTexture;
    // Start is called before the first frame update
    void Start()
    {
        clearTextures();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            videoRequestRecieved();
        }
    }

    public void clearTextures()
    {
        for (int i = 0; i < allRenderTextures.Length; i++)
        {
            RenderTexture rt = RenderTexture.active;
            RenderTexture.active = allRenderTextures[i];
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = rt;
        }
    }

    


    public VideoData findVideoToPlay()
    {
        //bool foundVideo = false;
        //int temp = 0;
        //int count = 0;
        //while (!foundVideo)
        //{

        //    temp = UnityEngine.Random.Range(0, allVideos.Length);
        //    if (!allVideos[temp].isPlaying)
        //    {
        //        foundVideo = true;
        //    }
        //    else
        //    {
        //        count++;
        //        if (count > 4)
        //        {
        //            for (int i = 0; i < allVideos.Length; i++)
        //            {
        //                if (!allVideos[i].isPlaying)
        //                {
        //                    foundVideo = true;
        //                    temp = i;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
        //return allVideos[temp];
        int ran = UnityEngine.Random.Range(0, allVideos.Length);
        return allVideos[ran];
    }

    public void videoRequestRecieved()
    {
        if (currentPlayingVideosCount < 6)
        {
            Debug.Log("1");
            currentPlayingVideosCount++;
            Debug.Log("2");
            tempCubePair = cubeController.findCubePair();
            Debug.Log("3");
            tempCubePair.currentVideo = findVideoToPlay();
            Debug.Log("4");
            tempCubePair.playVideo();
        }
        else
        {
            currentPendingRequests++;
        }
    }

    public void videoEnded()
    {
        currentPlayingVideosCount--;
        if (currentPendingRequests > 0)
        {
            currentPendingRequests--;
            videoRequestRecieved();
        }
    }
}
