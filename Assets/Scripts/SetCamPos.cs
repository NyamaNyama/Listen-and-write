using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetCamPos : NetworkBehaviour
{
    void Start()
    {
        if (isLocalPlayer)
        {
            Transform cameraTransform = Camera.main.gameObject.transform;
            cameraTransform.parent = gameObject.transform;
            cameraTransform.position = gameObject.transform.position;
            cameraTransform.rotation = gameObject.transform.rotation;
        }
    }

    
}
