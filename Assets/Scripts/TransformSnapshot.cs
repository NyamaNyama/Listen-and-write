using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TransformSnapshot 
{
    public Vector3 position { get; private set; }
    public Quaternion rotation { get; private set; }
    public TransformSnapshot(Transform obj)
    {
        this.position = obj.position;
        this.rotation = obj.rotation;
    }

    public TransformSnapshot(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
