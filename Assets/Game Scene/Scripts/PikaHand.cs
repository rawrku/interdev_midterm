using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikaHand : MonoBehaviour
{
    public Texture2D customCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;

    void Start()
    {
        hotSpot = new Vector2(customCursor.width / 2, 0);
        Cursor.SetCursor(customCursor, hotSpot, cursorMode);
    }
}
