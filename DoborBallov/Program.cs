
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;

namespace Dobor
{
    class Program
    {
        #region #1

        // Интерфейс для фигур
        public interface IShape
        {
            double GetArea();
        }

        // Класс "Круг"
        public class Circle : IShape
        {
            public double Radius { get; }

            public Circle(double radius)
            {
                Radius = radius;
            }

            public double GetArea()
            {
                return Math.PI * Radius * Radius;
            }
        }

        // Класс "Прямоугольник"
        public class Rectangle : IShape
        {
            public double Width { get; }
            public double Height { get; }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public double GetArea()
            {
                return Width * Height;
            }
        }

        // Фабрика для создания фигур
        public class ShapeFactory
        {
            public static IShape CreateShape(string shapeType, params double[] parameters)
            {
                switch (shapeType.ToLower())
                {
                    case "circle":
                        if (parameters.Length == 1)
                            return new Circle(parameters[0]);
                        throw new ArgumentException("Для круга требуется 1 параметр (радиус).");

                    case "rectangle":
                        if (parameters.Length == 2)
                            return new Rectangle(parameters[0], parameters[1]);
                        throw new ArgumentException("Для прямоугольника требуется 2 параметра (ширина и высота).");

                    default:
                        throw new ArgumentException("Неизвестный тип фигуры.");
                }
            }
        }

        #endregion

        #region #2

        // Интерфейс для обработки данных
        public interface IDataProcessor
        {
            void ProcessData(string data);
        }

        // Класс, реализующий интерфейс и имеющий событие
        public class DataProcessor : IDataProcessor
        {
            // Событие, которое вызывается после успешной обработки данных
            public event EventHandler<string> DataProcessed;

            public void ProcessData(string data)
            {
                Console.WriteLine($"Обрабатываем данные: {data}");

                // Имитация обработки данных (например, задержка)
                System.Threading.Thread.Sleep(1000);

                // Вызов события после успешной обработки
                OnDataProcessed(data);
            }

            // Метод для вызова события
            protected virtual void OnDataProcessed(string data)
            {
                // Проверка, есть ли подписчики на событие
                if (DataProcessed != null)
                {
                    // Вызов события
                    DataProcessed(this, $"Данные успешно обработаны: {data}");
                }
            }
        }

        // Метод для обработки события DataProcessed
        private static void Processor_DataProcessed(object sender, string message)
        {
            Console.WriteLine($"[Событие] {message}");
        }

        #endregion

        #region #3

        public abstract class Employee
        {
            public string Name { get; }
            public string Position { get; }
            public double BaseSalary { get; }

            public Employee(string name, string position, double baseSalary)
            {
                Name = name;
                Position = position;
                BaseSalary = baseSalary;
            }

            // Виртуальный метод расчета зарплаты (можно переопределить в наследниках)
            public virtual double GetSalary()
            {
                return BaseSalary;
            }

            public override string ToString()
            {
                return $"{Name} ({Position}): {GetSalary():C2}";
            }
        }

        // Класс "Менеджер" (получает фиксированный бонус)
        public class Manager : Employee
        {
            private double Bonus { get; }

            public Manager(string name, double baseSalary, double bonus)
                : base(name, "Manager", baseSalary)
            {
                Bonus = bonus;
            }

            public override double GetSalary()
            {
                return BaseSalary + Bonus;
            }
        }

        // Класс "Разработчик" (может получать премию)
        public class Developer : Employee
        {
            private double Bonus { get; }

            public Developer(string name, double baseSalary, double bonus = 0)
                : base(name, "Developer", baseSalary)
            {
                Bonus = bonus;
            }

            public override double GetSalary()
            {
                return BaseSalary + Bonus;
            }
        }


        #endregion

        #region #4

        class Vector
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Vector(double x, double y) { X = x; Y = y; }

            public static Vector operator +(Vector v1, Vector v2)
            {
                return new Vector(v1.X + v2.X, v1.Y + v2.Y);
            }

            public static Vector operator -(Vector v1, Vector v2)
            {
                return new Vector(v1.X - v2.X, v1.Y - v2.Y);
            }

            public static Vector operator *(Vector v1, double scalar)
            {
                return new Vector(v1.X * scalar, v1.Y * scalar);
            }

            public static bool operator ==(Vector v1, Vector v2)
            {
                return (v1.X == v2.X) && (v1.Y == v2.Y);
            }

            public static bool operator !=(Vector v1, Vector v2)
            {
                return (v1.X != v2.X) || (v1.Y != v2.Y);
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }

        #endregion

        #region #5

        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }
            public Node<T> Prev { get; set; }

