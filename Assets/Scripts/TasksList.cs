using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksList : ClickableObject
{
    private const float MOVE_TIME = 0.3f;
    private TransformSnapshot _oldTransform;
    private Camera _mainCamera;

    private void Start()
    {
        _oldTransform = new TransformSnapshot(this.transform);
        _mainCamera = Camera.main;
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

        StopAllCoroutines();
        StartCoroutine(MoveTo(target));
    }

    private IEnumerator MoveTo(TransformSnapshot target)
    {
        float time = 0;
        while (time < MOVE_TIME)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position, time / MOVE_TIME);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, time / MOVE_TIME);
            yield return null;
        }
    }
}
