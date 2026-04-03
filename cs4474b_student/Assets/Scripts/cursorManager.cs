using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D grabCursor;

    private Vector2 _defaultHotspot;
    private Vector2 _grabHotspot;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        _defaultHotspot = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        _grabHotspot = new Vector2(grabCursor.width / 2, grabCursor.height / 2);
        SetDefault();
    }

    public void SetDefault()
    {
        Cursor.SetCursor(defaultCursor, _defaultHotspot, CursorMode.Auto);
    }

    public void SetGrab()
    {
        Cursor.SetCursor(grabCursor, _grabHotspot, CursorMode.Auto);
    }
}