            public Node(T data)
            {
                Data = data;
                Next = null;
                Prev = null;
            }
        }

        // Двусвязный список
        public class CustomLinkedList<T>
        {
            private Node<T> head;
            private Node<T> tail;
            public int Count { get; private set; }

            // Добавление в начало списка
            public void AddFirst(T data)
            {
                Node<T> newNode = new Node<T>(data);
                if (head == null)
                {
                    head = tail = newNode;
                }
                else
                {
                    newNode.Next = head;
                    head.Prev = newNode;
                    head = newNode;
                }
                Count++;
            }

            // Добавление в конец списка
            public void AddLast(T data)
            {
                Node<T> newNode = new Node<T>(data);
                if (tail == null)
                {
                    head = tail = newNode;
                }
                else
                {
                    tail.Next = newNode;
                    newNode.Prev = tail;
                    tail = newNode;
                }
                Count++;
            }

            // Добавление по индексу
            public void AddAt(int index, T data)
            {
                if (index < 0 || index > Count)
                    throw new ArgumentOutOfRangeException("Индекс вне диапазона.");

                if (index == 0)
                {
                    AddFirst(data);
                    return;
                }
                if (index == Count)
                {
                    AddLast(data);
                    return;
                }

                Node<T> newNode = new Node<T>(data);
                Node<T> current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                newNode.Prev = current;
                current.Next.Prev = newNode;
                current.Next = newNode;

                Count++;
            }

            // Удаление элемента по значению
            public bool Remove(T data)
            {
                Node<T> current = head;
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        if (current == head)
                        {
                            head = head.Next;
                            if (head != null)
                                head.Prev = null;
                        }
                        else if (current == tail)
                        {
                            tail = tail.Prev;
                            tail.Next = null;
                        }
                        else
                        {
                            current.Prev.Next = current.Next;
                            current.Next.Prev = current.Prev;
                        }

                        Count--;
                        return true;
                    }
                    current = current.Next;
                }
                return false;
            }

            // Вывод содержимого списка
            public void PrintList()
            {
                Node<T> current = head;
                while (current != null)
                {
                    Console.Write(current.Data + " <-> ");
                    current = current.Next;
                }
                Console.WriteLine("null");
            }
        }

        #endregion

        static void Main(string[] args)
        {
            #region #1 

            // Реализуйте паттерн "Фабрика" для создания объектов разных классов, например, Circle и Rectangle,
            // которые имеют общий интерфейс IShape.Фабрика должна принимать параметр(тип фигуры) и
            // создавать соответствующий объект. Напишите программу, которая создает несколько фигур
            // с помощью фабрики и выводит их площади.

            try
            {
                IShape circle = ShapeFactory.CreateShape("circle", 5);
                IShape rectangle = ShapeFactory.CreateShape("rectangle", 4, 6);

                Console.WriteLine($"Площадь круга: {circle.GetArea():F2}");
                Console.WriteLine($"Площадь прямоугольника: {rectangle.GetArea():F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine();

            #endregion

            #region #2

            // Создайте интерфейс IDataProcessor, который содержит метод ProcessData. Реализуйте его в классе
            // DataProcessor.Создайте событие в классе DataProcessor, которое будет вызываться,
            // после успешной обработки данных. Напишите программу, которая будет подписываться
            // на это событие и выводить сообщение о завершении обработки.

            // Создаем объект обработчика данных
            DataProcessor processor = new DataProcessor();

            // Подписываемся на событие DataProcessed с использованием метода
            processor.DataProcessed += Processor_DataProcessed;

            // Запускаем обработку данных
            processor.ProcessData("Пример данных");

            Console.WriteLine("Программа завершена.");

            Console.WriteLine();

            #endregion

            #region #3

            // Реализуйте иерархию классов для разных типов сотрудников в компании. Создайте базовый класс
            // Employee, который содержит общие свойства(например, имя, должность, зарплата) и метод
            // GetSalary.Затем создайте производные классы Manager и Developer, которые переопределяют
            // метод GetSalary(например, менеджер получает бонус). Напишите программу, которая создает
            // объекты этих классов и выводит зарплаты всех сотрудников, используя полиморфизм.

            // Создаем список сотрудников
            List<Employee> employees = new List<Employee>
        {
            new Manager("Иван", 5000, 1000),
            new Developer("Алексей", 4000, 500),
            new Developer("Мария", 4500),
            new Manager("Ольга", 5500, 1500)
        };

            // Вывод информации о зарплатах сотрудников
            Console.WriteLine("Зарплаты сотрудников:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            };

            Console.WriteLine();

            #endregion

            #region #4

            // Создайте класс Vector, который представляет двумерный вектор с двумя свойствами Х и Y.
            // Перегрузите операторы для выполнения следующих операций: +, -, *, = (Два вектора считаются
            // равными, если их компоненты одинаковы). Кроме того, создайте метод ToStringO, который будет
            // возвращать строковое представление вектора в формате(Х, Y).Напишите программу, которая создает
            // несколько объектов типа Vector и выполняет с ними операции с использованием перегруженных операторов.
            // Выведите результаты этих операций в консоль

            Vector v1 = new Vector(3, 4);
            Vector v2 = new Vector(1, 2);
            double scalar = 2;

            // Демонстрация перегруженных операторов
            Vector sum = v1 + v2;
            Vector diff = v1 - v2;
            Vector scaled = v1 * scalar;
            bool areEqual = v1 == v2;
            bool areNotEqual = v1 != v2;

            // Вывод результатов
            Console.WriteLine($"v1 = {v1}");
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine($"v1 + v2 = {sum}");
            Console.WriteLine($"v1 - v2 = {diff}");
            Console.WriteLine($"v1 * {scalar} = {scaled}");
            Console.WriteLine($"v1 == v2: {areEqual}");
            Console.WriteLine($"v1 != v2: {areNotEqual}");

            Console.WriteLine();

            #endregion

            #region #5

            // Реализуйте собственную версию двухсвязного списка. Включите методы
            // для добавления элементов в начало, в конец и по индексу, а также
            // метод для удаления элемента по значению. Напишите программу, которая
            // использует этот список для выполнения различных операций и выводит его содержимое.

            CustomLinkedList<int> list = new CustomLinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(20);
            list.AddLast(30);
            list.AddAt(1, 25);

            Console.WriteLine("Список после добавления элементов:");
            list.PrintList();

            list.Remove(25);
            Console.WriteLine("Список после удаления 25:");
            list.PrintList();

            list.Remove(20);
            Console.WriteLine("Список после удаления 20:");
            list.PrintList();

            #endregion
        }
    }
}
