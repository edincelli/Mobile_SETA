using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMoveUI : MonoBehaviour
{
    [SerializeField] private float maxOffset = 300f;
    [SerializeField, Range(0.1f, 10f)] private float speed = 1f;
    [SerializeField] private float constOffset = 300f;

    private RectTransform rectTransform;
    private float initialPosX;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosX = rectTransform.localPosition.x;
    }

    private void Update()
    {
        float newOffset = Mathf.Sin(Time.realtimeSinceStartup * speed) * maxOffset;
        
        Vector3 newPosition = rectTransform.localPosition;
        newPosition.x = initialPosX + newOffset + constOffset;
    
        rectTransform.localPosition = newPosition;
    }
}
