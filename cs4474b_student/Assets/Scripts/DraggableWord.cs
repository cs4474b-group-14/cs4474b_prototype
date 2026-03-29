using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DraggableWord : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI label;

    public string Word { get; private set; }

    private Canvas _rootCanvas;
    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    private Transform _originalParent;
    private Vector2 _originalPosition;
    private int _originalSiblingIndex;

    public void Init(string word)
    {
        Word = word;
        label.text = word;
    }

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _rootCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        _originalParent = transform.parent;
        _originalPosition = _rect.anchoredPosition;
        _originalSiblingIndex = transform.GetSiblingIndex();

        transform.SetParent(_rootCanvas.transform, true);
        transform.SetAsLastSibling();

        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData e)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rootCanvas.GetComponent<RectTransform>(),
            e.position,
            e.pressEventCamera,
            out Vector2 localPoint
        );
        _rect.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData e)
    {
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _rootCanvas.transform)
            SnapBack();
    }

    public void SnapBack()
    {
        transform.SetParent(_originalParent, true);
        transform.SetSiblingIndex(_originalSiblingIndex);
        _rect.anchoredPosition = _originalPosition;
    }
}