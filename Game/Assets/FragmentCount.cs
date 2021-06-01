using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentCount : MonoBehaviour
{
    public Text rf1;
    public Text rf2;
    public Text gf1;
    public Text gf2;
    public int redF1 = 0;
    public int redF2 = 0;
    public int greenF1 = 0;
    public int greenF2 = 0;

    public static FragmentCount fc;

    private void Awake()
    {
        fc = this;
        
    }

    void Start()
    {
        rf1.text = "Red Fragment 1:  " + redF1.ToString();
        rf2.text = "Red Fragment 2:  " + redF2.ToString();
        gf1.text = "Green Fragment 1:  " + greenF1.ToString();
        gf2.text = "Green Fragment 2:  " + greenF2.ToString();
    }

    // Update is called once per frame
    public static void addFragment(int drop,GameObject item)
    {
        if(item.name.Contains("RedFragment1"))
        {
            SaveManager.instance.RedF1++;
        }
        else if(item.name.Contains("RedFragment2"))
        {
            SaveManager.instance.RedF2++;
        }
        else if(item.name.Contains("GreenFragment1"))
        {
            SaveManager.instance.GreenF1++;
        }
        else if (item.name.Contains("GreenFragment2"))
        {
            SaveManager.instance.GreenF2++;
        }

    }

  


}
