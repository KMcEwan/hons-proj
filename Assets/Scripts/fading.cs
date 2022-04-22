using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class fading : MonoBehaviour
{


    [SerializeField] Animator gameWonAniationController;
    [SerializeField] Animator gameLostAnimationController;

    void Start()
    {
        //animationController = GetComponent<Animator>();
    }

    public void survivedFade()
    {
        gameWonAniationController.SetBool("survived", true);
    }

    public void diedFade()
    {
        Debug.Log("died fade");
        gameLostAnimationController.SetBool("died", true);
    }
}
