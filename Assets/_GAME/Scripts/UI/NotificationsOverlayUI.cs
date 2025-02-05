using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NotificationsOverlayUI : MonoBehaviour
{
    public static NotificationsOverlayUI Instance { get; private set; }

    [Space]
    [SerializeField] private NotificationsOverlayUI_NotificationElement visibleNotification;

    [Space]
    [SerializeField] private Transform notificationsParent;
    [SerializeField] private GameObject notificationElementPrefab;

    [Space]
    [SerializeField] private float requiredDifferenceMouseY = 100;

    private bool dragging = false;
    private float dragInitialMouseY = 0;

    public static bool Active => Instance.currentVisibility > 0.9f;

    public NotificationsOverlayUI_NotificationElement ShowNotification(string appName, string notificationText, Sprite appIcon)
    {
        visibleNotification.OnClick.RemoveAllListeners();
        visibleNotification.SetupNotification(appName, notificationText, appIcon, false);
        SetPopupTargetVisibility(1);

        NotificationsOverlayUI_NotificationElement notification =
            Instantiate(notificationElementPrefab, notificationsParent)
            .GetComponent<NotificationsOverlayUI_NotificationElement>()
            .SetupNotification(appName, notificationText, appIcon, true);

        notification.OnClick.AddListener(HideNotificationsOverlay);

        visibleNotification.OnClick.AddListener(notification.Click);
        visibleNotification.OnClick.AddListener(ChangePopupVisibilityOnClick);

        VibrationsManager.Instance.StartNotificationVibrations();
        SoundsManager.PlayNotificationAudioOnce();

        return notification;
    }

    public void ShowNotificationsOverlay()
    {
        if (PhoneSimulatorManager.IsUnlocked == false)
            return;

        SetTargetVisibility(1);
    }

    public void HideNotificationsOverlay()
    {
        SetTargetVisibility(0);
    }

    public void DeleteAllNotifications()
    {
        HideNotificationsOverlay();
        notificationsParent.GetComponentsInChildren<NotificationsOverlayUI_NotificationElement>().ForEach(n => n.DestroyNotification());
    }

    public void StartDragging()
    {
        dragging = true;
        dragInitialMouseY = Input.mousePosition.y;
    }

    private void Awake()
    {
        Instance = this;
        
        AwakePopup();

        SetTargetVisibility(0);
        ApplyVisibilitySettings(0);
        SetPopupTargetVisibility(0);
        ApplyPopupVisibilitySettings(0);
    }

    private void Start()
    {
        DeleteAllNotifications();
    }

    private void Update()
    {
        UpdateDragging();
        UpdateVisibility();
        UpdatePopupVisibility();
    }

    private void UpdateDragging()
    {
        if (dragging == false)
            return;

        if(Mathf.Abs(Input.mousePosition.y - dragInitialMouseY) > requiredDifferenceMouseY)
        {
            dragging = false;
            ShowNotificationsOverlay();
        }
    }
}
