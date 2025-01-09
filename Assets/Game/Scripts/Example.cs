using System.Data;
using System.Collections.Generic;
using UnityEngine;

public class Example 
{
    private List<char> operators;
    private List<char> numbers;

    public Example(ExampleValues values)
    {
        operators = new List<char>(values.Operators) ;
        numbers = new List<char>(values.Numbers) ;
        ShuffleUtility.ShuffleList(operators);
        ShuffleUtility.ShuffleList(numbers);
    }
    public List<char> GenerateExample(int numberCount, out string result)
    {
        List<char> values = new List<char>();
        for(int i = 0; i < numberCount; i++)
        {
            char value = numbers[Random.Range(0, numbers.Count)];
            
            values.Add(value);

            if(i != numberCount - 1)
            {
                char operation = operators[Random.Range(0, operators.Count)]; ;
                values.Add(operation);
            }
        }
        result = SolveExample(values);
        return values;
    }

    private string SolveExample(List<char> list)
    {
        string example = string.Join(" ", list);
        DataTable dt = new DataTable();
        return dt.Compute(example,string.Empty).ToString();
    }
}
