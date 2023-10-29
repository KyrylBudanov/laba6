using System;

public class MathOperations
{
    public static dynamic Add(dynamic a, dynamic b)
    {
        return a + b;
    }

    public static dynamic Subtract(dynamic a, dynamic b)
    {
        return a - b;
    }

    public static dynamic Multiply(dynamic a, dynamic b)
    {
        return a * b;
    }

    public static dynamic Divide(dynamic a, dynamic b)
    {
        if (b == 0)
        {
            throw new ArgumentException("Division by zero is not allowed.");
        }
        return a / b;
    }
}

class Program
{
    static void Main()
    {
        int a = 5, b = 3;
        Console.WriteLine($"Add: {MathOperations.Add(a, b)}");
        Console.WriteLine($"Subtract: {MathOperations.Subtract(a, b)}");
        Console.WriteLine($"Multiply: {MathOperations.Multiply(a, b)}");
        Console.WriteLine($"Divide: {MathOperations.Divide(a, b)}");

        double x = 2.5, y = 1.5;
        Console.WriteLine($"Add: {MathOperations.Add(x, y)}");
        Console.WriteLine($"Subtract: {MathOperations.Subtract(x, y)}");
        Console.WriteLine($"Multiply: {MathOperations.Multiply(x, y)}");
        Console.WriteLine($"Divide: {MathOperations.Divide(x, y)}");

        int[] arr1 = { 1, 2, 3 };
        int[] arr2 = { 4, 5, 6 };
        int[] resultArray = new int[arr1.Length];
        for (int i = 0; i < arr1.Length; i++)
        {
            resultArray[i] = MathOperations.Add(arr1[i], arr2[i]);
        }

        Console.WriteLine("Add Arrays:");
        foreach (var item in resultArray)
        {
            Console.Write($"{item} ");
        }
    }
}