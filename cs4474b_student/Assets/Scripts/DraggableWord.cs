using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DraggableWord : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TextMeshProUGUI label;

    public string Word { get; private set; }

    private Canvas _rootCanvas;
    private RectTransform _rect;
    private CanvasGroup _canvasGroup;
    private Transform _originalParent;
    private Vector2 _originalPosition;
    private int _originalSiblingIndex;
    private Image _image;
    private Color _originalColor;

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
        _image = GetComponent<Image>();
        _originalColor = _image.color;
    }

    public void OnBeginDrag(PointerEventData e)
    {
        _originalParent = transform.parent;
        _originalPosition = _rect.anchoredPosition;
        _originalSiblingIndex = transform.GetSiblingIndex();

        transform.SetParent(_rootCanvas.transform, true);
        transform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
        CursorManager.Instance.SetGrab();
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
        CursorManager.Instance.SetDefault();
    }

    public void SnapBack()
    {
        transform.SetParent(_originalParent, true);
        transform.SetSiblingIndex(_originalSiblingIndex);
        _rect.anchoredPosition = _originalPosition;
        _image.color = _originalColor;
    }

    public IEnumerator Flash(Color color, float duration)
    {
        _image.color = color;
        yield return new WaitForSeconds(duration);
        _image.color = _originalColor;
    }
}