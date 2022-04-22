using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class volumeController : MonoBehaviour
{

    public AudioMixer backgroundAudio;
    public AudioMixer soundFX;

    public void setBackgroundVolume(float sliderValue)
    {
        backgroundAudio.SetFloat("backgroundVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void setFXvolume(float sliderValue)
    {
        soundFX.SetFloat("soundFXVolume", Mathf.Log10(sliderValue) * 20);

    }

}
