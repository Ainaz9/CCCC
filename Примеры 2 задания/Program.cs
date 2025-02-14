
using System.Collections;
using System.Reflection;
using System.Runtime.ConstrainedExecution;

namespace KR1
{
    class Program
    {
        #region #2 Инкапсуляция. Модификаторы доступа. Примеры

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

        #region #3 Абстрактные классы. Интерфейсы. Примеры

        abstract class Animal
        {
            public string Name { get; set; }

            public Animal(string name)
            {
                Name = name;
            }

            public abstract void MakeSound(); // Абстрактный метод (без реализации)

            public void Sleep() // Обычный метод с реализацией
            {
                Console.WriteLine($"{Name} спит.");
            }
        }

        // Дочерний класс обязан реализовать абстрактный метод
        class Dog : Animal
        {
            public Dog(string name) : base(name) { }

            public override void MakeSound() // Обязательная реализация
            {
                Console.WriteLine($"{Name} лает: Гав-гав!");
            }
        }

        interface IFlyable
        {
            void Fly(); // Нет реализации
        }

        // Реализация интерфейса в разных классах
        class Bird : IFlyable
        {
            public void Fly()
            {
                Console.WriteLine("Птица летает.");
            }
        }

        class Airplane : IFlyable
        {
            public void Fly()
            {
                Console.WriteLine("Самолёт летит.");
            }
        }

        #endregion

        #region #4 Паттерн.Фабричные методы. Примеры
        abstract class Transport
        {
            public abstract void Deliver();
        }

        // Конкретные классы (продукты)
        class Truck : Transport
        {
            public override void Deliver()
            {
                Console.WriteLine("Грузовик доставляет товар по дороге.");
            }
        }

        class Ship : Transport
        {
            public override void Deliver()
            {
                Console.WriteLine("Корабль доставляет товар по морю.");
            }
        }

        abstract class Logistics
        {
            public abstract Transport CreateTransport(); // Фабричный метод

            public void PlanDelivery()
            {
                Transport transport = CreateTransport();
                transport.Deliver();
            }
        }

        // Конкретные фабрики
        class RoadLogistics : Logistics
        {
            public override Transport CreateTransport()
            {
                return new Truck();
            }
        }

        class SeaLogistics : Logistics
        {
            public override Transport CreateTransport()
            {
                return new Ship();
            }
        }

        // Интерфейс сообщения
        interface IMessage
        {
            void Send(string text);
        }

        // Реализации сообщений
        class SmsMessage : IMessage
        {
            public void Send(string text)
            {
                Console.WriteLine($"Отправка SMS: {text}");
            }
        }

        class EmailMessage : IMessage
        {
            public void Send(string text)
            {
                Console.WriteLine($"Отправка Email: {text}");
            }
        }

        // Фабричный метод
        abstract class MessageFactory
        {
            public abstract IMessage CreateMessage();
        }

        class SmsFactory : MessageFactory
        {
            public override IMessage CreateMessage()
            {
                return new SmsMessage();
            }
        }

        class EmailFactory : MessageFactory
        {
            public override IMessage CreateMessage()
            {
                return new EmailMessage();
            }
        }

        #endregion

        #region #5 Конструкторы. Классификации конструкторов. Примеры

        class Person3
        {
            public string Name { get; set; }
            public int Age { get; set; }

            // Конструктор по умолчанию (можно не писать, он создаётся автоматически)
            public Person3()
            {
                Name = "Безымянный";
                Age = 0;
                Console.WriteLine("Создан объект Person");
            }

            // Конструктор с параметром
            public Person3(string name)
            {
                Name = name;
                Age = 0;
            }

