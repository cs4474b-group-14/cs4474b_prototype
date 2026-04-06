using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProofCursorManager : MonoBehaviour
{
    public static ProofCursorManager Instance { get; private set; }

    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor;

    private Vector2 _defaultHotspot;
    private Vector2 _hoverHotspot;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        _defaultHotspot = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        _hoverHotspot = new Vector2(hoverCursor.width / 2, hoverCursor.height / 2);
        SetDefault();
    }

    public void SetDefault()
    {
        Cursor.SetCursor(defaultCursor, _defaultHotspot, CursorMode.Auto);
    }

    public void SetHover()
    {
        Cursor.SetCursor(hoverCursor, _hoverHotspot, CursorMode.Auto);
    }
}