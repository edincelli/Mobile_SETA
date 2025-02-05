using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance { get; private set; }

    [SerializeField] private AudioSource audioCall;
    [SerializeField] private AudioSource audioNotification;
    [SerializeField] private AudioSource audioVibrations;

    public static void PlayCallAudio()
    {
        Instance.audioCall.Play();
    }

    public static void StopCallAudio()
    {
        Instance.audioCall.Stop();
    }

    public static void PlayNotificationAudioOnce()
    {
        Instance.audioNotification.Play();
    }

    public static void PlayVibrationsAudio()
    {
        Instance.audioVibrations.Play();
    }

    public static void StopVibrationsAudio()
    {
        Instance.audioVibrations.Stop();
    }


    private void Awake()
    {
        Instance = this;   
    }
}
