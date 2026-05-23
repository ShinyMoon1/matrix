using System;

public class Matrix
{
    private int[,] _data;
    public int Rows { get; }
    public int Cols { get; }

    public Matrix(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        _data = new int[rows, cols];
        Random rnd = new Random();
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                _data[i, j] = rnd.Next(1, 100);
    }

    public Matrix(Matrix other)
    {
        Rows = other.Rows;
        Cols = other.Cols;
        _data = new int[Rows, Cols];
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                _data[i, j] = other._data[i, j];
    }

    public Matrix Transpose()
    {
        Matrix result = new Matrix(Cols, Rows);
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                result._data[j, i] = _data[i, j];
        return result;
    }

    public Matrix Rotate90()
    {
        Matrix result = new Matrix(Cols, Rows);
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols; j++)
                result._data[i, j] = _data[Cols - 1 - j, i];
        return result;
    }

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

    public void ReverseColumnsOrder()
    {
        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Cols / 2; j++)
            {
                int temp = _data[i, j];
                _data[i, j] = _data[i, Cols - 1 - j];
                _data[i, Cols - 1 - j] = temp;
            }
    }

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

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Программа для работы с матрицей ===\n");

        Console.Write("Введите количество строк: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов: ");
        int cols = int.Parse(Console.ReadLine());

        Matrix matrix = new Matrix(rows, cols);
        matrix.Print("Исходная матрица:");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Транспонирование");
            Console.WriteLine("2 - Поворот на 90°");
            Console.WriteLine("3 - Сортировка строк по возрастанию");
            Console.WriteLine("4 - Реверс столбцов");
            Console.WriteLine("0 - Выход");
            Console.Write("Ваш выбор: ");

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
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }
}