
namespace KR
{
    class Program
    {
        #region #2 Ассоциация
        class Engine
        { 
            public void Start() => Console.WriteLine("Двигатель запущен.");
        }
        class Car
        {
            public Engine Engine { get; set; }  // Ассоциация с классом Engine

            public Car(Engine engine)
            {
                Engine = engine;
            }

            public void StartCar()
            {
                Engine.Start();
                Console.WriteLine("Машина едет.");
            }
        }

        #endregion
        #region #5 Наследование

        // Родительский (базовый) класс
        class Animal
        {
            public string Name { get; set; }

            public void Eat()
            {
                Console.WriteLine($"{Name} ест.");
            }
        }

        // Дочерний (производный) класс
        class Dog : Animal
        {
            public void Bark()
            {
                Console.WriteLine($"{Name} лает: Гав-гав!");
            }
        }

        #endregion
        #region #7 Методы. Параметры. Ref, out, Params. Пример

        // Обычный метод
        static void PrintSum(int a, int b) 
        {
            int sum = a + b;
            Console.WriteLine($"Сумма: {sum}");
        }

        // Ref
        static void Increase(ref int number) 
        {
            number += 10;
        }

        // Out
        static void SplitNumber(double num, out int integerPart, out double fractionalPart) 
        {
            integerPart = (int)num;
            fractionalPart = num - integerPart;
        }

        // Params
        static int Sum(params int[] numbers) 
        {
            int sum = 0;
            foreach (int num in numbers)
                sum += num;
            return sum;
        }

        #endregion
        #region #10 Инкапсуляция. Модификаторы доступа. Примеры

        //Пример инкапсуляции с private и public
        class Person
        {
            private string name; // Приватное поле

            public void SetName(string newName) // Публичный метод для установки
            {
                if (!string.IsNullOrWhiteSpace(newName))
                    name = newName;
            }

            public string GetName() // Публичный метод для получения
            {
                return name;
            }
        }

        // Инкапсуляция с private и public через свойства (свойство get и set)

        class Person1
        {
            private string name; // Приватное поле

            public string Name
            {
                get { return name; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        name = value;
                }
            }
        }

        // Автоматическое свойство

        class Person2
        {
            public string Name { get; set; } // Автосвойство
        }

        // Protected - доступ для наследников

        class Animal1
        {
            protected string species;

            public void SetSpecies(string species)
            {
                this.species = species;
            }
        }

        class Dog1 : Animal1
        {
            public void PrintSpecies()
            {
                Console.WriteLine($"Я – {species}");
            }
        }

        // internal – доступ только в этой сборке

        internal class InternalClass
        {
            public void SayHello()
            {
                Console.WriteLine("Привет из internal-класса!");
            }
        }


        #endregion
        #region #11 Полиморфизм. Перегрузка и переопределение. Пример

        // перегрузка метода Print
        class Printer
        {
            public void Print(string message)
            {
                Console.WriteLine("Сообщение: " + message);
            }

            public void Print(int number)
            {
                Console.WriteLine("Число: " + number);
            }

            public void Print(string message, int count)
            {
                for (int i = 0; i < count; i++)
                    Console.WriteLine(message);
            }
        }

        // переопределение метода MakeSound

        class Animal2
        {
            public virtual void MakeSound()
            {
                Console.WriteLine("Животное издает звук");
            }
        }

        class Dog2 : Animal2
        {
            public override void MakeSound()
            {
                Console.WriteLine("Собака лает: Гав-гав!");
            }
        }

        // Абстрактный класс и полиморфизм

        abstract class Animal3
        {
            public abstract void MakeSound();
        }

        class Cat3 : Animal3
        {
            public override void MakeSound()
            {
                Console.WriteLine("Кошка мяукает: Мяу-мяу!");
            }
        }

        // Полиморфизм через интерфейсы

        interface IShape
        {
            void Draw();
        }

        class Circle : IShape
        {
            public void Draw() => Console.WriteLine("Рисуем круг");
        }

        class Square : IShape
        {
            public void Draw() => Console.WriteLine("Рисуем квадрат");
        }

