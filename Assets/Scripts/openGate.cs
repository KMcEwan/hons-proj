using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openGate : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void openGateAudio()
    {
        audioSource.Play();
    }

    public void closeGateAudio()
    {
        audioSource.Stop();
    }

}
