using Mirror.BouncyCastle.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleOnList : MonoBehaviour
{
    [SerializeField] private List<Sprite> numberSprites;
    [SerializeField] private List<Sprite> operatorSprites;
    [SerializeField] private int decodersCount;
    [SerializeField] private int numbersCount;
    [SerializeField] private GameObject panel;
    private List<string> _exampleList;
    private string _result;
    private Dictionary<string, Sprite> _trueDecoder;
    
    void Start()
    {
        float time = Time.time;
        Example example = new Example();
        Decoder decoder = new Decoder(example, numberSprites, operatorSprites, decodersCount);
        _exampleList = example.GenerateExample(numbersCount, out _result);
        decoder.GenerateDecoders();
        _trueDecoder = decoder.GetTrueDecoder();
        SetExample();
    }

    private void SetExample()
    {
        foreach(string value  in _exampleList)
        {
            GameObject imageObject = new GameObject("ExampleSprite", typeof(Image));


            imageObject.transform.SetParent(panel.transform, false);


            RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100, 100);


            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.sprite = _trueDecoder[value];

        }
    }
}