        #endregion
        static void Main(string[] args)
        {
            #region #1 Условные операторы

            int value1 = 6;

            // if else
            if (value1 > 5) 
            {
                Console.WriteLine("Число больше 5");
            }
            else
            {
                Console.WriteLine("Число меньше или равно 5");
            }

            // Тернарный оператор
            Console.WriteLine(value1 > 5 ? "Число больше 5" : "Число меньше или равно 5"); 

            // switch case
            switch (value1)
            {
                case 1:
                    Console.WriteLine("1");
                    break;
                case 2:
                    Console.WriteLine("2");
                    break;
                case 3:
                    Console.WriteLine("3");
                    break;
                case 4:
                    Console.WriteLine("4");
                    break;
                case 5:
                    Console.WriteLine("5");
                    break;
                case 6:
                    Console.WriteLine("6");
                    break;
                case 7:
                    Console.WriteLine("7");
                    break;
                case 8:
                    Console.WriteLine("8");
                    break;
                case 9:
                    Console.WriteLine("9");
                    break;
                case 10:
                    Console.WriteLine("10");
                    break;
                default:
                    Console.WriteLine("Число больше 10 или меньше 1");
                    break;
            }

            #endregion
            #region #3 Упаковка и Распаковка

            // Упаковка (Boxing)
            int number = 42;  // Значимый тип (Value Type)
            object boxedNumber = number; 
            Console.WriteLine(boxedNumber); // 42

            // Распаковка (Unboxing)
            object boxedValue = 100; // Упаковка (Boxing)
            int unboxedValue = (int)boxedValue; 
            Console.WriteLine(unboxedValue);

            #endregion
            #region #4 Операторы IS и AS, явное и неявное приведение 

            // Оператор is
            object obj = "Привет, мир!"; 
            if (obj is string) 
            {
                Console.WriteLine("Объект является строкой.");
            }
            else
            {
                Console.WriteLine("Объект не является строкой.");
            }

            // Оператор as
            object obj1 = "Hello";
            string? str = obj1 as string; 
            if (str != null)
            {
                Console.WriteLine($"Строка: {str}");
            }
            else
            {
                Console.WriteLine("Приведение не удалось.");
            }

            // Явное приведение
            double d = 9.7;
            int i = (int)d; //  (усечение дробной части)
            Console.WriteLine(i); // 9

            // Неявное приведение
            int a = 10;
            double b = a; // int → double
            Console.WriteLine(b); // 10.0

            #endregion
            #region #6 Операторы цикла

            // for
            for (i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                if (i == 3)
                {
                    break;
                }
            }

            // while
            i = 0;
            while (i < 5)
            {
                Console.WriteLine(i);
                i++;

                if (i == 3)
                {
                    continue;
                }
            }
            
            // do while
            i = 0;
            do
            {
                Console.WriteLine(i);
            } while (i < 0);

            // foreach
            string[] fruits = { "Яблоко", "Банан", "Апельсин" };
            foreach (string fruit in fruits)
            {
                Console.WriteLine(fruit);
            }

            #endregion
            #region #7 Методы. Параметры. Ref, out, Params. Пример

            // Метод
            PrintSum(5, 7); // Выведет: Сумма: 12

            // Ref
            int value = 5;
            Increase(ref value);
            Console.WriteLine(value); // Выведет: 15
            
            // Out
            double number1 = 7.75;
            SplitNumber(number1, out int intPart, out double fracPart);
            Console.WriteLine($"Целая часть: {intPart}, Дробная часть: {fracPart}");

            // params
            Console.WriteLine(Sum(1, 2, 3, 4, 5)); // 15
            Console.WriteLine(Sum(10, 20)); // 30

            #endregion
            #region #8 Массивы, типы массивов. Примеры

            // Одномерный массив
            int[] numbers = { 1, 2, 3, 4, 5 }; 
            Console.WriteLine(numbers[0]); // Выведет: 1
            numbers[2] = 10; // Изменяем значение
            Console.WriteLine(numbers[2]); // Выведет: 10

            // Инициализация массива
            int[] arr1 = new int[3]; // По умолчанию { 0, 0, 0 } 
            int[] arr2 = new int[] { 1, 2, 3 };
            int[] arr3 = { 4, 5, 6 };

            // Двумерный массив
            int[,] matrix = 
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            };
            Console.WriteLine(matrix[1, 2]); // Выведет: 6
            
