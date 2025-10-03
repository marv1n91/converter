using System;
using System.Numerics;

class Program
{
    static readonly char[] Digits =
        "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmn".ToCharArray();

    static void Main()
    {
        Console.WriteLine("Введите число:");
        string input = Console.ReadLine();

        Console.WriteLine("Введите исходную систему счисления (2-50):");
        int fromBase = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите конечную систему счисления (2-50):");
        int toBase = int.Parse(Console.ReadLine());

        try
        {
            string result = ConvertBase(input, fromBase, toBase);
            Console.WriteLine($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static string ConvertBase(string number, int fromBase, int toBase)
    {
        if (fromBase < 2 || fromBase > Digits.Length || toBase < 2 || toBase > Digits.Length)
            throw new ArgumentException($"Системы счисления должны быть в диапазоне 2-{Digits.Length}.");

        BigInteger decimalValue = ToDecimal(number, fromBase);

        return FromDecimal(decimalValue, toBase);
    }

    static BigInteger ToDecimal(string number, int fromBase)
    {
        BigInteger result = 0;
        foreach (char c in number)
        {
            int digit = Array.IndexOf(Digits, c);
            if (digit < 0 || digit >= fromBase)
                throw new ArgumentException($"Недопустимый символ '{c}' для системы счисления {fromBase}.");
            result = result * fromBase + digit;
        }
        return result;
    }

    static string FromDecimal(BigInteger value, int toBase)
    {
        if (value == 0) return "0";
        string result = "";
        while (value > 0)
        {
            int digit = (int)(value % toBase);
            result = Digits[digit] + result;
            value /= toBase;
        }
        return result;
    }
}
