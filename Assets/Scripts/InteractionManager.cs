using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ClickableObject _currentHovered;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out ClickableObject obj))
            {
                if(_currentHovered != obj)
                {
                    _currentHovered?.OnHoverExit();
                    _currentHovered = obj;
                    _currentHovered.OnHoverEnter();
                }
                if(Input.GetMouseButtonDown(0))
                {
                    _currentHovered?.OnHoverExit();
                    _currentHovered.OnClick();
                }
            }

            else
            {
                _currentHovered?.OnHoverExit();
                _currentHovered = null;
            }
        }
    }
}
