using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public float volume; // slider
    public AudioMixer mixer; // audio source
    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }
}
