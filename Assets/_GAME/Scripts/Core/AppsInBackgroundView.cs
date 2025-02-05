using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class AppsInBackgroundView : MonoBehaviour
{
    public static AppsInBackgroundView Instance { get; private set; }

    [SerializeField] private Transform buttonsParent;
    [SerializeField] private GameObject buttonPrefab;

    [Space]
    [SerializeField] private GameObject emptyListLabel;
    [SerializeField] private GameObject closeAllButton;

    public void DeleteFromBackground(string appID)
    {
        PhoneSimulatorManager.Instance.RemoveAppFromBackground(appID);
    }

    public void RemoveAllAppsFromBackground()
    {
        PhoneSimulatorManager.Instance.RemoveAllAppsFromBackground();
    }

    public void SetupView()
    {
        UpdateButtonsList();

        bool isListEmpty = PhoneSimulatorManager.Instance.AppsInBackground.Count == 0;

        emptyListLabel.SetActive(isListEmpty);
        closeAllButton.SetActive(!isListEmpty);
    }

    private void UpdateButtonsList() 
    { 
        int currentButtonsCount = buttonsParent.childCount;

        for (int i = 0; i < PhoneSimulatorManager.Instance.AppsInBackground.Count; i++)
        {
            int reversedAppDataIndex = PhoneSimulatorManager.Instance.AppsInBackground.Count - i - 1;
            AppsInBackgroundView_Button button;
            AppData appData = PhoneSimulatorManager.Instance.GetAppReferencesByID(PhoneSimulatorManager.Instance.AppsInBackground[reversedAppDataIndex]).appData;

            if (i >= currentButtonsCount)
            {
                Instantiate(buttonPrefab, buttonsParent);
            }

            button = buttonsParent.GetChild(i).GetComponent<AppsInBackgroundView_Button>();
            button.SetupButton(appData);
        }

        for (int i = currentButtonsCount; i > PhoneSimulatorManager.Instance.AppsInBackground.Count; i--)
        {
            Destroy(buttonsParent.GetChild(i - 1).gameObject);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
