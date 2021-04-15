using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    //Resolution variables
    public int[] dimensions = new int[2];
    private int[] widths = new int[6] { 3840, 2560, 1920, 1600, 1366, 1280};
    private int[] heights = new int[6] { 2160, 1440, 1080, 900, 768, 720};
    public int resolution;

    void Start()
    {
        resolution = -1;
    }

    void Update()
    {

    }

    public void getResolution(int val)
    {
        resolution = (val - 1);
        if (resolution >= 0)
        {
            dimensions[0] = heights[resolution];
            dimensions[1] = widths[resolution];
        }
    }

    public void setResolution(int val)
    {
        Screen.SetResolution(dimensions[1], dimensions[0], Screen.fullScreen);
    }

    public void applySettings()
    {
        if (resolution != -1)
        {
            setResolution(resolution);
        }

        //rest of settings
    }
}
