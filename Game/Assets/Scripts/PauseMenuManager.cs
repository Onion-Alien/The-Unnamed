using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    public bool optionOpen = false;
    public GameObject selectionImage;
    public float highlightMoveAmount = 0.88f;
    public int state;
    public GameObject exitMenu;

    void Start()
    {
        state = 0;
        pauseMenu.SetActive(false);
    }

    //Controls for pausing the game, checks if options menu is current open, if so does not unpause

    //Pauses the game, freezes time, sets isPaused to true for other methods
    public void PauseGame()
    {
        if (!isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            exitMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    //Uses the immortal object OptionController to access the options menu
    public void OptionMenu()
    {
        OptionController.instance.enableCanvas(pauseMenu);
    }

    //Exit game
    public void QuitGame()
    {
        //Note only works on built project, not with editor
        SaveManager.instance.Save();
        Application.Quit();
    }

    void highLightMove(int moveTo)
    {
        if (state == 0)
        {
            if (moveTo == 1)
            {
                selectionImage.transform.Translate(new Vector3(0, -highlightMoveAmount, 0));
            }
            else if (moveTo == 2)
            {
                selectionImage.transform.Translate(new Vector3(0, -(highlightMoveAmount * 2), 0));
            }
            else if (moveTo == 3)
            {
                selectionImage.transform.Translate(new Vector3(0, -(highlightMoveAmount * 3), 0));
            }
            state = moveTo;
        }
        if (state == 1)
        {
            if (moveTo == 0)
            {
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount, 0));
            }
            else if (moveTo == 2)
            {
                selectionImage.transform.Translate(new Vector3(0, -highlightMoveAmount, 0));
            }
            else if (moveTo == 3)
            {
                selectionImage.transform.Translate(new Vector3(0, -highlightMoveAmount * 2, 0));
            }
            state = moveTo;
        }
        if (state == 2)
        {
            if (moveTo == 0)
            {
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount * 2, 0));
            }
            else if (moveTo == 1)
            {
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount, 0));
            }
            else if (moveTo == 3)
            {
                selectionImage.transform.Translate(new Vector3(0, -highlightMoveAmount, 0));
            }
            state = moveTo;
        }
    }

    public void moveResume()
    {
        highLightMove(0);
    }

    public void moveOptions()
    {
        highLightMove(1);
    }

    public void moveQuit()
    {
        highLightMove(2);
    }
}
