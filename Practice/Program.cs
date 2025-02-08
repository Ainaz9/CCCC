
using System.Diagnostics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Practice
{
    class Program
    {
        class Matrix
        {
            private int[,] Data;

            public Matrix(int rows, int cols)
            {
                Data = new int[rows, cols];
                Random random = new Random();
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0;  j < cols; j++)
                    {
                        Data[i, j] = random.Next(1, 10);
                    }
                }
            }

            public static Matrix operator + (Matrix x, Matrix y)
            {
                if ((x.Cols != y.Cols) || (x.Rows != y.Rows))
                {
                    throw new InvalidOperationException("Матрицы должны иметь одинаковые размеры для сложения.");
                }

                Matrix result = new Matrix(x.Rows, y.Cols);
                for (int i = 0;i < x.Rows;i++)
                {
                    for (int j = 0;j < y.Cols;j++)
                    {
                        result.Data[i, j] = x.Data[i, j] + y.Data[i, j];
                    }
                }

                return result;
            }

            public static Matrix operator * (Matrix x, int value)
            {
                Matrix result = new Matrix(x.Rows, x.Cols);

                for(int i = 0; i < x.Rows;i++)
                {
                    for (int j = 0;j < x.Cols;j++)
                    {
                        result.Data[i,j] = x.Data[i,j] * value;
                    }
                }

                return result;
            }

            public Matrix Transpose()
            {
                Matrix result = new Matrix(Rows, Cols);

                for ( int i = 0; i < Rows;i++)
                {
                    for (int j = 0;  j < Cols;j++)
                    {
                        result.Data[i,j] = Data[j,i];
                    }
                }

                return result;
            }

            public void Print()
            {
                for (int i = 0;i < Rows;i++)
                {
                    for (int j = 0; j <= Cols;j++)
                    {
                        Console.Write(Data[i,j] + "\t");
                    }
                    Console.WriteLine();
                }
            }

            public int Rows => Data.GetLength(0);
            public int Cols => Data.GetLength(1);
        }
        static void Main(string[] args)
        {
            // Создаем матрицы 3x3
            Matrix matrix1 = new Matrix(3, 3);
            Matrix matrix2 = new Matrix(3, 3);

            Console.WriteLine("Матрица 1:");
            matrix1.Print();
            Console.WriteLine();

            Console.WriteLine("Матрица 2:");
            matrix2.Print();
            Console.WriteLine();

            // Сложение матриц
            try
            {
                Matrix sum = matrix1 + matrix2;
                Console.WriteLine("Результат сложения:");
                sum.Print();
                Console.WriteLine();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Умножение матрицы на число
            Matrix multiplied = matrix1 * 5;
            Console.WriteLine("Матрица 1, умноженная на 5:");
            multiplied.Print();
            Console.WriteLine();

            // Транспонирование матрицы
            Matrix transposed = matrix1.Transpose();
            Console.WriteLine("Транспонированная матрица 1:");
            transposed.Print();
        }
    }
}
