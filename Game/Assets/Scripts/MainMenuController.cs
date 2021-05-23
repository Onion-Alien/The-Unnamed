using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject characterCreation;
    public GameObject selectionImage;
    public GameObject mainMenu;
    private bool hoverTrue = false;
    public float highlightMoveAmount = 0.93f;
    public int state;
    public Button newGame, options, exit;

    void Start()
    {
        state = 0;
        characterCreation.SetActive(false);
    }

    //Update used to scan for any input from keyboard in terms of menu control

    void Update()
    {
        //if (!hoverTrue)
        //{
        //    if (Input.GetKeyDown(KeyCode.S) && state < 3 || Input.GetKeyDown(KeyCode.DownArrow) && state < 3)
        //    {
        //        selectionImage.transform.Translate(new Vector3(0, -highlightMoveAmount, 0));
        //        state++;
        //    }
        //    else if (Input.GetKeyDown(KeyCode.W) && state > 0 || Input.GetKeyDown(KeyCode.UpArrow) && state > 0)
        //    {
        //        selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount, 0));
        //        state--;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    itemSelected(state);
        //}
    }

    public void updateHoverTrue()
    {
        hoverTrue = !hoverTrue;
    }

    //This method is called when the user selects a choice in the main menu, has 4 cases for 4 buttons

    public void itemSelected(int n)
    {
            if (n == 0)
            {
                mainMenu.SetActive(false);
                characterCreation.SetActive(true);
            }
            else if (n == 1)
            {
                if (SaveManager.instance.checkSaveExist())
                {
                //   newGame. = false;options.interactable = false;exit.interactable = false;
              //  newGame.enabled = false;
                }
            }
            else if (n == 2)
            {
                OptionController.instance.enableCanvas(mainMenu);
            }
            else if (n == 3)
            {
                Application.Quit();
            }
        
    }

    //Changes the location of the menu highlight bar thing, has 3 cases for each button..
    void highLightMove(int moveTo)
    {
        if(state == 0)
        {
            if(moveTo == 1)
            {
                selectionImage.transform.Translate(new Vector3(0, -highlightMoveAmount, 0));
            }
            else if(moveTo == 2)
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
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount  * 2, 0));
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
        if (state == 3)
        {
            if (moveTo == 0)
            {
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount * 3, 0));
            }
            else if (moveTo == 1)
            {
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount * 2, 0));
            }
            else if (moveTo == 2)
            {
                selectionImage.transform.Translate(new Vector3(0, highlightMoveAmount, 0));
            }
            state = moveTo;
        }
    }

    //Below methods either change the state to the selected button, or when the user presses e or clicks a button it uses the select methods

    public void moveStart()
    {
        highLightMove(0);
    }

    public void moveLoad()
    {
        highLightMove(1);
    }

    public void moveOption()
    {
        highLightMove(2);
    }

    public void moveQuit()
    {
        highLightMove(3);
    }

    public void selectStart()
    {
        itemSelected(0);
    }

    public void selectLoad()
    {
        itemSelected(1);
    }

    public void selectOption()
    {
        itemSelected(2);
    }

    public void selectQuit()
    {
        itemSelected(3);
    }

    public void closeOptionMenu()
    {
        mainMenu.SetActive(true);
    }
}
