using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveName : MonoBehaviour
{
    public Text nametag;

    private void Start()
    {
        getName();
    }

    //Get name string from SaveManager object
    public void getName()
    {
        if (SaveManager.instance.getName() != "temp")
        {
            nametag.text = SaveManager.instance.getName();
        }
    }
}
