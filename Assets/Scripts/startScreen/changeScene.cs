using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changeScene : MonoBehaviour
{
    [SerializeField] Image controlsMenuUI;
    private bool isControlMenuActive = false;


    public void changeScenes()
    {
        Debug.Log("change scenes");
        SceneManager.LoadScene(1);
        controlsMenuUI.gameObject.SetActive(false);
    }

    public void testLoadFull()
    {
        SceneManager.LoadScene(2);
    }

    public void testLoadSmll()
    {
        SceneManager.LoadScene(4);
    }


    public void playFullscreen()
    {
        SceneManager.LoadScene(1);
        controlsMenuUI.gameObject.SetActive(false);
    }
    public void playSmallScreen()
    {
        SceneManager.LoadScene(3);
        controlsMenuUI.gameObject.SetActive(false);
    }


    public void displayControlsMenu()
    {
        if(isControlMenuActive)
        {
            controlsMenuUI.gameObject.SetActive(false);
            isControlMenuActive = false;
        }
        else
        {
            controlsMenuUI.gameObject.SetActive(true);
            isControlMenuActive = true;
        }
       
    }

    public void closeControlsMenu()
    {
        controlsMenuUI.gameObject.SetActive(false);
        isControlMenuActive = false;
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
