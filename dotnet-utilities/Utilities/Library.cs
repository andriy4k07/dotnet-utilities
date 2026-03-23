using System.Globalization;

namespace Utilities;

/// <summary>
/// Provides static methods for enhanced console input/output,
/// including colored output and input validation with retry loops.
/// </summary>
public static class ConsoleEx
{
    /// <summary>
    /// Reads a value of the specified type from the console, repeating the prompt
    /// until a valid value is entered.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value to read. Must implement <see cref="IParsable{T}"/>.
    /// </typeparam>
    /// <param name="message">The prompt message displayed before input.</param>
    /// <returns>A successfully parsed value of type <typeparamref name="T"/>.</returns>
    /// <example>
    /// <code>
    /// int age = ConsoleEx.ReadValue&lt;int&gt;("Enter age:");
    /// double price = ConsoleEx.ReadValue&lt;double&gt;("Enter price:");
    /// </code>
    /// </example>
    public static T ReadValue<T>(string message) where T : IParsable<T>
    {
        T? value;
        WriteColor(ConsoleColor.Green, $"{message} \t");
        while (!T.TryParse(Console.ReadLine(), null, out value))
        {
            WriteColor(ConsoleColor.Red, "INCORRECT DATA TYPE! ");
            WriteColor(ConsoleColor.Green, $"{message} \t");
        }
        return value;
    }

    /// <summary>
    /// Reads a single <see cref="char"/> from the console, repeating the prompt
    /// if the input contains more than one character or is empty.
    /// </summary>
    /// <param name="message">The prompt message displayed before input.</param>
    /// <returns>The <see cref="char"/> entered by the user.</returns>
    /// <example>
    /// <code>
    /// char choice = ConsoleEx.ReadChar("Enter choice (y/n):");
    /// </code>
    /// </example>
    public static char ReadChar(string message)
    {
        char input;
        WriteColor(ConsoleColor.Green, $"{message} \t");
        while (!char.TryParse(Console.ReadLine(), out input))
        {
            WriteColor(ConsoleColor.Red, "Please enter a single character! ");
            WriteColor(ConsoleColor.Green, $"{message} \t");
        }
        return input;
    }

    /// <summary>
    /// Reads a non-empty string from the console, repeating the prompt
    /// if the input is empty or consists only of whitespace characters.
    /// </summary>
    /// <param name="message">The prompt message displayed before input.</param>
    /// <returns>A non-empty <see cref="string"/> entered by the user.</returns>
    /// <example>
    /// <code>
    /// string name = ConsoleEx.ReadString("Enter name:");
    /// </code>
    /// </example>
    public static string ReadString(string message)
    {
        string? value;
        WriteColor(ConsoleColor.Green, $"{message} \t");

        while (true)
        {
            value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
                break;
            WriteColor(ConsoleColor.Red, "INCORRECT DATA TYPE! ");
            WriteColor(ConsoleColor.Green, $"{message} \t");
        }

        return value;
    }

    /// <summary>
    /// Reads a date and time from the console in <c>dd.MM.yyyy HH:mm</c> or <c>dd.MM.yyyy</c>
    /// format using the Ukrainian locale, repeating the prompt on invalid input.
    /// </summary>
    /// <param name="message">The prompt message displayed before input.</param>
    /// <returns>
    /// A <see cref="DateTime"/> value parsed from the user's input.
    /// </returns>
    /// <remarks>
    /// Supported input formats:
    /// <list type="bullet">
    ///   <item><description><c>dd.MM.yyyy HH:mm</c> — date and time</description></item>
    ///   <item><description><c>dd.MM.yyyy</c> — date only</description></item>
    /// </list>
    /// Parsing is performed using the <c>uk-UA</c> culture.
    /// </remarks>
    /// <example>
    /// <code>
    /// DateTime birthday = ConsoleEx.ReadDateTime("Enter date of birth:");
    /// </code>
    /// </example>
    public static DateTime ReadDateTime(string message)
    {
        string[] formats = { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" };
        var culture = CultureInfo.GetCultureInfo("uk-UA");
        DateTime date;
        WriteColor(ConsoleColor.Green, $"{message} \t");

        while (!DateTime.TryParseExact(Console.ReadLine(), formats, culture, DateTimeStyles.None, out date))
        {
            WriteColor(ConsoleColor.Red, "INCORRECT DATE FORMAT! ");
            WriteColor(ConsoleColor.Green, $"Enter the date in the format dd.MM.yyyy or dd.MM.yyyy HH:mm \t");
        }
        return date;
    }

    /// <summary>
    /// Writes a string to the console followed by a newline, using the specified text color.
    /// The console color is reset to its default value after output.
    /// </summary>
    /// <param name="color">The text color from the <see cref="ConsoleColor"/> enumeration.</param>
    /// <param name="message">The string to write.</param>
    /// <example>
    /// <code>
    /// ConsoleEx.WriteLineColor(ConsoleColor.Yellow, "Warning: operation complete.");
    /// </code>
    /// </example>
    public static void WriteLineColor(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"{message}");
        Console.ResetColor();
    }

