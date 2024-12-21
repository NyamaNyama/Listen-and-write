using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TransformSnapshot 
{
    public Vector3 position { get; private set; }
    public Quaternion rotation { get; private set; }
    public TransformSnapshot(Transform obj)
    {
        position = obj.position;
        rotation = obj.rotation;
    }
}
