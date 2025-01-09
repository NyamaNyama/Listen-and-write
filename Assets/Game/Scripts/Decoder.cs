using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decoder 
{
    private List<char> operators;
    private List<char> numbers;
    private List<Sprite > numberSprites;
    private List<Sprite> operatorSprites;
    private Dictionary<char,Sprite> trueDecoder;
    private List<Dictionary<char, Sprite>> fakeDecoders;
    private int decoderCount;

    public Dictionary<char, Sprite> GetTrueDecoder()
    {
        return new Dictionary<char, Sprite>(trueDecoder);
    }

    public Decoder(ExampleValues values , int decoderCount)
    {
        operators = new List<char>(values.Operators);
        numbers = new List<char>(values.Numbers);

        numberSprites = new List<Sprite>(values.NumbersSprite);
        operatorSprites = new List<Sprite>(values.OperatorsSprite);

        this.decoderCount = decoderCount;

        trueDecoder = new Dictionary<char, Sprite>();
        fakeDecoders = new List<Dictionary<char, Sprite>>();
    }

    private void ShuffleAll()
    {
        ShuffleUtility.ShuffleList(operatorSprites);
        ShuffleUtility.ShuffleList(numberSprites);
    }
    public void GenerateDecoders()
    {
        for (int i = 0; i < decoderCount; i++)
        {
            Dictionary<char, Sprite> newDecoder = CreateDecoder();
            if (i == 0)
            {
                trueDecoder = newDecoder;
            }
            else
            {
                while(IsDecoderDuplicate(newDecoder))
                {
                    newDecoder = CreateDecoder();
                }
                fakeDecoders.Add(newDecoder);
            }
        }
    }

    private Dictionary<char, Sprite> CreateDecoder()
    {
        ShuffleAll();
        Dictionary<char, Sprite> decoder = new Dictionary<char, Sprite>();
        for (int j = 0; j < numbers.Count; j++)
        {
            decoder[numbers[j]] = numberSprites[j];
        }
        for (int k = 0; k < operators.Count; k++)
        {
            decoder[operators[k]] = operatorSprites[k];
        }
        return decoder;
    }

    private bool IsDecoderDuplicate(Dictionary<char, Sprite> newDecoder)
    {
        if (AreDecoderEqual(trueDecoder, newDecoder)) return true;
        foreach(var decoder in fakeDecoders)
        {
            if(AreDecoderEqual(decoder,newDecoder)) return true;
        }
        return false;
    }

    private bool AreDecoderEqual(Dictionary<char, Sprite> decoder1, Dictionary<char, Sprite> decoder2)
    {
        foreach (var pair in decoder1)
        {
            if (!decoder2.TryGetValue(pair.Key, out Sprite value) || value != pair.Value)
            {
                return false;
            }
        }

        return true;
    }

}