            // Конструктор с двумя параметрами
            public Person3(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        class Logger
        {
            public static string LogFilePath;

            // Статический конструктор
            static Logger()
            {
                LogFilePath = "log.txt";
                Console.WriteLine("Статический конструктор вызван");
            }
        }

        class Singleton
        {
            private static Singleton _instance;

            // Приватный конструктор
            private Singleton() { }

            public static Singleton GetInstance()
            {
                if (_instance == null)
                    _instance = new Singleton();

                return _instance;
            }
        }

        #endregion

        #region #9 Индексаторы. Атрибуты. Примеры
        class MyCollection
        {
            private string[] _items = new string[10];

            // Индексатор
            public string this[int index]
            {
                get
                {
                    if (index < 0 || index >= _items.Length)
                        throw new ArgumentOutOfRangeException("index");
                    return _items[index];
                }
                set
                {
                    if (index < 0 || index >= _items.Length)
                        throw new ArgumentOutOfRangeException("index");
                    _items[index] = value;
                }
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class MyCustomAttribute : Attribute
        {
            public string Description { get; }

            public MyCustomAttribute(string description)
            {
                Description = description;
            }
        }

        // Определение атрибута
        [MyCustomAttribute("Это специальный класс")]
        class MyClass
        {
            [MyCustomAttribute("Этот метод выполняет задачу")]
            public void DoSomething()
            {
                Console.WriteLine("Делаю что-то важное!");
            }
        }

        #endregion

        #region #10 Операторы перехода. Примеры

        static int Square(int x)
        {
            return x * x;  // Возвращаем результат вычисления квадрата числа
        }

        #endregion

        #region #11 Наследование

        // Родительский (базовый) класс
        class Animal2
        {
            public string Name { get; set; }

            public void Eat()
            {
                Console.WriteLine($"{Name} ест.");
            }
        }

        // Дочерний (производный) класс
        class Dog2 : Animal2
        {
            public void Bark()
            {
                Console.WriteLine($"{Name} лает: Гав-гав!");
            }
        }

        #endregion

        static void Main(string[] args)
        {
            #region #1 Git. Основные команды

            #endregion

            #region #3 Абстрактные классы. Интерфейсы. Примеры

            Dog dog = new Dog("Бобик");
            dog.MakeSound(); // Бобик лает: Гав-гав!
            dog.Sleep(); // Бобик спит.

            IFlyable bird = new Bird();
            bird.Fly(); // Птица летает.

            IFlyable plane = new Airplane();
            plane.Fly(); // Самолёт летит.

            #endregion

            #region #4 Паттерн.Фабричные методы. Примеры

            Logistics logistics;

            // Можно легко менять тип логистики
            logistics = new RoadLogistics();
            logistics.PlanDelivery(); // Грузовик доставляет товар по дороге.

            logistics = new SeaLogistics();
            logistics.PlanDelivery(); // Корабль доставляет товар по морю.


            MessageFactory factory = new SmsFactory();
            IMessage message = factory.CreateMessage();
            message.Send("Привет!"); // Отправка SMS: Привет!

            factory = new EmailFactory();
            message = factory.CreateMessage();
            message.Send("Hello!"); // Отправка Email: Hello!

            #endregion

            #region #5 Конструкторы. Классификации конструкторов. Примеры

            Person3 p = new Person3(); // Создан объект Person
            Console.WriteLine(p.Name); // Безымянный

            Person3 p2 = new Person3("Иван");
            Console.WriteLine(p.Name); // Иван

            Person3 p3 = new Person3("Мария", 25);
            Console.WriteLine($"{p3.Name}, {p3.Age}"); // Мария, 25

            Console.WriteLine(Logger.LogFilePath);
            Console.WriteLine(Logger.LogFilePath); // Статический конструктор уже не вызывается

            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            Console.WriteLine(s1 == s2); // True (оба объекта одинаковые)

            #endregion

            #region #6 .Net, Типы приложений. Сборка, CLR. Примеры

            // Основные типы приложений в .NET

            /* Консольные приложения | Приложения без графического интерфейса, работающие в терминале | dotnet new console
               Веб-приложения (ASP.NET) | Веб-сайты и API, работающие в браузере | dotnet new webapi, dotnet new mvc
               Windows-приложения (WinForms, WPF) | Приложения с графическим интерфейсом для Windows | dotnet new winforms, dotnet new wpf
               Мобильные приложения (Xamarin, MAUI) | Разработка мобильных приложений для iOS и Android | dotnet new maui
               Игры (Unity, MonoGame) | Игровые движки, использующие C# | Unity, MonoGame
               Облачные и микросервисные приложения | Приложения для Azure, AWS, Kubernetes| ASP.NET Core, Docker
             */

            // Сборка (Assembly) в .NET

            // Сборка(Assembly) — это единица развертывания кода в.NET.
            // Сборки бывают двух типов:
            // Исполняемые(EXE) — запускаемые приложения.
            // Библиотеки(DLL) — классы, которые можно использовать в других проектах.
            // Создание сборки: dotnet build

            // CLR (Common Language Runtime)
            // CLR — это виртуальная машина.NET, которая:
            // Компилирует код(JIT-компиляция).
            // Управляет памятью(Garbage Collector).
            // Обеспечивает безопасность и обработку ошибок.

            // Принцип работы CLR:
            // 1. Код на C# → Компилятор C# → IL-код (Intermediate Language).
            // 2. IL - код выполняется в CLR, где компилируется в машинный код JIT-компилятором.
            // 3️. CLR управляет ресурсами во время работы программы.

            #endregion

            #region #7 Упаковка и Распаковка

            // Упаковка (Boxing)
            int number = 42;  // Значимый тип (Value Type)
            object boxedNumber = number;
            Console.WriteLine(boxedNumber); // 42

            // Распаковка (Unboxing)
            int unboxedValue = (int)boxedNumber;
            Console.WriteLine(unboxedValue);

            try
            {
                double invalidUnboxing = (double)boxedNumber; // Попытка распаковать int как double
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Ошибка при распаковке: " + ex.Message);
            }

            ArrayList list = new ArrayList();

            // Упаковка: добавление значимых типов в коллекцию
            list.Add(10); // int
            list.Add(3.14); // double
            list.Add(true); // bool

            // Распаковка: извлечение значений из коллекции
            int intValue = (int)list[0];
            double doubleValue = (double)list[1];
            bool boolValue = (bool)list[2];

            Console.WriteLine($"int: {intValue}, double: {doubleValue}, bool: {boolValue}");

            // Примитивные типы, такие как int, double, bool, обычно хранятся на стеке,
            // если они являются частью локальных переменных метода. Однако, когда
            // примитивные типы упаковываются (boxing), они перемещаются в кучу, так как
            // упаковка предполагает создание объекта. Это замедляет выполнение программы,
            // так как требует дополнительных операций выделения памяти и преобразования.
            // Упаковка(boxing) происходит, когда значение типа - значения(например, int) преобразуется
            // в тип object.Распаковка(unboxing) — это обратный процесс, когда объект преобразуется обратно
            // в тип-значение.Оба процесса требуют дополнительных ресурсов и могут негативно сказаться на производительности.

            #endregion

            #region #8 Условные операторы

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

            #region #9 Индексаторы. Атрибуты. Примеры

            MyCollection collection = new MyCollection();

            // Использование индексатора для установки значений
            collection[0] = "Привет";
            collection[1] = "Мир";

            // Использование индексатора для получения значений
            Console.WriteLine(collection[0]); // Привет
            Console.WriteLine(collection[1]); // Мир

            // Получение атрибутов с класса
            var classAttributes = (MyCustomAttribute[])typeof(MyClass).GetCustomAttributes(typeof(MyCustomAttribute), false);
            Console.WriteLine($"Описание класса: {classAttributes[0].Description}");

            // Получение атрибутов с метода
            var methodAttributes = (MyCustomAttribute[])typeof(MyClass).GetMethod("DoSomething").GetCustomAttributes(typeof(MyCustomAttribute), false);
            Console.WriteLine($"Описание метода: {methodAttributes[0].Description}");

            #endregion

            #region #10 Операторы перехода. Примеры
            
            for (int j = 0; j < 10; j++)
            {
                if (j == 5)
                {
                    break; // Прерывает цикл, если i == 5
                }
                Console.WriteLine(j);
            }

            int x = 2;
            switch (x)
            {
                case 1:
                    Console.WriteLine("Один");
                    break;  // Завершаем выполнение switch после выполнения первого case
                case 2:
                    Console.WriteLine("Два");
                    break;
                case 3:
                    Console.WriteLine("Три");
                    break;
                default:
                    Console.WriteLine("Неизвестно");
                    break;
            }

            for (int j = 0; j < 10; j++)
            {
                if (j % 2 == 0) // Если число чётное
                {
                    continue; // Пропустить вывод чётных чисел
                }
                Console.WriteLine(j); // Вывод только нечётных чисел
            }

            Console.WriteLine(Square(5));  // Вызов метода с return

            int i = 0;

            StartLoop:
            if (i < 5)
            {
                Console.WriteLine(i);
                i++;
                goto StartLoop; // Переход к метке StartLoop
            }

            #endregion

            #region #12 Массивы, типы массивов. Примеры

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

            #region #13 Var. Примеры

            // var используется для неявной типизации переменных. Это означает,
            // что компилятор сам определяет тип переменной на основе значения,
            // которое ей присваивается.

            var number1 = 10; // Компилятор определяет тип как int
            var text = "Hello, World!"; // Компилятор определяет тип как string
            var flag = true; // Компилятор определяет тип как bool

            var numbers1 = new List<int> { 1, 2, 3, 4, 5 }; // Тип: List<int>
            var dictionary = new Dictionary<string, int> // Тип: Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 }
            };

            var person = new { Name = "Alice", Age = 25 }; // Анонимный тип
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");

            var fruits = new List<string> { "Apple", "Banana", "Cherry" };
            // Использование var в foreach
            foreach (var fruit in fruits)
            {
                Console.WriteLine(fruit);
            }

            var myClass = new MyClass1(); // Тип: MyClass
            myClass.PrintMessage(); // Вывод: Hello from MyClass!

            #endregion

            #region #14 Преобразования. Convert, Tryparse, Parse

            string strNumber = "123";
            int number3 = Convert.ToInt32(strNumber);  // Преобразует строку в число
            Console.WriteLine(number3);  // Выведет: 123

            string strNull = null;
            int defaultNumber = Convert.ToInt32(strNull);  // Не вызовет ошибку, вернет 0
            Console.WriteLine(defaultNumber);  // Выведет: 0

            string strNumber1 = "123";
            int number4 = int.Parse(strNumber1);  // Преобразует строку в число
            Console.WriteLine(number3);  // Выведет: 123

            string invalidStr = "abc";
            // int invalidNumber = int.Parse(invalidStr);  // Ошибка: FormatException

            string strNull1 = null;
            // int nullNumber = int.Parse(strNull);  // Ошибка: ArgumentNullException

            string strNumber2 = "123";
            if (int.TryParse(strNumber2, out int result))
            {
                Console.WriteLine(result);  // Выведет: 123
            }
            else
            {
                Console.WriteLine("Ошибка преобразования");
            }

            string invalidStr3 = "abc";
            if (int.TryParse(invalidStr, out int invalidResult))
            {
                Console.WriteLine(invalidResult);
            }
            else
            {
                Console.WriteLine("Ошибка преобразования");  // Выведет этот текст
            }

            #endregion
        }

        class MyClass1
        {
            public void PrintMessage()
            {
                Console.WriteLine("Hello from MyClass!");
            }
        }
    }
}