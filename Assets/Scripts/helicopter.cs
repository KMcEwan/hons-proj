using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helicopter : MonoBehaviour
{
    [SerializeField] GameObject helicopterObj;

    [SerializeField] fading fadingScript;



    [SerializeField] Animator helicopterAnim;


    //ENABLE SURVIVED SCRIPT
    [SerializeField] GameObject survivedUI;


 

    public void activateHelicopter()
    {
        helicopterObj.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            Debug.Log("player collision");
            //
            survivedUI.gameObject.SetActive(true);
            helicopterAnim.SetBool("reachedHelicopter", true);
            other.gameObject.SetActive(false);
        }
    }
    

    public void gameCompletedOverlay()
    {
        fadingScript.survivedFade();
    }

    public void gameOverOverlay()
    {
        fadingScript.diedFade();
    }

    public void helicopterLeave()
    {
        helicopterAnim.SetBool("leaveTimeReached", true);
    }
}
