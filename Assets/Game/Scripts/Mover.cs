using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public void MoveTo(TransformSnapshot targetObject, float duration)
    {
        StartCoroutine(MoveToCoroutine(targetObject, duration));
    }

    private IEnumerator MoveToCoroutine(TransformSnapshot targetObject, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetObject.position, elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetObject.rotation, elapsedTime / duration);
            yield return null;
        }
    }
}
