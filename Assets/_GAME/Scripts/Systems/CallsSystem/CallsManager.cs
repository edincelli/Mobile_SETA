using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class CallsManager : GameSystemComponent
{
    public static CallsManager Instance { get; private set; }

    [SerializeField] private AppData callsApp;

    private float nextStageTime = 0;
    private AudioSource audioSource;

    public static bool WaitBeforeStartingNextStage { get; set; }
    public static bool CanHangUp { get; set; }

    public bool CallIncoming { get; private set; }
    public bool CallInProgress { get; private set; }
    public CallScript CurrentCallScript { get; private set; }
    public MessageContactData Contact { get; private set; }
    public float CallTime { get; private set; }
    public int CallStage { get; private set; }

    public string CallTimeString
    {
        get
        {
            int minutes = (int)CallTime / 60;
            int seconds = (int)CallTime % 60;

            string colon = CallTime % 1 <= 0.5f ? ":": " ";
            string minutesString;
            string secondsString;

            if(minutes < 10)
                minutesString = "0" + minutes;
            else
                minutesString = minutes.ToString();

            if (seconds < 10)
                secondsString = "0" + seconds;
            else
                secondsString = seconds.ToString();

            return minutesString + colon +secondsString;

        }
    }

    private App_Call CallsAppController => (App_Call)PhoneSimulatorManager.Instance.GetAppControllerByID(callsApp.ID);

    public void SetupCall(CallScript callScript)
    {
        if (CallIncoming) 
            return;

        if (CallInProgress)
            return;

        CurrentCallScript = callScript;
        Contact = callScript.contact;
        CallIncoming = true;
        CallTime = 0;

        ShowApp();
        CallsAppController.ShowIncomingCall();
        VibrationsManager.Instance.StartCallVibrations();
        PhoneSimulatorManager.LockMainButtons = true;
        SoundsManager.PlayCallAudio();
    }

    public void AcceptCall()
    {
        if (CallIncoming == false)
            return;

        if (CallInProgress)
            return;

        CallInProgress = true;
        CallsAppController.ShowOngoinCall();
        VibrationsManager.Instance.StopVibrations();
        PhoneSimulatorManager.LockMainButtons = false;

        CallStage = -1;
        PlayNextCallStage();
        SoundsManager.StopCallAudio();
    }

    public void StopCall()
    {
        if (CanHangUp == false)
            return;

        CallIncoming = false;
        CallInProgress = false;
        CurrentCallScript = null;
        Contact = null;

        audioSource.Stop();

        PhoneSimulatorManager.LockMainButtons = false;

        ShowHideOverlay();
        CallsAppController.CloseApp();
        PhoneSimulatorManager.Instance.ShowMainScreen();
        VibrationsManager.Instance.StopVibrations();
        CallTranscriptUI.Instance.HideCallTranscript();
        SoundsManager.StopCallAudio();
    }

    public void ShowHideOverlay()
    {
        if (CallInProgress == false)
        {
            CallOverlayUI.Instance.HideCallOverlay();
            return;
        }

        if (CallsAppController.IsActive)
        {
            CallOverlayUI.Instance.HideCallOverlay();
            return;
        }

        CallOverlayUI.Instance.ShowCallOverlay();
    }

    public void ShowApp()
    {
        PhoneSimulatorManager.Instance.OpenApp(callsApp.ID);
    }

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateCall();
    }

    private void UpdateCall()
    {
        if (CallInProgress == false)
            return;

        if (CallTime > nextStageTime 
            && PhoneSimulatorManager.IsAnyExtraPanelActive == false
            && WaitBeforeStartingNextStage == false)
                PlayNextCallStage();

        CallTime += Time.deltaTime;
    }

    private void PlayNextCallStage()
    {
        if (CallStage >= CurrentCallScript.stages.Count - 1)
        {
            //if finish call
                //finish call

            return;
        }

        CallStage++;
        CallScript_Stage stage = CurrentCallScript.stages[CallStage];


        nextStageTime = CallTime + stage.stageWaitTime;

        if (stage.audioClip != null)
        {
            audioSource.PlayOneShot(stage.audioClip);
            nextStageTime += stage.audioClip.length;
        }

        CallTranscriptUI.Instance.ShowTranscript(stage.transcript);
    }
}
