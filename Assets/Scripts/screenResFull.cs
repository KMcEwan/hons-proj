using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenResFull : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("screen res");
        Screen.SetResolution(1920, 1080, true, 60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
