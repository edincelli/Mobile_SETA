using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILayoutRebuilder : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool forceRebuild = false;

    public void ForceRebuild()
    {
        if (rectTransform == null)
            return;

        forceRebuild = true;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ForceRebuild();
    }

    private void Update()
    {
        if (forceRebuild == false)
            return;

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        forceRebuild = false;
    }
}
