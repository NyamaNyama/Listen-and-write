using Mirror.BouncyCastle.Security;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleOnList : MonoBehaviour
{
    private const int MAX_EXAMPLE_NUMBER = 5;
    private const int MIN_EXAMPLE_NUMBER = 2;

    [SerializeField] private ExampleValues exampleData;

    [SerializeField] private int decodersCount;

    [SerializeField] private GameObject examplePanel;
    
    

    [SerializeField] private Image exampleValueImage;
    
    
    private List<char> _exampleList;
    private string _result;
    public string Result => _result;
    private Dictionary<char, Sprite> _trueDecoder;
    public Dictionary<char, Sprite> TrueDecoder => _trueDecoder;

    public void Init()
    {
        Example example = new Example(exampleData);
        Decoder decoder = new Decoder(exampleData, decodersCount);
        _exampleList = example.GenerateExample(Random.Range(MIN_EXAMPLE_NUMBER, MAX_EXAMPLE_NUMBER + 1), out _result);
        decoder.GenerateDecoders();
        _trueDecoder = decoder.GetTrueDecoder();
        SetExample();
        Debug.Log(_result);
    }


    private void SetExample()
    {
        foreach (char value in _exampleList)
        {
            Debug.Log($"{value} {_trueDecoder[value]}");
            Image imageObject = Instantiate(exampleValueImage, examplePanel.transform);
            imageObject.sprite = _trueDecoder[value];
        }
    }
}