    /// <summary>
    /// Writes a string to the console without a trailing newline, using the specified text color.
    /// The console color is reset to its default value after output.
    /// </summary>
    /// <param name="color">The text color from the <see cref="ConsoleColor"/> enumeration.</param>
    /// <param name="message">The string to write.</param>
    /// <example>
    /// <code>
    /// ConsoleEx.WriteColor(ConsoleColor.Cyan, "Status: ");
    /// Console.WriteLine("OK");
    /// </code>
    /// </example>
    public static void WriteColor(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write($"{message}");
        Console.ResetColor();
    }
}

/// <summary>
/// Provides static helper methods for creating and populating one-dimensional
/// and two-dimensional arrays, either with random values or with data read from the console.
/// </summary>
public static class ArrayHelper
{
    /// <summary>
    /// Generates a one-dimensional integer array filled with random values
    /// within the specified range.
    /// </summary>
    /// <param name="count">The number of elements in the array.</param>
    /// <param name="minValue">The inclusive lower bound of the random range.</param>
    /// <param name="maxValue">The exclusive upper bound of the random range.</param>
    /// <param name="seed">
    /// An optional seed for the random number generator.
    /// When specified, the output is deterministic and reproducible.
    /// When <c>null</c>, the system clock is used as the seed.
    /// </param>
    /// <returns>An <see cref="int"/> array of length <paramref name="count"/>.</returns>
    /// <example>
    /// <code>
    /// // Reproducible result with a fixed seed
    /// int[] arr = ArrayHelper.Generate1DArray(10, 0, 100, seed: 42);
    ///
    /// // Non-deterministic result without a seed
    /// int[] arr2 = ArrayHelper.Generate1DArray(5, -10, 10);
    /// </code>
    /// </example>
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

    /// <summary>
    /// Generates a two-dimensional integer array filled with random values
    /// within the specified range.
    /// </summary>
    /// <param name="rows">The number of rows in the matrix.</param>
    /// <param name="cols">The number of columns in the matrix.</param>
    /// <param name="minValue">The inclusive lower bound of the random range.</param>
    /// <param name="maxValue">The exclusive upper bound of the random range.</param>
    /// <param name="seed">
    /// An optional seed for the random number generator.
    /// When specified, the output is deterministic and reproducible.
    /// When <c>null</c>, the system clock is used as the seed.
    /// </param>
    /// <returns>
    /// A two-dimensional <see cref="int"/> array of size
    /// <paramref name="rows"/> × <paramref name="cols"/>.
    /// </returns>
    /// <example>
    /// <code>
    /// int[,] matrix = ArrayHelper.Generate2DArray(3, 4, 0, 50, seed: 7);
    /// </code>
    /// </example>
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

