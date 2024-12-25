using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksList : ClickableObject
{
    private const float MOVE_TIME = 0.3f;
    private bool _isInteract = false;
    private TransformSnapshot _oldTransform;

    private void Start()
    {
        _oldTransform = new TransformSnapshot(this.transform);
    }

    public override void OnClick()
    {
        TransformSnapshot target;
        Camera.main.GetComponent<CameraRotation>().enabled = _isInteract;

        if(_isInteract)
        {
            target =_oldTransform;
        }

        else
        {
            target = new TransformSnapshot(Camera.main.transform.GetChild(0));
        }

        StopAllCoroutines();
        StartCoroutine(Moveble(target));
    }

    private IEnumerator Moveble(TransformSnapshot target)
    {
        _isInteract = !_isInteract;
        float time = 0;
        while (time < MOVE_TIME)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position, time / MOVE_TIME);
            transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation, time/MOVE_TIME);
            yield return null;
        }
    }
}
