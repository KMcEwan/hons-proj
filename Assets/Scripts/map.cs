using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour
{
    [SerializeField] Image mapUI;
    private bool mapIsActive = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(mapIsActive)
            {
                deactivateMap();
            }
            else
            {
                activateMap();
            }
        }
    }

    void deactivateMap()
    {
        mapUI.gameObject.SetActive(false);
        mapIsActive = false;
    }

    void activateMap()
    {
        mapUI.gameObject.SetActive(true);
        mapIsActive = true;
    }
}
