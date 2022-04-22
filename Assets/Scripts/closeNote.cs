using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeNote : MonoBehaviour
{

    [SerializeField] Image noteUI;
    [SerializeField] GameObject fadeInCanvas;


    public void deactivateNote()
    {
        noteUI.gameObject.SetActive(false);
        fadeInCanvas.SetActive(false);
    }


}
