using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicTest : MonoBehaviour
{
    private WebCamTexture _webcamTexture;
    private Renderer _renderer;
    // Assign the Material you are using for the web cam feed
    [SerializeField] private Material webCamTex;





    void Start()
    {

        webCamTex = Resources.Load("webCamTex", typeof(Material)) as Material;

        // Grabbing all web cam devices
        WebCamDevice[] devices = WebCamTexture.devices;

        // I just use the first one, use which ever one you need 
        string camName = devices[0].name;

        // set the Texture from the cam feed
        WebCamTexture camFeed = new WebCamTexture(camName);

        // Assign the materials texture to the WebCamTexture you made,
        // this applies it to all objects using this Material
        webCamTex.mainTexture = camFeed;

        // Then start the texture
        camFeed.Play();

        //gameObject.GetComponent<Renderer>().material = webCamTex;

    }
}
