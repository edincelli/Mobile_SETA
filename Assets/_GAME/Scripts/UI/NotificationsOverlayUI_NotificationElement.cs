using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NotificationsOverlayUI_NotificationElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI appNameTMP;
    [SerializeField] private TextMeshProUGUI notificationTMP;
    [SerializeField] private Image appIconImage;
    [SerializeField] private TextMeshProUGUI timeTMP;

    private bool destroyOnClick = false;

    private UnityEvent onClick = new UnityEvent();

    public UnityEvent OnClick => onClick;

    public NotificationsOverlayUI_NotificationElement SetupNotification(string appName, string notificationText, Sprite appIcon, bool destroyOnClick = true)
    {
        appNameTMP.text = appName;
        notificationTMP.text = notificationText;
        appIconImage.sprite = appIcon;
        timeTMP.text = System.DateTime.Now.ToString("hh:mm", CultureInfo.GetCultureInfo("en-US"));
        this.destroyOnClick = destroyOnClick;
        return this;
    }

    public void Click()
    {
        onClick.Invoke();

        if (destroyOnClick)
            DestroyNotification();
    }

    public void DestroyNotification()
    {
        Destroy(gameObject);
    }
}
