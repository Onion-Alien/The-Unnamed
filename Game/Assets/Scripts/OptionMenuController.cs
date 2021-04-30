using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    //Resolution variables
    private int[] dimensions = new int[2];
    private int[] widths = new int[6] { 3840, 2560, 1920, 1600, 1366, 1280};
    private int[] heights = new int[6] { 2160, 1440, 1080, 900, 768, 720};
    private int resolution;
    private bool fullscreenActive, fsTogglePressed, resoSelected = false;
    public Toggle fullscreenToggle;

    void Start()
    {
        resolution = -2;
        if (PlayerPrefs.GetInt("fullscreen") == 1)
        {
            fullscreenActive = true;
        }
        else if (PlayerPrefs.GetInt("fullscreen") == 2)
        {
            fullscreenActive = false;
            fullscreenToggle.isOn = !fullscreenToggle.isOn;
        }
        else
        {
            fullscreenActive = true;
            PlayerPrefs.SetInt("fullscreen", 1);

        }
        fsTogglePressed = false;
    }

    public void getResolution(int val)
    {
        resolution = (val - 1);
        if (resolution >= 0)
        {
            resoSelected = true;
            dimensions[0] = heights[resolution];
            dimensions[1] = widths[resolution];
        }
    }

    public void setResolution(int val)
    {
        if (fsTogglePressed || resoSelected)
        {
            if (resolution >= 0)
            {
                if (fsTogglePressed)
                {
                    Screen.SetResolution(dimensions[1], dimensions[0], !Screen.fullScreen);
                    togglefullscreenActive();
                    toggleFullscreen();
                }
                else
                {
                    Screen.SetResolution(dimensions[1], dimensions[0], Screen.fullScreen);
                }
                resoSelected = false;
            }
            else
            {
                if (fullscreenActive && fsTogglePressed)
                {
                    Screen.SetResolution(1920, 1080, !Screen.fullScreen);
                    togglefullscreenActive();
                }
                else if (!fullscreenActive && fsTogglePressed)
                {
                    Screen.SetResolution(1920, 1080, !Screen.fullScreen);
                    togglefullscreenActive();
                }
                toggleFullscreen();
            }
        }
        else
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
    }

    public void toggleFullscreen()
    {
        if (!fsTogglePressed)
        {
            fsTogglePressed = true;
        }
        else
        {
            fsTogglePressed = false;
        }
    }

    public void togglefullscreenActive()
    {
        if (!fullscreenActive)
        {
            fullscreenActive = true;
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            fullscreenActive = false;
            PlayerPrefs.SetInt("fullscreen", 2);
        }
    }

    public void applySettings()
    {
        setResolution(resolution);

        //rest of settings
    }
}
