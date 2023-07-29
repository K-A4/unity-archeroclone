using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VirtualGamepad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Vector2 Value { get => value; }

    public UnityEvent OnClick;
    public UnityEvent<Vector2> OnHover;
    public UnityEvent OnUp;
    public UnityEvent OnDown;

    [SerializeField]
    private bool StaticGamepad;
    [SerializeField]
    private bool TouchPad;

    private Vector2 value = Vector2.zero;

    private Vector2 prevPos;
    private Vector2 originPos;

    [SerializeField]
    private float threshold;

    
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var val = (eventData.position - prevPos) / (rectTransform.rect.width / 2);
        value = Vector2.ClampMagnitude(val, 1.0f);

        if (TouchPad)
        {
            prevPos = eventData.position;
        }
        OnHover.Invoke(value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originPos = eventData.position;
        prevPos = originPos;
        OnDown.Invoke();
        if (StaticGamepad)
        {
            prevPos = gameObject.transform.position;

            var val = (eventData.position - prevPos) / (rectTransform.rect.width / 2);
            value = Vector2.ClampMagnitude(val, 1.0f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp.Invoke();
        value = Vector2.zero;
        if ((originPos - eventData.position).sqrMagnitude < threshold)
        {
            OnClick.Invoke();
        }
    }

    private void OnDisable()
    {
        value = Vector2.zero;
    }
}
