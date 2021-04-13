using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IDragHandler
{
    private RectTransform RectTransform;

    [SerializeField]
    private Canvas currentCanvas;

    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition += eventData.delta / currentCanvas.scaleFactor;

    }
}
