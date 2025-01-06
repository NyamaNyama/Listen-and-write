using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZero : MonoBehaviour
{
    private Mover _mover;
    private TransformSnapshot _originalTransform;
    private TransformSnapshot _zeroRotation;
    private const float CAMERA_MOVE_DURATION = 0.1f;

    private void Awake()
    {
        GameEvents.OnObjectInteractionStart += MoveToZero;
        GameEvents.OnObjectInteractionEnd += MoveBack;
    }
    private void OnDestroy()
    {
        GameEvents.OnObjectInteractionStart -= MoveToZero;
        GameEvents.OnObjectInteractionEnd -= MoveBack;
    }
    private void Start()
    {
        _mover = GetComponent<Mover>();
        _zeroRotation = new TransformSnapshot(transform.position, new Quaternion(0,0,0,0));

    }

    private void MoveToZero()
    {
        _originalTransform = new TransformSnapshot(transform);
        _mover.MoveTo(_zeroRotation,CAMERA_MOVE_DURATION);
    }

    private void MoveBack()
    {
        _mover.MoveTo(_originalTransform,CAMERA_MOVE_DURATION);
    }
}
