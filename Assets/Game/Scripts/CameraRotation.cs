using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity;

    private float _verticalRotation = 0f;
    private float _horizontRotation = 0f;

    private const float MAX_VERTICAL_ANGLE = 90f;
    private const float MAX_HORIZONTAL_ANGLE = 90f;

    private bool isRotate;

    private void Awake()
    {
        GameEvents.OnObjectInteractionStart += StopRotatate;
        GameEvents.OnObjectInteractionEnd += StartRotate;
    }

    private void OnDestroy()
    {
        GameEvents.OnObjectInteractionStart -= StopRotatate;
        GameEvents.OnObjectInteractionEnd -= StartRotate;
    }
    private void Start()
    {
        StartRotate();
    }


    void Update()
    {
        if(isRotate) Rotate();
    }

    private void Rotate()
    {
        float inputX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float inputY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _verticalRotation -= inputY;
        _horizontRotation += inputX;

        _verticalRotation = Mathf.Clamp(_verticalRotation, -MAX_VERTICAL_ANGLE, MAX_VERTICAL_ANGLE);
        _horizontRotation = Mathf.Clamp(_horizontRotation, -MAX_HORIZONTAL_ANGLE, MAX_HORIZONTAL_ANGLE);

        transform.localEulerAngles = new Vector3(_verticalRotation, _horizontRotation, 0);
    }

    private void StopRotatate() => isRotate = false;
    private void StartRotate() => isRotate = true;
}
