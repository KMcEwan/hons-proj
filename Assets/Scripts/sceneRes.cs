using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneRes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("screen res");
        Screen.SetResolution(768, 432, false, 60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
