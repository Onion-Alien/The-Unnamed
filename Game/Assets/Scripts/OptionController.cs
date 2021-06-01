using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    //Variables for resolution
    private int[] dimensions = new int[2];
    private int[] actualWidths = new int[7], defWidths = new int[7] { 3840, 2560, 1920, 1600, 1366, 1280, 0};
    private int[] actualHeights = new int[7], defHeights = new int[7] { 2160, 1440, 1080, 900, 768, 720, 0};
    
    private int resolution = -2;
    private int dropdownSize = 1;
    
    //Fullscreen variables
    private bool fullscreenActive, fsTogglePressed, resoSelected = false;
   
    public Toggle fullscreenToggle;

    //unity object stuff
    public Dropdown dropdown;
   
    public GameObject optionCanvas;
    public GameObject accessCanvas;

    //List to hold the resolutions
    List<string> m_DropOptions;
    public static OptionController instance { get; private set; }

    //Creates an instance of this object that doesn't get destroyed upon changing scenes.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    //Sets up base variables and checks if user already has a saved fullscreen preference
    void Start()
    {
        optionCanvas.SetActive(false);
        buildResArray();
        updateDropDown();

        if (PlayerPrefs.GetInt("fullscreen") == 1)
        {
            fullscreenActive = true;
        }
        else if (PlayerPrefs.GetInt("fullscreen") == 2)
        {
            fullscreenActive = false;
        }
        else
        {
            fullscreenActive = true;
            PlayerPrefs.SetInt("fullscreen", 1);

        }

        if (fullscreenToggle.isOn)
        {
            if (fullscreenActive == false)
            {
                fullscreenToggle.isOn = !fullscreenToggle.isOn;
            }
        }
        fsTogglePressed = false;
    }

    //Gets dropdown selection index, assigns appropriate values to dimensions array
    public void getResolution()
    {
        Debug.Log("shit pressed ");
        resolution = dropdown.value;
        resoSelected = true;
        dimensions[0] = actualHeights[resolution];
        dimensions[1] = actualWidths[resolution];
    }

    //Updates the games resolution and switches between fullscreen and windowed
    public void setResolution(int val)
    {
        if (fsTogglePressed || resoSelected)
        {
            if(fsTogglePressed && resoSelected)
            {
                Screen.SetResolution(dimensions[1], dimensions[0], !Screen.fullScreen);
                resoSelected = false;
                togglefsBool();
                toggleFullscreen();
                saveUserChoice();
            }
            else if (fsTogglePressed)
            {
                Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
                togglefsBool();
                toggleFullscreen();
                saveUserChoice();
            }
            else
            {
                Screen.SetResolution(dimensions[1], dimensions[0], Screen.fullScreen);
                resoSelected = false;
            }
        }
    }

    public void togglefsBool()
    {
        if (!fullscreenActive)
        {
            fullscreenActive = true;
        }
        else
        {
            fullscreenActive = false;
        }
    }

    public void toggleFullscreen()
    {
        if (!fsTogglePressed)
        {
            fullscreenToggle.Select();
            fsTogglePressed = true;
        }
        else
        {
            fullscreenToggle.Select();
            fsTogglePressed = false;
        }
    }

    public void saveUserChoice()
    {
        if (fullscreenActive)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 2);
        }
    }

    //Updates the dropdown menu with the current list
    public void updateDropDown()
    {
        dropdown.ClearOptions();
        m_DropOptions = new List<string>();
        m_DropOptions.Add(Screen.width.ToString() + "x" + Screen.height.ToString());
        
        for (int i = 1; i <= dropdownSize - 1; i++)
        {
            m_DropOptions.Add(actualWidths[i].ToString() + "x" + actualHeights[i].ToString());
        }
        dropdown.AddOptions(m_DropOptions);
    }

    //Creates a list for resolutions in relation to; the users current resolution and the list of support resolutions
    public void buildResArray()
    {
        actualWidths[0] = Screen.width;
        actualHeights[0] = Screen.height;

        for (int i = 0; i <= 5; i++)
        {
            if (defWidths[i] != Screen.width && defHeights[i] != Screen.height)
            {
                actualWidths[dropdownSize] = defWidths[i];
                actualHeights[dropdownSize] = defHeights[i];

                dropdownSize++;
            }
        }
    }

    //Switches the settings back to default status in the case that the user changes settings then doesn't apply them and uses the back/return button
    public void switchToDefault()
    {
        if (resoSelected)
        {
            dropdownSize = 1;
            dropdown.ClearOptions();
            buildResArray();
            updateDropDown();                                   
        }
        if (fsTogglePressed)
        {
            fullscreenToggle.isOn = !fullscreenToggle.isOn;
        }
    }

    public void applySettings()
    {
        setResolution(resolution);

        //rest of settings
    }

    public void enableCanvas(GameObject accessor)
    {
        accessCanvas = accessor;
        accessCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void disableCanvas()
    {
        optionCanvas.SetActive(false);
        accessCanvas.SetActive(true);
    }
}
