using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickableObject: MonoBehaviour
{

    public void OnHoverEnter()
    {
        Debug.Log("навелся");
    }
    public void OnHoverExit()
    {

    }

    public abstract void OnClick(int mouseButton);
}