    /// <summary>
    /// Creates a one-dimensional integer array by reading each element from the console.
    /// Uses <see cref="ConsoleEx.ReadValue{T}"/> to validate input.
    /// </summary>
    /// <param name="count">The number of elements to read.</param>
    /// <returns>
    /// An <see cref="int"/> array of length <paramref name="count"/>
    /// populated with values entered by the user.
    /// </returns>
    /// <example>
    /// <code>
    /// int[] userArray = ArrayHelper.Create1DIntArray(5);
    /// </code>
    /// </example>
    public static int[] Create1DIntArray(int count)
    {
        int[] array = new int[count];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            int number = ConsoleEx.ReadValue<int>($"Enter the {i + 1}th element of the array: \t");
            array[i] = number;
        }
        return array;
    }

    /// <summary>
    /// Creates a one-dimensional string array by reading each element from the console.
    /// Uses <see cref="ConsoleEx.ReadString"/> to validate input,
    /// so empty or whitespace-only strings are not accepted.
    /// </summary>
    /// <param name="count">The number of strings to read.</param>
    /// <returns>
    /// A <see cref="string"/> array of length <paramref name="count"/>
    /// populated with values entered by the user.
    /// </returns>
    /// <example>
    /// <code>
    /// string[] names = ArrayHelper.Create1DStringArray(3);
    /// </code>
    /// </example>
    public static string[] Create1DStringArray(int count)
    {
        string[] array = new string[count];

        for (int i = 0; i < array.GetLength(0); i++)
        {
            string elements = ConsoleEx.ReadString($"Enter the {i + 1}th element of the array: \t");
            array[i] = elements;
        }
        return array;
    }
}

/// <summary>
/// Provides extension methods for arrays: printing one-dimensional and two-dimensional arrays
/// to the console, and filling an array either from console input or with random values.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Prints each element of a one-dimensional array on a separate line
    /// in the format <c>index: value</c>.
    /// If an element is <c>null</c>, the string <c>[null]</c> is printed instead.
    /// </summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The array to print.</param>
    /// <example>
    /// <code>
    /// int[] arr = { 10, 20, 30 };
    /// arr.Print();
    /// // 0: 10
    /// // 1: 20
    /// // 2: 30
    /// </code>
    /// </example>
    public static void Print<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string? value = array[i] is not null ? array[i]?.ToString() : "[null]";
            Console.WriteLine($"{i}: {value}");
        }
    }

    /// <summary>
    /// Prints all elements of a one-dimensional array on a single line, separated by spaces.
    /// If an element is <c>null</c>, the string <c>[null]</c> is printed instead.
    /// A newline is written after the last element.
    /// </summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The array to print.</param>
    /// <example>
    /// <code>
    /// int[] arr = { 1, 2, 3 };
    /// arr.PrintOneLine();
    /// // 1 2 3
    /// </code>
    /// </example>
    public static void PrintOneLine<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            string? value = array[i] is not null ? array[i]?.ToString() : "[null]";
            Console.Write($"{value} ");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Prints a two-dimensional array as a matrix: each row on a separate console line,
    /// with elements separated by spaces.
    /// </summary>
    /// <typeparam name="T">The type of the array elements.</typeparam>
    /// <param name="array">The two-dimensional array to print.</param>
    /// <example>
    /// <code>
    /// int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };
    /// matrix.Print2DMatrix();
    /// // 1 2 3
    /// // 4 5 6
    /// </code>
    /// </example>
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

    /// <summary>
    /// Fills an existing one-dimensional integer array with values read from the console.
    /// Uses <see cref="ConsoleEx.ReadValue{T}"/> for input validation.
    /// The array is modified in place.
    /// </summary>
    /// <param name="array">The array to fill.</param>
    /// <example>
    /// <code>
    /// int[] arr = new int[4];
    /// arr.FillFromConsole();
    /// </code>
    /// </example>
    public static void FillFromConsole(this int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = ConsoleEx.ReadValue<int>($"Enter the {i + 1}th element of the array:");
        }
    }

    /// <summary>
    /// Fills an existing one-dimensional integer array with random values
    /// within the specified range. The array is modified in place.
    /// </summary>
    /// <param name="array">The array to fill.</param>
    /// <param name="minValue">The inclusive lower bound of the random range.</param>
    /// <param name="maxValue">The exclusive upper bound of the random range.</param>
    /// <param name="seed">
    /// An optional seed for the random number generator.
    /// When specified, the output is deterministic and reproducible.
    /// When <c>null</c>, the system clock is used as the seed.
    /// </param>
    /// <example>
    /// <code>
    /// int[] arr = new int[5];
    /// arr.FillRandom(1, 100);
    ///
    /// // Reproducible result:
    /// arr.FillRandom(0, 50, seed: 123);
    /// </code>
    /// </example>
    public static void FillRandom(this int[] array, int minValue, int maxValue, int? seed = null)
    {
        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(minValue, maxValue);
        }
    }
}