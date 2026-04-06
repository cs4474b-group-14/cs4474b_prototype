using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData e)
    {
        ProofCursorManager.Instance.SetHover();
    }

    public void OnPointerExit(PointerEventData e)
    {
        ProofCursorManager.Instance.SetDefault();
    }
}