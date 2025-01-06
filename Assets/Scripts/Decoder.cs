using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decoder 
{
    private List<string> operators;
    private List<int> numbers;
    private List<Sprite > numberSprites;
    private List<Sprite> operatorSprites;
    private Dictionary<string,Sprite> trueDecoder;
    private List<Dictionary<string, Sprite>> fakeDecoders;
    private int decoderCount;

    public Dictionary<string, Sprite> GetTrueDecoder()
    {
        return new Dictionary<string, Sprite>(trueDecoder);
    }

    public Decoder(Example example, List<Sprite> numberSprites,List<Sprite> operatorSprites, int decoderCount)
    {
        operators = example.GetOperatorsCopy();
        numbers = example.GetNumbersCopy();
        this.numberSprites = numberSprites;
        this.decoderCount = decoderCount;
        this.operatorSprites = operatorSprites;
        trueDecoder = new Dictionary<string, Sprite>();
        fakeDecoders = new List<Dictionary<string, Sprite>>();
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
            Dictionary<string, Sprite> newDecoder = CreateDecoder();
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

    private Dictionary<string, Sprite> CreateDecoder()
    {
        ShuffleAll();
        Dictionary<string, Sprite> decoder = new Dictionary<string, Sprite>();
        for (int j = 0; j < numbers.Count; j++)
        {
            decoder[numbers[j].ToString()] = numberSprites[j];
        }
        for (int k = 0; k < operators.Count; k++)
        {
            decoder[operators[k]] = operatorSprites[k];
        }
        return decoder;
    }

    private bool IsDecoderDuplicate(Dictionary<string, Sprite> newDecoder)
    {
        if (AreDecoderEqual(trueDecoder, newDecoder)) return true;
        foreach(var decoder in fakeDecoders)
        {
            if(AreDecoderEqual(decoder,newDecoder)) return true;
        }
        return false;
    }

    private bool AreDecoderEqual(Dictionary<string, Sprite> decoder1, Dictionary<string, Sprite> decoder2)
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
