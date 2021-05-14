using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{

    public Text returnText;
    public float alpha;
    public bool switcher = true;
    private Color colorTemp;

    private void Start()
    {
        alpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(switcher)
        {
            alpha += 0.005f;
            if (alpha > 1.0f)
            {
                switcher = false;
            }
        }
        else if(!switcher)
        {
            alpha -= 0.005f;
            if (alpha < 0.0f)
            {
                switcher = true;
            }
        }
        colorTemp = returnText.color;
        colorTemp.a = alpha;

        returnText.color = colorTemp;
    }
}
