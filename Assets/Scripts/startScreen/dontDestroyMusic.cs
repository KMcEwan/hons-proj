using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyMusic : MonoBehaviour
{
    [SerializeField] private GameObject[] musicSourceObjects;
    private void Awake()
    {if(musicSourceObjects.Length < 1)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
        
    }
}
