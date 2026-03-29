using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Image zoneImage;
    public Color emptyColor = new Color(1f, 1f, 0f, 0.3f);
    public Color filledColor = new Color(0f, 1f, 0f, 0.3f);

    private DraggableWord _currentCard;

    void Start() => Reset();

    public void OnDrop(PointerEventData e)
    {
        DraggableWord card = e.pointerDrag?.GetComponent<DraggableWord>();
        if (card == null) return;

        if (_currentCard != null)
            _currentCard.SnapBack();
        
        _currentCard = card;
        card.transform.SetParent(transform, false);
        card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        zoneImage.color = filledColor;
    }

    public void Reset()
    {
        if (_currentCard != null)
        {
            _currentCard.SnapBack();
            _currentCard = null;
        }
        zoneImage.color = emptyColor;
    }
}