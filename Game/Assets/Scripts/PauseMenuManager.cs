using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;
    public static bool isPaused;
    private static bool optionOpen;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !optionOpen)
            {
                ResumeGame();
            }
            else if(!isPaused)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OptionMenu()
    {
        optionMenu.SetActive(true);
        pauseMenu.SetActive(false);
        optionOpen = true;
    }

    public void CloseOptionMenu()
    {
        optionMenu.SetActive(false);
        pauseMenu.SetActive(true);
        optionOpen = false;
    }

    public void QuitGame()
    {
        //Note only works on built project, not with editor
        Application.Quit();
    }
}
