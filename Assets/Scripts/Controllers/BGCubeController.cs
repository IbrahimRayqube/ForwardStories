using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class BGCubeController : MonoBehaviour
{
    public PairCube[] pairCubeList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PairCube findCubePair()
    {
        bool foundPair = false;
        int ran = 0;
        int count = 0;
        while (!foundPair)
        {
            ran = Random.Range(0, pairCubeList.Length);
            if (!pairCubeList[ran].isPlayingVideo)
            {
                foundPair = true;
            }
            else
            {
                count++;
                if (count > 3)
                {
                    for (int i = 0; i < pairCubeList.Length; i++)
                    {
                        if (pairCubeList[i].isPlayingVideo)
                        {
                            foundPair = true;
                            ran = i;
                            break;
                        }
                    }
                }
            }
        }
        return pairCubeList[ran];
    }
}
