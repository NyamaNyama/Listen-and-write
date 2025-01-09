using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    private void Start()
    {
        GetComponent<ExampleOnList>().Init();
        GetComponent<SetAnswerButton>().Init();
    }
}
