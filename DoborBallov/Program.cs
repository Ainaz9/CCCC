
namespace Dobor
{
    // 3
    public static class ArrayExtensions
    {
        public static int[] Square(this int[] array)
        {
            int[] result = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i] * array[i];
            }

            return result;
        }

        public static int[] Cube(this int[] array)
        {
            int[] result = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[i] * array[i] * array[i];
            }

            return result;
        }

        public static double Average(this int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length ; i++)
            {
                sum += array[i];
            }

            return (double)sum / array.Length;
        }

        public static int Even(this int[] array)
        {
            return array.Where(x => (x % 2 == 0)).Sum();
        }

        public static int Odd(this int[] array)
        {
            return array.Where(x => (x % 2 == 1)).Sum();
        }
    }
    class Program
    {
        // #1

        static void Rhombus(int dimension)
        {
            if (dimension % 2 == 0 || dimension < 0)
            {
                Console.WriteLine("Размерность должна быть нечетная и больше нуля");
                return;
            }
            int i = 1;
            int k = 0;
            int l = dimension / 2;
            while (i <= dimension)
            {
                if ((i == 1) || (i == dimension))
                {
                    for (int j = 0; j < (dimension / 2); j++)
                        Console.Write(" ");

                    Console.WriteLine("*");
                    k++;
                    l--;
                }
                else if (i < ((dimension / 2) + 2))
                {
                    for (int c = 0; c < l; c++)
                        Console.Write(" ");

                    for (int j = 0; j < i + k; j++)
                    {
                        Console.Write("*");
                    }
                    k++;
                    l--;
                    if (i == (dimension / 2) + 1)
                    {
                        k -= 4;
                        l = 1;
                    }
                    Console.WriteLine();
                }
                else
                {
                    
                    for (int c = 0; c < l; c++)
                        Console.Write(" ");
                    for (int j = 0;j < i + k; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                    k -= 3;
                    l++;
                }
                i++;
            }
        }

        // 2

        static string[] Sort(string[] arrayString)
        {
            string[] result = arrayString.OrderBy(x => x.Length).ToArray();

            return result;
        }

        // 4

        static int countSmileys(string[] arrayString)
        {
            return arrayString.Count(x => x == ":-)" || x == ":-D" || x == ":~)" || x == ":~D" || x == ":)" || x == ":D" || x == ";-)" || x == ";-D" || x == ";~)" || x == ";~D" || x == ";)" || x == ";D");
        }

        // 5

        static int? Zero(int[] array)
        {
            int value = array.Max();
            if (array.Where(x => x == 0).Count() > 0)
            {
                return 0;
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (Math.Abs(array[i]) < Math.Abs(value))
                {
                    value = array[i];
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (value == array[i] * -1)
                {
                    return null;
                }
            }

            return value;
        }

        static void Main(string[] args)
        {
            // 1
            Rhombus(5);
            Console.WriteLine();

            // 2 
            string[] result = Sort(["22","1","4444","55555","333","7777777","333","666666"]);
            foreach (string s in result) Console.Write(s + " ");
            Console.WriteLine();

            // 3
            int[] numbers = { 1, 2, 3, 4, 5 };

            int[] squaredNumbers = numbers.Square(); 
            Console.WriteLine(string.Join(", ", squaredNumbers)); 

            int[] cubeNumbers = numbers.Cube();
            Console.WriteLine(string.Join(", ", cubeNumbers));

            double average = numbers.Average();
            Console.WriteLine(average);

            int sum = numbers.Sum();
            Console.WriteLine(sum);

            int sumEven = numbers.Even();
            Console.WriteLine(sumEven);

            int sumOdd = numbers.Odd();
            Console.WriteLine(sumOdd);

            // 5

            int? a1 = Zero([2, 4, -1, -3]);
            int? a2 = Zero([5, 2, -2]);
            int? a3 = Zero([5, 2, 2]);
            int? a4 = Zero([13, 0, -6]);

            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3);
            Console.WriteLine(a4);
        }
    }
}
