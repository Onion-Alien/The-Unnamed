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
    public int GreenF1 = 0;
    public int GreenF2 = 0;

    public static FragmentCount fc;

    private void Awake()
    {
        fc = this;
    }

    void Start()
    {
        rf1.text = "Red Fragment 1:" + redF1.ToString();
        rf2.text = "Red Fragment 2:" + redF2.ToString();
        gf1.text = "Green Fragment 1:" + GreenF1.ToString();
        gf2.text = "Green Fragment 2:" + GreenF2.ToString();
    }

    // Update is called once per frame
    public void addRedF1(int drop)
    {
        redF1 += drop;
        rf1.text = "Red Fragment 1:  " + redF1.ToString();
    }

    public void addRedF2(int drop)
    {
        redF2 += drop;
        rf2.text = "Red Fragment 2:  " + redF2.ToString();
    }

    public void addGreenF1(int drop)
    {
        GreenF1 += drop;
        gf1.text = "Green Fragment 1:  " + GreenF1.ToString();
    }

    public void addGreenF2(int drop)
    {
        GreenF2 += drop;
        gf2.text = "Green Fragment 2:  " + GreenF2.ToString();
    }


}
