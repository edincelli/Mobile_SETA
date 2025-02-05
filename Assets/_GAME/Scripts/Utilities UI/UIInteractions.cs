using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIInteractions : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [FoldoutGroup("Events")] public UnityEvent OnStart = new UnityEvent();
    [FoldoutGroup("Events")] public UnityEvent OnMouseEnter = new UnityEvent();
    [FoldoutGroup("Events")] public UnityEvent OnMouseMove = new UnityEvent();
    [FoldoutGroup("Events")] public UnityEvent OnMouseExit = new UnityEvent();
    [FoldoutGroup("Events")] public UnityEvent OnMouseDown = new UnityEvent();
    [FoldoutGroup("Events")] public UnityEvent OnMouseUp = new UnityEvent();
    [FoldoutGroup("Events")] public UnityEvent OnMouseClick = new UnityEvent();

    private void Start()
    {
        OnStart.Invoke();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter.Invoke();
    }

    public virtual void OnPointerMove(PointerEventData eventData)
    {
        OnMouseMove.Invoke();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit.Invoke();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnMouseDown.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        OnMouseUp.Invoke();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnMouseClick.Invoke();
    }
}