using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// GAME OBJECT USING THIS SCRIPT HAS TO BE TAGGED "mainMenuAudio" TO STOP THE AUDIO PLAYING IN THE MAIN GAME

public class audioController : MonoBehaviour
{

    public AudioMixer musicMixer;


    public void setMenuMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("mainMenuVolume", Mathf.Log10(sliderValue) * 20);
    }


}
