using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class TasksList : ClickableObject
{
    private const float MOVE_TIME = 0.3f;
    private TransformSnapshot _oldTransform;
    private Camera _mainCamera;
    private Mover _move;

    private void Start()
    {
        _oldTransform = new TransformSnapshot(this.transform);
        _mainCamera = Camera.main;
        _move = GetComponent<Mover>();
    }

    public override void OnClick(int mouseButton)
    {
        TransformSnapshot target;
        bool isRight = mouseButton == 1;

        if(isRight)
        {
            target =_oldTransform;
        }
        else
        {
            target = new TransformSnapshot(_mainCamera.transform.GetChild(0));
        }

        _move.MoveTo(target, MOVE_TIME);
    }

}