            //  Ступенчатый массив
            int[][] jaggedArray =  
            {
                new int[] { 1, 2 },
                new int[] { 3, 4, 5 }
            };
            Console.WriteLine(jaggedArray[1][2]); // Выведет: 5

            // Цикл foreach   Перебор массива
            foreach (int num in numbers)
                Console.Write(num + " ");

            // Основные свойства массива
            Console.WriteLine(numbers.Length); // 5 (длина массива) 
            Console.WriteLine(numbers.Rank);   // 1 (размерность)
            Console.WriteLine(numbers[0]);     // 1 (первый элемент)

            Array.Sort(numbers);  // Сортировка {1, 3, 4, 5, 8}
            Array.Reverse(numbers); // {8, 5, 4, 3, 1}

            #endregion
            #region #9 Коллекции. Типы коллекций. Примеры.

            // List<T> – динамический список

            List<int> numbers1 = new List<int> { 1, 2, 3 }; // List
            numbers1.Add(4); // Добавляем элемент
            numbers1.Remove(2); // Удаляем элемент
            numbers1.Insert(1, 10); // Вставляем 10 на позицию 1

            foreach (var num in numbers1)
                Console.Write(num + " "); // Вывод: 1 10 3 4

            // Dictionary<TKey, TValue>

            Dictionary<string, int> ages = new Dictionary<string, int> 
            {
                { "Алиса", 25 },
                { "Боб", 30 }
            };

            ages["Чарли"] = 28; // Добавление нового ключа
            Console.WriteLine(ages["Алиса"]); // Вывод: 25

            // Очередь

            Queue<string> queue = new Queue<string>();

            queue.Enqueue("Первый");
            queue.Enqueue("Второй");

            Console.WriteLine(queue.Dequeue()); // Вывод: Первый
            Console.WriteLine(queue.Peek()); // Вывод: Второй (не удаляется)

            // Stack<T> – стек

            Stack<string> stack = new Stack<string>();

            stack.Push("Первый");
            stack.Push("Второй");

            Console.WriteLine(stack.Pop()); // Вывод: Второй (удаляется)
            Console.WriteLine(stack.Peek()); // Вывод: Первый (не удаляется)

            // HashSet<T> – коллекция уникальных элементов

            HashSet<int> numbers2 = new HashSet<int> { 1, 2, 3, 3, 4 };
            numbers2.Add(5);

            foreach (var num in numbers)
                Console.Write(num + " "); // Вывод: 1 2 3 4 5
            #endregion
            #region #10 Инкапсуляция. Модификаторы доступа. Примеры

            // Пример инкапсуляции с private и public

            Person person = new Person();
            person.SetName("Алиса");
            Console.WriteLine(person.GetName()); // Вывод: Алиса

            //  Инкапсуляция с private и public через свойства (свойство get и set)

            Person1 person1 = new Person1();
            person1.Name = "Боб";
            Console.WriteLine(person1.Name); // Вывод: Боб

            // Автоматические свойства

            Person2 person2 = new Person2 { Name = "Чарли" };
            Console.WriteLine(person2.Name); // Вывод: Чарли

            // protected – доступ для наследников

            Dog1 dog1 = new Dog1();
            dog1.SetSpecies("Собака");
            dog1.PrintSpecies(); // Вывод: Я – Собака

            // internal – доступ только в этой сборке

            #endregion
            #region #11 Полиморфизм. Перегрузка и переопределение. Пример
            
            // перегрузка метода Print

            Printer printer = new Printer();
            printer.Print("Привет"); // Сообщение: Привет
            printer.Print(10);       // Число: 10
            printer.Print("Повтор", 3);

            // переопределение метода MakeSound

            Animal2 myAnimal = new Animal2();
            myAnimal.MakeSound(); // Животное издает звук

            Animal2 myDog = new Dog2();
            myDog.MakeSound(); // Собака лает: Гав-гав!

            // Абстрактный класс и полиморфизм

            Animal3 myCat = new Cat3();
            myCat.MakeSound(); // Кошка мяукает: Мяу-мяу!

            // Полиморфизм через интерфейсы

            IShape shape1 = new Circle();
            IShape shape2 = new Square();

            shape1.Draw(); // Рисуем круг
            shape2.Draw(); // Рисуем квадрат

            #endregion

        }
    }
}