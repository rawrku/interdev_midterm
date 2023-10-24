using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikaHand : MonoBehaviour
{
    public Texture2D customCursor;
    public Vector2 cursorHotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
    }
}
