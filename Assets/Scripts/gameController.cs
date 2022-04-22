using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{

    internal bool isPaused = false;



    // ui for pause menu
    [SerializeField] Image pausedMenuUI;
    [SerializeField] Image optionsMenuUI;

    public GameObject musicSourceObjects;

    // TO STOP BEING ABLE TO FIRE WHEN PAUSED
    [SerializeField] gunsController gunControllerScipt;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Awake()
    {
        musicSourceObjects = GameObject.FindGameObjectWithTag("mainMenuAudio");
        Destroy(musicSourceObjects);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    private void resumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausedMenuUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        gunControllerScipt.enabled = true;
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pausedMenuUI.gameObject.SetActive(true);
        gunControllerScipt.enabled = false;

    }


    public void quitGame()
    {
        Application.Quit();
    }

    public void showOptions()
    {
        optionsMenuUI.gameObject.SetActive(true);
    }

    public void returnToMain()
    {
        SceneManager.LoadScene(0);
    }


    public void restartGame()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




}
