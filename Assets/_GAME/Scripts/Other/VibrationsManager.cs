using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationsManager : MonoBehaviour
{
    public static VibrationsManager Instance { get; private set; }

    [SerializeField] private Transform phoneElement;
    [SerializeField] private float vibFrequency = 3f;
    [SerializeField] private float vibAngle = 1f;

    [Header("Call Vibration")]
    [SerializeField] private float vibCallSingleLength = 1f;
    [SerializeField] private float vibCallTotalLength = 3f;

    [Header("Notification Vibration")]
    [SerializeField] private float vibNotificationSingleLength = 1f;
    [SerializeField] private float vibNotificationTotalLength = 1f;

    [ShowInInspector, ShowIf("@this.isVibratingTest"), PropertySpace]
    private float selectedVibSingleLength = 0;
    private float remainingVibrationTime = 0;
    private bool isVibratingCall = false;
    private bool isVibratingNotification = false;
    private bool isVibratingTest = false;

    private bool isVibrationActive = false;

    private bool ShowStartVibrationButtons
    {
        get
        {
            if(Application.isPlaying == false)
                return false;

            return remainingVibrationTime <= 0;
        }
    }
    private bool ShowStopVibrationButtons
    {
        get
        {
            if (Application.isPlaying == false)
                return false;

            return remainingVibrationTime > 0;
        }
    }

    [Button("Stop Vibrations"), ShowIf("@ShowStopVibrationButtons")]
    public void StopVibrations()
    {
        isVibratingCall = false;
        isVibratingNotification = false;
        isVibratingTest = false;
        remainingVibrationTime = 0;

        phoneElement.localRotation = Quaternion.identity;
    }

    [Button("Start Call Vibrations"), ShowIf("ShowStartVibrationButtons"), PropertySpace(40)]
    public void StartCallVibrations()
    {
        isVibratingCall = true;
        remainingVibrationTime = vibCallTotalLength;
        selectedVibSingleLength = vibCallSingleLength;
    }

    [Button("Start Notification Vibrations"), ShowIf("ShowStartVibrationButtons")]
    public void StartNotificationVibrations()
    {
        isVibratingNotification = true;
        remainingVibrationTime = vibNotificationTotalLength;
        selectedVibSingleLength = vibNotificationSingleLength;
    }

    [Button("Start Test Vibrations"), ShowIf("ShowStartVibrationButtons")]
    private void StartTestVibrations()
    {
        isVibratingTest = true;
        remainingVibrationTime = 100;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateVibrations();
    }

    private void UpdateVibrations()
    {
        if (remainingVibrationTime <= 0)
        {
            ToggleVibrationSound(false);
            return;
        }

        remainingVibrationTime -= Time.deltaTime;

        if (remainingVibrationTime < 0)
        {
            ToggleVibrationSound(false);
            StopVibrations();
            return;
        }

        bool vibrate = Mathf.Sin(remainingVibrationTime * (float)Math.PI / selectedVibSingleLength) > 0;

        if(vibrate == false)
        {
            phoneElement.localRotation = Quaternion.identity;
            ToggleVibrationSound(false);
            return;
        }

        ToggleVibrationSound(true);

        float pingPong = Mathf.PingPong(remainingVibrationTime * vibFrequency, 1) - 0.5f;
        float rotationZ = pingPong * vibAngle;

        phoneElement.localRotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private void ToggleVibrationSound(bool value)
    {
        if (value == isVibrationActive)
            return;

        isVibrationActive = value;

        if (isVibrationActive)
            SoundsManager.PlayVibrationsAudio();
        else
            SoundsManager.StopVibrationsAudio();
    }
}
