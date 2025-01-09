using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisibleController : MonoBehaviour
{
    private void Awake()
    {
        GameEvents.OnObjectInteractionStart += CursorActivate;
        GameEvents.OnObjectInteractionEnd += CursorDeactivate;
         
    }
    private void OnDestroy()
    {
        GameEvents.OnObjectInteractionStart -= CursorActivate;
        GameEvents.OnObjectInteractionEnd -= CursorDeactivate;
    }

    private void Start()
    {
        CursorDeactivate();
    }

    private void CursorActivate()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void CursorDeactivate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
