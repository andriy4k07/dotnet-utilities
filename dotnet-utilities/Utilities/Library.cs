using System.Globalization;

namespace Utilities;

public static class ConsoleEx
{
    public static T ReadValue<T>(string message) where T : IParsable<T>
    {
        T? value;
        WriteColor(ConsoleColor.Green, $"{message} \t");
        while (!T.TryParse(Console.ReadLine(), null, out value))
        {
            WriteColor(ConsoleColor.Red, "НЕПРАВИЛЬНИЙ ТИП ДАНИХ! ");
            WriteColor(ConsoleColor.Green, $"{message} \t");
        }
        return value;
    }
    public static char ReadChar(string message)
    {
        char input;
        WriteColor(ConsoleColor.Green, $"{message} \t");
        while (!char.TryParse(Console.ReadLine(), out input))
        {
            WriteColor(ConsoleColor.Red, "Введіть один символ! ");
            WriteColor(ConsoleColor.Green, $"{message} \t");
        }
        return input;
    }

    public static string ReadString(string message)
    {
        string? value; 
        WriteColor(ConsoleColor.Green, $"{message} \t");

        while (true)
        {
            value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
                break;
            WriteColor(ConsoleColor.Red, "НЕПРАВИЛЬНИЙ ТИП ДАНИХ! ");
            WriteColor(ConsoleColor.Green, $"{message} \t");
        }

        return value;
    }

    public static DateTime ReadDateTime(string message)
    {
        string[] formats = { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" };
        var culture = CultureInfo.GetCultureInfo("uk-UA");
        DateTime date;
        WriteColor(ConsoleColor.Green, $"{message} \t");
        
        while (!DateTime.TryParseExact(Console.ReadLine(), formats, culture, DateTimeStyles.None, out date))
        {
            WriteColor(ConsoleColor.Red, "НЕПРАВИЛЬНИЙ ФОРМАТ ДАТИ! ");
            WriteColor(ConsoleColor.Green, $"Введіть дату у форматі dd.MM.yyyy або dd.MM.yyyy HH:mm \t");
        }
        return date;
    }
    public static void WriteLineColor(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"{message}");
        Console.ResetColor();
    }
    public static void WriteColor(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write($"{message}");
        Console.ResetColor();
    }
}

public static class ArrayHelper
{
    public static int[] Generate1DArray(int count, int minValue, int maxValue, int? seed = null)
    {
        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        int[] array = new int[count];

        for (int i = 0; i < array.Length; i++)
        {
            int randomNumber = random.Next(minValue, maxValue);
            array[i] = randomNumber;
        }
        return array;
    }

    public static int[,] Generate2DArray(int rows, int cols, int minValue, int maxValue, int? seed = null)
    {
        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        int[,] array = new int[rows, cols];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                int randomNumber = random.Next(minValue, maxValue);
                array[i, j] = randomNumber;
            }
        }
        return array;
    }

    public static int[] Create1DIntArray(int count)
    {
        int[] array = new int[count];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            int number = ConsoleEx.ReadValue<int>($"Введіть {i + 1} елемент масиву: \t");
            array[i] = number;
        }
        return array;
    }
    
    public static string[] Create1DStringArray(int count)
    {
        string[] array = new string[count];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            string elements = ConsoleEx.ReadString($"Введіть {i + 1} елемент масиву: \t");
            array[i] = elements;
        }
        return array;
    }
}
public static class ArrayExtensions
{
    public static void Print<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string? value = array[i] is not null ? array[i]?.ToString() : "[null]";
            Console.WriteLine($"{i}: {value}");
        }
    }
    public static void PrintOneLine<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string? value = array[i] is not null ? array[i]?.ToString() : "[null]";
            Console.Write($"{value} ");
        }
        Console.WriteLine();
    }
    public static void Print2DMatrix<T>(this T[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write($"{array[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    public static void FillFromConsole(this int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = ConsoleEx.ReadValue<int>($"Введіть {i + 1} елемент масиву:");
        }
    }
    public static void FillRandom(this int[] array, int minValue, int maxValue, int? seed = null)
    {
        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(minValue, maxValue);
        }
    }
}