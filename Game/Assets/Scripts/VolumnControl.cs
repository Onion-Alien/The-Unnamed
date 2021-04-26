using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumnControl : MonoBehaviour
{
    private AudioSource audio;
    private float volume = 1f;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        audio.volume = volume;
    }

    public void Set(float vol)
    {
        volume = vol;
    }
}
