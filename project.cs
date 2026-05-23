using System;

/// <summary>
/// Класс, представляющий матрицу целых чисел.
/// Содержит методы преобразований: транспонирование, поворот, сортировка строк, реверс столбцов.
/// </summary>
public class Matrix
{
    private int[,] _data;
    
    /// <summary>
    /// Возвращает количество строк матрицы.
    /// </summary>
    public int Rows { get; }
    
    /// <summary>
    /// Возвращает количество столбцов матрицы.
    /// </summary>
    public int Cols { get; }

    /// <summary>
    /// Конструктор. Создает матрицу заданного размера и заполняет случайными числами от 1 до 99.
    /// </summary>
    /// <param name="rows">Количество строк (должно быть положительным).</param>
    /// <param name="cols">Количество столбцов (должно быть положительным).</param>
    /// <exception cref="ArgumentException">Выбрасывается, если rows или cols меньше или равны 0.</exception>
    public Matrix(int rows, int cols)
    {
        if (rows <= 0 || cols <= 0)
            throw new ArgumentException("Размеры матрицы должны быть положительными числами.");
        
        Rows = rows;
        Cols = cols;
        _data = new int[rows, cols];
        Random rnd = new Random();
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                _data[i, j] = rnd.Next(1, 100);
    }

    /// <summary>
    /// Конструктор копирования. Создает глубокую копию существующей матрицы.
    /// </summary>
    /// <param name="other">Исходная матрица для копирования.</param>
    /// <exception cref="ArgumentNullException">Выбрасывается, если other равен null.</exception>
    public Matrix(Matrix other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        
        Rows = other.Rows;
        Cols = other.Cols;
        _data = new int[Rows, Cols];
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                _data[i, j] = other._data[i, j];
    }

    /// <summary>
    /// Транспонирование матрицы.
    /// </summary>
    /// <returns>Новая транспонированная матрица.</returns>
    public Matrix Transpose()
    {
        Matrix result = new Matrix(Cols, Rows);
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                result._data[j, i] = _data[i, j];
        return result;
    }

    /// <summary>
    /// Поворот матрицы на 90 градусов по часовой стрелке.
    /// </summary>
    /// <returns>Новая повернутая матрица.</returns>
    public Matrix Rotate90()
    {
        Matrix result = new Matrix(Cols, Rows);
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                result._data[j, Rows - 1 - i] = _data[i, j];
        return result;
    }

    /// <summary>
    /// Сортировка каждой строки матрицы по возрастанию.
    /// Внимание: метод изменяет текущую матрицу.
    /// </summary>
    public void SortRowsAscending()
    {
        for (int i = 0; i < Rows; i++)
        {
            int[] row = new int[Cols];
            for (int j = 0; j < Cols; j++)
                row[j] = _data[i, j];
            Array.Sort(row);
            for (int j = 0; j < Cols; j++)
                _data[i, j] = row[j];
        }
    }

    /// <summary>
    /// Изменение порядка столбцов на обратный (реверс столбцов).
    /// Внимание: метод изменяет текущую матрицу.
    /// </summary>
    public void ReverseColumnsOrder()
    {
        if (Cols == 1) return;
        
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols / 2; j++)
            {
                int temp = _data[i, j];
                _data[i, j] = _data[i, Cols - 1 - j];
                _data[i, Cols - 1 - j] = temp;
            }
    }

    /// <summary>
    /// Вывод матрицы в консоль.
    /// </summary>
    /// <param name="title">Заголовок перед выводом матрицы.</param>
    public void Print(string title = "Текущая матрица:")
    {
        Console.WriteLine(title);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                Console.Write($"{_data[i, j],4} ");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Преобразование матрицы в строку.
    /// </summary>
    /// <returns>Строковое представление матрицы.</returns>
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                result += $"{_data[i, j],4} ";
            result += Environment.NewLine;
        }
        return result;
    }
}

/// <summary>
/// Главный класс программы с диалоговым режимом работы с матрицей.
/// </summary>
class Program
{
    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Программа для работы с матрицей ===\n");

        int rows = ReadPositiveInt("Введите количество строк: ");
        int cols = ReadPositiveInt("Введите количество столбцов: ");

        Matrix matrix = new Matrix(rows, cols);
        matrix.Print("Исходная матрица:");

        bool exit = false;
        while (!exit)
        {
            ShowMenu();
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    matrix = matrix.Transpose();
                    matrix.Print("Результат транспонирования:");
                    break;
                case "2":
                    matrix = matrix.Rotate90();
                    matrix.Print("Результат поворота:");
                    break;
                case "3":
                    matrix.SortRowsAscending();
                    matrix.Print("Результат сортировки строк:");
                    break;
                case "4":
                    matrix.ReverseColumnsOrder();
                    matrix.Print("Результат реверса столбцов:");
                    break;
                case "0":
                    exit = true;
                    Console.WriteLine("Работа завершена.");
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Пожалуйста, выберите пункт от 0 до 4.");
                    break;
            }
        }
    }

    /// <summary>
    /// Отображает меню выбора операций.
    /// </summary>
    static void ShowMenu()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║           ВЫБЕРИТЕ ДЕЙСТВИЕ:                       ║");
        Console.WriteLine("╠════════════════════════════════════════════════════╣");
        Console.WriteLine("║  1 - Транспонирование матрицы                      ║");
        Console.WriteLine("║  2 - Поворот на 90° по часовой стрелке             ║");
        Console.WriteLine("║  3 - Сортировка строк по возрастанию               ║");
        Console.WriteLine("║  4 - Изменить порядок столбцов на обратный         ║");
        Console.WriteLine("║  0 - Завершить работу                              ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.Write("Ваш выбор: ");
    }

    /// <summary>
    /// Безопасное чтение целого положительного числа из консоли.
    /// </summary>
    /// <param name="prompt">Приглашение для ввода.</param>
    /// <returns>Введенное положительное целое число.</returns>
    static int ReadPositiveInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Ошибка: введите положительное целое число (больше 0).");
        }
    }
}