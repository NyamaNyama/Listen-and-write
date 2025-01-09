using Mirror.BouncyCastle.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerChecker : MonoBehaviour
{
    [SerializeField] GameObject answerPanel;

    [SerializeField] ExampleOnList example;
    private List<Button> buttonList;


    
    public void Check()
    {
        string result = example.Result;
        Dictionary<char,Sprite> decoder = new Dictionary<char,Sprite>(example.TrueDecoder);

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < answerPanel.transform.childCount; i++) 
        {
            list.Add(answerPanel.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < result.Length; i++) 
        {
            if (decoder[result[i]] != list[i].GetComponent<Button>().image.sprite)
            {
                Debug.Log("Не угадал");
                return;
            }
        }
        Debug.Log("Угадал");
    }
}
