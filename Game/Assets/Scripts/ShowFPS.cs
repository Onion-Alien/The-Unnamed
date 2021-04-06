using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public float timer, refresh, avgFramerate;
    public string display;
    public Text m_Text;

    public void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        m_Text.text = "FPS: " + avgFramerate.ToString();
    }
}
