using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExampleData", menuName ="ScriptableObjects")]
public class ExampleValues : ScriptableObject
{
    [SerializeField] private List<char> numbers;
    [SerializeField] private List<char> operators;

    [SerializeField] private List<Sprite> numbersSprite;
    [SerializeField] private List<Sprite> operatorsSprite;


    public List<char> Numbers => numbers;
    public List<char> Operators => operators;

    public List<Sprite> NumbersSprite => numbersSprite;
    public List <Sprite> OperatorsSprite => operatorsSprite;
    public int GetLength()
    {
        return numbers.Count + operators.Count;
    }
}
