using System.Data;
using System.Collections.Generic;
using UnityEngine;

public class Example 
{
    private List<string> operators;
    private List<int> numbers;

    public Example()
    {
        operators = new List<string>() { "+", "-", "*"};
        numbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ShuffleUtility.ShuffleList(operators);
        ShuffleUtility.ShuffleList(numbers);
    }
    public List<int> GetNumbersCopy()
    {
        return new List<int>(numbers);
    }

    public List<string> GetOperatorsCopy()
    {
        return new List<string>(operators);
    }

    public List<string> GenerateExample(int numberCount, out string result)
    {
        List<string> values = new List<string>();
        for(int i = 0; i < numberCount; i++)
        {
            string value = numbers[Random.Range(0, numbers.Count)].ToString();
            
            values.Add(value);

            if(i != numberCount - 1)
            {
                string operation = operators[Random.Range(0, operators.Count)]; ;
                values.Add(operation);
            }
        }
        result = SolveExample(values);
        return values;
    }

    private string SolveExample(List<string> list)
    {
        string example = string.Join(" ", list);
        DataTable dt = new DataTable();
        return dt.Compute(example,string.Empty).ToString();
    }
}
