using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;     //Stores the name of sound effect
    public AudioClip clip;  //Store the actual sound effect clip
    [Range(0f, 1f)]         //limits the range in the Unity editor
    public float volume;    //Stores volume
    [Range(0.1f, 3f)]       //Limits the Range again
    public float pitch;     // sets the picth for sound effect
    [HideInInspector]       //Hide this variable from the Editor
    public AudioSource source;// the source that will play the sound
    public bool loop = false;// gives option to loop sound 
}