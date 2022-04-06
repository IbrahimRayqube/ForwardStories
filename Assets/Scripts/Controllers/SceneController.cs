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
        Debug.Log("Clearing Textures");
        for (int i = 0; i < allRenderTextures.Length; i++)
        {
            RenderTexture rt = UnityEngine.RenderTexture.active;
            UnityEngine.RenderTexture.active = allRenderTextures[i];
            GL.Clear(true, true, Color.clear);
            UnityEngine.RenderTexture.active = rt;
        }
    }

    public VideoData findVideoToPlay()
    {
        bool foundVideo = false;
        int temp = 0;
        int count = 0;
        while (!foundVideo)
        {

            temp = UnityEngine.Random.Range(0, allVideos.Length);
            if (!allVideos[temp].isPlaying)
            {
                foundVideo = true;
            }
            else
            {
                count++;
                if (count > 4)
                {
                    for (int i = 0; i < allVideos.Length; i++)
                    {
                        if (!allVideos[i].isPlaying)
                        {
                            foundVideo = true;
                            temp = i;
                            break;
                        }
                    }
                }
            }
        }
        return allVideos[temp];
    }

    public void videoRequestRecieved()
    {
        if (currentPlayingVideosCount < 6)
        {
            currentPlayingVideosCount++;
            tempCubePair = cubeController.findCubePair();
            tempCubePair.currentVideo = findVideoToPlay();
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
