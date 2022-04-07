using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    public Material camMaterial;
    void Start()
    {
        GetComponent<Renderer>().material = camMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
