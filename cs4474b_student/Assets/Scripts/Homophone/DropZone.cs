using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image zoneImage;
    [SerializeField] private Color emptyColor = new Color(1f, 1f, 0f, 0.3f);
    [SerializeField] private Color correctColor = new Color(25/255f, 185/255f, 25/255f, 0.3f);
    [SerializeField] private Color wrongColor = new Color(219/255f, 55/255f, 55/255f, 0.3f);

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

        RectTransform cardRect = card.GetComponent<RectTransform>();
        RectTransform zoneRect = GetComponent<RectTransform>();
        cardRect.sizeDelta = zoneRect.sizeDelta;
        cardRect.pivot = new Vector2(0.5f, 0.5f);
        cardRect.anchorMin = new Vector2(0.5f, 0.5f);
        cardRect.anchorMax = new Vector2(0.5f, 0.5f);
        cardRect.anchoredPosition = Vector2.zero;

        bool correct = GameManager.Instance.CheckAnswer(card.Word);
        StartCoroutine(HandleResult(card, correct));
    }

    IEnumerator HandleResult(DraggableWord card, bool correct)
    {
        if (correct)
        {
            StartCoroutine(card.Flash(Color.green, 1.5f));
            zoneImage.color = correctColor;
        }
        else
        {
            StartCoroutine(card.Flash(Color.red, 0.5f));
            zoneImage.color = wrongColor;
            yield return new WaitForSeconds(0.5f);
            _currentCard = null;
            card.SnapBack();
            zoneImage.color = emptyColor;
        }
    }

    public void SetCorrect(bool correct)
    {
        zoneImage.color = correct ? correctColor : wrongColor;
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