using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Terminal.ToggleCursor(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ToggleCursor(bool cursor)
    {
        Cursor.visible = cursor;
        if (cursor) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;

    }
}
