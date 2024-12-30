using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ClickableObject _currentHovered;
    private Camera _mainCamera;
    private bool isInteract = false;
    private bool isDraging = false;
    private float _rightClickDurationStart;
    private const float RIGHT_CLICK_DURATION = 0.3f;
    private const float ROTATION_SENSITIVITY = 0.2f;
    private Vector3 _lastMousePosition;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    void Update()
    {
        if (!isInteract)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            FindInteractObject(ray);
            if (_currentHovered != null)
            {
                if (Input.GetMouseButtonDown(0)) LeftClick();
            }
        }
        
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1)) RightClickStart();
            if(Input.GetMouseButton(1)) ObjectRotate();
            if (Input.GetMouseButtonUp(1)) RightClickEnd();
        }
    }

    private void FindInteractObject(Ray CameraRay)
    {
        if (Physics.Raycast(CameraRay, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out ClickableObject obj))
            {
                if (_currentHovered != obj)
                {
                    _currentHovered?.OnHoverExit();
                    _currentHovered = obj;
                    _currentHovered.OnHoverEnter();
                }
            }
            else
            {
                _currentHovered?.OnHoverExit();
                _currentHovered = null;
            }
        }
    }

    private void RightClickStart()
    {
        isDraging = false;
        _rightClickDurationStart = Time.time;
        _lastMousePosition = Input.mousePosition;
    }
    private void RightClickEnd()
    {
        if(Time.time - _rightClickDurationStart <= RIGHT_CLICK_DURATION && !isDraging)
        {
            GameEvents.TriggerInteracionEnd();
            _currentHovered.OnClick(1);
            isInteract = false;
        }
        _rightClickDurationStart = -1f;
    }
    private void LeftClick()
    {
        GameEvents.TriggerInteracionStart();
        _currentHovered.OnHoverExit();
        _currentHovered.OnClick(0);
        isInteract = true;
    }

    private void ObjectRotate()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if(Vector3.Distance(currentMousePosition, _lastMousePosition) > 0.1f)
        {
            isDraging = true;

            Vector3 mouseDelta = currentMousePosition - _lastMousePosition;

            float rotationX = mouseDelta.y * ROTATION_SENSITIVITY;
            float rotationY = -mouseDelta.x * ROTATION_SENSITIVITY;
            _currentHovered.transform.Rotate(Vector3.up, rotationY, Space.World); 
            _currentHovered.transform.Rotate(Vector3.right, rotationX, Space.World); 

            _lastMousePosition = currentMousePosition;

            _lastMousePosition = currentMousePosition;
        }
    }
}
