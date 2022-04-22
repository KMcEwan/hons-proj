using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameStartPlay : MonoBehaviour
{
    [SerializeField] Image noteUI;
    void Awake()
    {
        Time.timeScale = 0f;
    }


    public void startGamePlay()
    {
        Time.timeScale = 1f;
        noteUI.gameObject.SetActive(true);
    }

}
