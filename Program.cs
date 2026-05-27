using System;
using System.Globalization;
using System.Text;

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("Лабораторна робота №18");
        Console.WriteLine("Тема: Масиви в C#");
        Console.WriteLine("Варіант 19");
        Console.WriteLine();

        WorkWithOneDimensionalArray();

        Console.WriteLine();
        Console.WriteLine(new string('-', 60));
        Console.WriteLine();

        WorkWithTwoDimensionalArray();

        Console.WriteLine();
        Console.WriteLine("Програму завершено.");
    }

    static void WorkWithOneDimensionalArray()
    {
        Console.WriteLine("1. Одновимірний масив");

        int n = ReadInt("Введіть кількість елементів масиву n: ", 1);

        double[] array = new double[n];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = ReadDouble($"array[{i + 1}] = ");
        }

        PrintArray("Початковий масив:", array);

        double sumOddNumbers = 0;

        for (int i = 0; i < array.Length; i++)
        {
            int elementNumber = i + 1;

            if (elementNumber % 2 != 0)
            {
                sumOddNumbers += array[i];
            }
        }

        Console.WriteLine($"Сума елементів масиву з непарними номерами: {FormatNumber(sumOddNumbers)}");

        int firstNegativeIndex = -1;
        int lastNegativeIndex = -1;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < 0)
            {
                if (firstNegativeIndex == -1)
                {
                    firstNegativeIndex = i;
                }

                lastNegativeIndex = i;
            }
        }

        double sumBetweenFirstAndLastNegative = 0;

        if (firstNegativeIndex != -1 && lastNegativeIndex != -1 && firstNegativeIndex != lastNegativeIndex)
        {
            for (int i = firstNegativeIndex + 1; i < lastNegativeIndex; i++)
            {
                sumBetweenFirstAndLastNegative += array[i];
            }

            Console.WriteLine(
                $"Сума елементів між першим і останнім від'ємними елементами: " +
                $"{FormatNumber(sumBetweenFirstAndLastNegative)}"
            );
        }
        else
        {
            Console.WriteLine("У масиві немає двох від'ємних елементів. Сума між ними дорівнює 0.");
        }

        double[] compressedArray = CompressArray(array);

        PrintArray(
            "Стиснутий масив, де видалено елементи з |x| <= 1, а кінець заповнено нулями:",
            compressedArray
        );
    }

    static double[] CompressArray(double[] source)
    {
        double[] result = new double[source.Length];
        int writeIndex = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (Math.Abs(source[i]) > 1)
            {
                result[writeIndex] = source[i];
                writeIndex++;
            }
        }

        return result;
    }

    static void WorkWithTwoDimensionalArray()
    {
        Console.WriteLine("2. Двовимірний масив");

        int rows = ReadInt("Введіть кількість рядків: ", 1);
        int columns = ReadInt("Введіть кількість стовпців: ", 1);

        double[,] matrix = new double[rows, columns];

        Console.WriteLine("Введіть елементи двовимірного масиву:");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = ReadDouble($"matrix[{i + 1},{j + 1}] = ");
            }
        }

        PrintMatrix("Початковий двовимірний масив:", matrix);

        double topRightElement = matrix[0, columns - 1];

        Console.WriteLine();
        Console.WriteLine("2а. Порівняння за абсолютною величиною");
        Console.WriteLine($"Елемент у верхньому правому куті: matrix[1,{columns}] = {FormatNumber(topRightElement)}");

        Console.WriteLine("Введіть координати іншого елемента для порівняння з верхнім правим кутом.");

        int otherRow = ReadIndex("Рядок іншого елемента", rows);
        int otherColumn = ReadIndex("Стовпець іншого елемента", columns);

        double otherElement = matrix[otherRow, otherColumn];

        Console.WriteLine(
            $"Інший елемент: matrix[{otherRow + 1},{otherColumn + 1}] = {FormatNumber(otherElement)}"
        );

        CompareByAbsoluteValue(
            topRightElement,
            $"|matrix[1,{columns}]|",
            otherElement,
            $"|matrix[{otherRow + 1},{otherColumn + 1}]|"
        );

        Console.WriteLine();
        Console.WriteLine("2б. Порівняння двох будь-яких елементів масиву");
        Console.WriteLine("Введіть координати першого елемента.");

        int firstRow = ReadIndex("Рядок першого елемента", rows);
        int firstColumn = ReadIndex("Стовпець першого елемента", columns);

        Console.WriteLine("Введіть координати другого елемента.");

        int secondRow = ReadIndex("Рядок другого елемента", rows);
        int secondColumn = ReadIndex("Стовпець другого елемента", columns);

        double firstElement = matrix[firstRow, firstColumn];
        double secondElement = matrix[secondRow, secondColumn];

        Console.WriteLine(
            $"Перший елемент: matrix[{firstRow + 1},{firstColumn + 1}] = {FormatNumber(firstElement)}"
        );

        Console.WriteLine(
            $"Другий елемент: matrix[{secondRow + 1},{secondColumn + 1}] = {FormatNumber(secondElement)}"
        );

        if (firstElement < secondElement)
        {
            Console.WriteLine("Перший елемент менший.");
        }
        else if (secondElement < firstElement)
        {
            Console.WriteLine("Другий елемент менший.");
        }
        else
        {
            Console.WriteLine("Елементи рівні.");
        }
    }

    static void CompareByAbsoluteValue(double firstValue, string firstName, double secondValue, string secondName)
    {
        double firstAbs = Math.Abs(firstValue);
        double secondAbs = Math.Abs(secondValue);

        Console.WriteLine($"{firstName} = {FormatNumber(firstAbs)}");
        Console.WriteLine($"{secondName} = {FormatNumber(secondAbs)}");

        if (firstAbs > secondAbs)
        {
            Console.WriteLine($"Більша абсолютна величина: {firstName}");
        }
        else if (secondAbs > firstAbs)
        {
            Console.WriteLine($"Більша абсолютна величина: {secondName}");
        }
        else
        {
            Console.WriteLine("Абсолютні величини рівні.");
        }
    }

    static int ReadInt(string message, int minValue)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= minValue)
            {
                return value;
            }

            Console.WriteLine($"Помилка. Введіть ціле число не менше {minValue}.");
        }
    }

    static int ReadIndex(string message, int maxValue)
    {
        while (true)
        {
            Console.Write($"{message} від 1 до {maxValue}: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= 1 && value <= maxValue)
            {
                return value - 1;
            }

            Console.WriteLine($"Помилка. Введіть число від 1 до {maxValue}.");
        }
    }

    static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input != null)
            {
                input = input.Trim().Replace(',', '.');

                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    return value;
                }
            }

            Console.WriteLine("Помилка. Введіть дійсне число. Наприклад: 2.5 або 2,5");
        }
    }

    static void PrintArray(string title, double[] array)
    {
        Console.WriteLine();
        Console.WriteLine(title);

        for (int i = 0; i < array.Length; i++)
        {
            Console.Write($"{FormatNumber(array[i])}\t");
        }

        Console.WriteLine();
    }

    static void PrintMatrix(string title, double[,] matrix)
    {
        Console.WriteLine();
        Console.WriteLine(title);

        int rows = matrix.GetLength(0);
        int columns = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"{FormatNumber(matrix[i, j])}\t");
            }

            Console.WriteLine();
        }
    }

    static string FormatNumber(double value)
    {
        return value.ToString("0.###", CultureInfo.InvariantCulture);
    }
}
