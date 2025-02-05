using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppsInBackgroundView_Button : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameLabel;

    [Space]
    [SerializeField] private float heightAfterDelete = 0;
    [SerializeField] private float speedChangeHeight = 300;
    [SerializeField] private float speedMoving = 2000;
    [SerializeField] private Transform visualsObject;

    private RectTransform rectTransform;
    private AppData appData;
    private bool isDeleting = false;

    public void SetupButton(AppData appData)
    {
        this.appData = appData;

        iconImage.sprite = appData.appIcon;
        nameLabel.text = appData.name;

        rectTransform = GetComponent<RectTransform>();
    }

    public void Click()
    {
        if (isDeleting)
            return;

        PhoneSimulatorManager.Instance.OpenApp(appData.ID);
    }

    public void DeleteFromBackground()
    {
        if (isDeleting)
            return;

        isDeleting = true;
        AppsInBackgroundView.Instance.DeleteFromBackground(appData.ID);
    }

    private void Update()
    {
        if (isDeleting == false)
            return;

        Vector3 newPosition = visualsObject.position;
        newPosition.x += Time.deltaTime * speedMoving;
        visualsObject.position = newPosition;

        Vector2 newSize = rectTransform.sizeDelta;
        newSize.y = Mathf.MoveTowards(newSize.y, heightAfterDelete, Time.deltaTime * speedChangeHeight);
        rectTransform.sizeDelta = newSize;

        if (newSize.y == heightAfterDelete)
            Destroy(gameObject);
    }
}
