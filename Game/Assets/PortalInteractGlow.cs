using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteractGlow : MonoBehaviour
{
    float duration = 1.0f;

    public Color color1;
    public Color color2;
    private SpriteRenderer sr;
    public bool isEnabled;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            sr.color = Color.Lerp(color1, color2, t);
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
