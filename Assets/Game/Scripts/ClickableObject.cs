using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickableObject: MonoBehaviour
{
    private Material _material;
    private Color _color;
    protected virtual void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _color = _material.color;
    }
    public void OnHoverEnter()
    {
        _material.color = _color* 1.3f;
    }
    public void OnHoverExit()
    {
        _material.color = _color;
    }

    public abstract void OnClick(int mouseButton);
}
