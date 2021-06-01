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
    public Text rp;
    public Text sp;
    public int redF1 = 0;
    public int redF2 = 0;
    public int GreenF1 = 0;
    public int GreenF2 = 0;
    public int greenPotion = 0;
    public int redPotion = 0;

    public static FragmentCount fc;

    private void Awake()
    {
        fc = this;
        
    }

    void Start()
    {
        rf1.text = "Red Fragment 1:  " + redF1.ToString();
        rf2.text = "Red Fragment 2:  " + redF2.ToString();
        gf1.text = "Green Fragment 1:  " + GreenF1.ToString();
        gf2.text = "Green Fragment 2:  " + GreenF2.ToString();
        rp.text = ": " + redPotion.ToString();
        sp.text = ": " + greenPotion.ToString();
    }

    // Update is called once per frame
    public void addFragment(int drop,GameObject item)
    {
        if(item.name.Contains("RedFragment1"))
        {
            redF1 += drop;
            rf1.text = "Red Fragment 1:  " + redF1.ToString();
        }
        else if(item.name.Contains("RedFragment2"))
        {
            redF2 += drop;
            rf2.text = "Red Fragment 2:  " + redF2.ToString();
        }
        else if(item.name.Contains("GreenFragment1"))
        {
            GreenF1 += drop;
            gf1.text = "Green Fragment 1:  " + GreenF1.ToString();
        }
        else if (item.name.Contains("GreenFragment2"))
        {
            GreenF2 += drop;
            gf2.text = "Green Fragment 2:  " + GreenF2.ToString();
        }


    }


    public void updateText()
    {
        rf1.text = "Red Fragment 1:  " + redF1.ToString();
        rf2.text = "Red Fragment 2:  " + redF2.ToString();
        gf1.text = "Green Fragment 1:  " + GreenF1.ToString();
        gf2.text = "Green Fragment 2:  " + GreenF2.ToString();
        rp.text = ": " + redPotion.ToString();
        sp.text = ": " + greenPotion.ToString();
    }


}
