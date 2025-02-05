using Sirenix.OdinInspector;
using UnityEngine;

public partial class PhoneSimulatorManager
{
    [Header("Panels Settings")]
    [SerializeField] private GameObject pausePanel;

    public static bool IsAnyExtraPanelActive
    {
        get
        {
            if(Instance.pausePanel.activeSelf)
                return true;

            return false;
        }
    }
    
    public void ShowPausePanel()
    {
        HideAllExtraPanels();
        pausePanel.SetActiveOptimized(true);
    }

    [Button("Hidde All Extra Panels")]
    public void HideAllExtraPanels()
    {
        pausePanel.SetActiveOptimized(false);
    }

    private void AwakePanels()
    {
        HideAllExtraPanels();
    }
}
