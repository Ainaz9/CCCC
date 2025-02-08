
namespace C_Sharp
{
    class Program
    {
        #region #1 задача

        class Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double DistanceTo(Point other)
            {
                return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2)); // т.Пифагора
            }
        }

        class Circle
        {
            public Point Center { get; set; }
            public double Radius { get; set; }

            public Circle(double x, double y, double radius)
            {
                Center = new Point(x, y);
                Radius = radius;
            }

            public double GetArea()
            {
                return Math.PI * Math.Pow(Radius, 2);
            }

            public bool Intersects(Circle other)
            {
                double distance = Center.DistanceTo(other.Center);
                return distance <= (Radius + other.Radius);
            }

            public static bool operator <(Circle c1, Circle c2) => c1.Radius < c2.Radius;
            public static bool operator >(Circle c1, Circle c2) => c1.Radius > c2.Radius;

        }
        #endregion

        #region #2 задача

        class Graph
        {
            // Список рёбер, каждое ребро представлено как кортеж из двух точек
            public List<(PointGraph, PointGraph)> Edges { get; set; } = new List<(PointGraph, PointGraph)>();

            // Метод для добавления ребра
            public void AddEdge(PointGraph p1, PointGraph p2)
            {
                Edges.Add((p1, p2));
            }

            // Метод для удаления ребра
            public void RemoveEdge(PointGraph p1, PointGraph p2)
            {
                for (int i = Edges.Count - 1; i >= 0; i--) // Идём с конца списка, чтобы безопасно удалять элементы
                {
                    var edge = Edges[i];
                    if ((edge.Item1.Equals(p1) && edge.Item2.Equals(p2)) ||
                        (edge.Item1.Equals(p2) && edge.Item2.Equals(p1)))
                    {
                        Edges.RemoveAt(i);
                        Console.WriteLine($"Удалено ребро между {p1} и {p2}");
                    }
                }
            }

            // Метод для поиска пути между точками
            public bool PathExists(PointGraph start, PointGraph end, HashSet<PointGraph> visited = null)
            {
                if (visited == null) visited = new HashSet<PointGraph>();
                if (start == end) return true; // Если начальная и конечная точки одинаковы, путь существует

                visited.Add(start); // Добавляем точку в посещенные

                foreach (var edge in Edges)
                {
                    if (edge.Item1 == start && !visited.Contains(edge.Item2))
                    {
                        if (PathExists(edge.Item2, end, visited)) return true;
                    }
                    else if (edge.Item2 == start && !visited.Contains(edge.Item1))
                    {
                        if (PathExists(edge.Item1, end, visited)) return true;
                    }
                }

                return false; // Путь не найден
            }
        }

        // Класс для представления точки
        class PointGraph
        {
            public double X { get; set; }
            public double Y { get; set; }

            public PointGraph(double x, double y)
            {
                X = x;
                Y = y;
            }
        }


        #endregion

        #region #3 задача

        // Класс для представления книги
        class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime ReleaseDate { get; set; }

            public Book(string title, string author, DateTime releaseDate)
            {
                Title = title;
                Author = author;
                ReleaseDate = releaseDate;
            }

            public override string ToString()
            {
                return $"Название: {Title}, Автор: {Author}, Дата выхода: {ReleaseDate.ToShortDateString()}";
            }
        }

        // Класс для представления библиотеки
        class Library
        {
            // Список книг в библиотеке
            public List<Book> Books { get; set; } = new List<Book>();

            // Метод для добавления книги
            public void AddBook(Book book)
            {
                Books.Add(book);
            }

            // Метод для удаления книги по названию
            public void RemoveBook(string title)
            {
                int removedCount = Books.RemoveAll(book => book.Title == title);
                if (removedCount > 0)
                    Console.WriteLine("Книга удалена");
                else
                    Console.WriteLine("Такой книги нету");
            }

            // Метод для сортировки по названию
            public void SortByTitle()
            {
                Books = Books.OrderBy(b => b.Title).ToList();
            }

            // Метод для сортировки по автору
            public void SortByAuthor()
            {
                Books = Books.OrderBy(b => b.Author).ToList();
            }

            // Метод для сортировки по дате выхода
            public void SortByReleaseDate()
            {
                Books = Books.OrderBy(b => b.ReleaseDate).ToList();
            }

            // Метод для вывода всех книг
            public void DisplayBooks()
            {
                if (Books.Count == 0)
                {
                    Console.WriteLine("Библиотека пуста.");
                    return;
                }

                foreach (var book in Books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        #endregion

        #region #4 задача

        // Абстрактный класс Product
        public abstract class Product
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            public Product(string name, int quantity, decimal price)
            {
                Name = name;
                Quantity = quantity;
                Price = price;
            }

            // Абстрактный метод для расчета скидки
            public abstract decimal CountDiscount();

            // Метод для вывода информации о товаре с учетом скидки
            public void PrintInfo()
            {
                decimal discount = CountDiscount();
                decimal priceWithDiscount = Price - discount;
                Console.WriteLine($"Название: {Name}, Количество: {Quantity}, Цена: {Price:C}, Скидка: {discount:C}, Цена со скидкой: {priceWithDiscount:C}");
            }
        }

        // Класс Electronic
        public class Electronic : Product
        {
            public Electronic(string name, int quantity, decimal price) : base(name, quantity, price) { }

            // Реализация метода для расчета скидки на электронные товары
            public override decimal CountDiscount()
            {
                if (Quantity > 5)
                    return Price * 0.15m; // Скидка 15%, если товара больше 5 единиц
                return Price * 0.05m; // Иначе скидка 5%
            }
        }

        // Класс Furniture
        public class Furniture : Product
        {
            public Furniture(string name, int quantity, decimal price) : base(name, quantity, price) { }

            // Реализация метода для расчета скидки на мебель
            public override decimal CountDiscount()
            {
                if (Quantity > 2)
                    return Price * 0.10m; // Скидка 10%, если товара больше 2 единиц
                return Price * 0.03m; // Иначе скидка 3%
            }
        }

        // Класс Clothes
        public class Clothes : Product
        {
            public Clothes(string name, int quantity, decimal price) : base(name, quantity, price) { }

            // Реализация метода для расчета скидки на одежду
            public override decimal CountDiscount()
            {
                if (Quantity > 10)
                    return Price * 0.20m; // Скидка 20%, если товара больше 10 единиц
                return Price * 0.07m; // Иначе скидка 7%
            }
        }

        // Абстрактная фабрика для создания продуктов
        public abstract class ProductFactory
        {
            public abstract Product CreateProduct(string name, int quantity, decimal price);
        }

        // Фабрика для создания электронных товаров
        public class ElectronicFactory : ProductFactory
        {
            public override Product CreateProduct(string name, int quantity, decimal price)
            {
                return new Electronic(name, quantity, price);
            }
        }

        // Фабрика для создания мебели
        public class FurnitureFactory : ProductFactory
        {
            public override Product CreateProduct(string name, int quantity, decimal price)
            {
                return new Furniture(name, quantity, price);
            }
        }

        // Фабрика для создания одежды
        public class ClothesFactory : ProductFactory
        {
            public override Product CreateProduct(string name, int quantity, decimal price)
            {
                return new Clothes(name, quantity, price);
            }
        }

        #endregion

        #region #5 задача

        class Matrix
        {
            private int[,] data;

            // Конструктор: количество строк и столбцов, заполняется случайными числами от 1 до 100
            public Matrix(int rows, int cols)
            {
                data = new int[rows, cols];
                Random rand = new Random();
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        data[i, j] = rand.Next(1, 101); // Случайное число от 1 до 100
                    }
                }
            }

            // Переопределение оператора сложения матриц
            public static Matrix operator +(Matrix m1, Matrix m2)
            {
                if (m1.Rows != m2.Rows || m1.Cols != m2.Cols)
                    throw new InvalidOperationException("Матрицы должны иметь одинаковые размеры для сложения.");

                Matrix result = new Matrix(m1.Rows, m1.Cols);
                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m1.Cols; j++)
                    {
                        result.data[i, j] = m1.data[i, j] + m2.data[i, j];
                    }
                }
                return result;
            }

            // Переопределение оператора умножения матрицы на число
            public static Matrix operator *(Matrix m, int scalar)
            {
                Matrix result = new Matrix(m.Rows, m.Cols);
                for (int i = 0; i < m.Rows; i++)
                {
                    for (int j = 0; j < m.Cols; j++)
                    {
                        result.data[i, j] = m.data[i, j] * scalar;
                    }
                }
                return result;
            }

            // Метод транспонирования матрицы
            public Matrix Transpose()
            {
                Matrix result = new Matrix(Cols, Rows);
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        result.data[j, i] = data[i, j];
                    }
                }
                return result;
            }

            // Метод для вывода матрицы на экран
            public void Print()
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        Console.Write(data[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }

            // Свойства для получения количества строк и столбцов
            public int Rows => data.GetLength(0);
            public int Cols => data.GetLength(1);
        }

        #endregion
        static void Main()
        {
            #region #1 задача

                // Класс точка (координаты и метод для поиска расстояния между точками) и класс
                // окружность(свойство радиус и координаты центра окружности,
                // метод для расчета площади и метод, который пишет пересекаются окружности или нет,
                // перегрузка операторов <, > для радиуса

                Circle c1 = new Circle(0, 0, 5);
                Circle c2 = new Circle(4, 0, 3);

                Console.WriteLine($"Площадь первой окружности: {c1.GetArea()}");
                Console.WriteLine($"Площадь второй окружности: {c2.GetArea()}");
                Console.WriteLine($"Окружности пересекаются: {c1.Intersects(c2)}");
                Console.WriteLine($"Первая окружность < второй: {c1 < c2}");
                Console.WriteLine($"Первая окружность > второй: {c1 > c2}");

            #endregion

            #region #2 задача

            // Класс граф в нем свойство список ребер с кортежем ,
            // метод для добавления ребра, метод для удаления ребра,
            // метод для поиска пути между точками

            // Создаем точки
            PointGraph p1 = new PointGraph(0, 0);
            PointGraph p2 = new PointGraph(1, 1);
            PointGraph p3 = new PointGraph(2, 2);

            // Создаем граф и добавляем рёбра
            Graph graph = new Graph();
            graph.AddEdge(p1, p2);
            graph.AddEdge(p2, p3);

            // Проверяем, существует ли путь между точками
            Console.WriteLine($"Путь между p1 и p3: {graph.PathExists(p1, p3)}");

            // Удаляем ребро
            graph.RemoveEdge(p1, p2);
            Console.WriteLine($"Путь между p1 и p3 после удаления ребра: {graph.PathExists(p1, p3)}");

            #endregion

            #region #3 задача

            //  Класс Library в нем поле List<Book>, class Book, поля автор, название, дата выхода,
            //  в классе Library метод AddBook, RemoveBook, вроде по названию
            //  и сортировки по названию/автору/дате выхода. 

            // Создаем библиотеку
            Library library = new Library();

            // Добавляем книги
            library.AddBook(new Book("Война и мир", "Лев Толстой", new DateTime(1869, 1, 1)));
            library.AddBook(new Book("Преступление и наказание", "Фёдор Достоевский", new DateTime(1866, 1, 1)));
            library.AddBook(new Book("1984", "Джордж Оруэлл", new DateTime(1949, 6, 8)));

            // Отображаем книги
            Console.WriteLine("Все книги в библиотеке:");
            library.DisplayBooks();
            Console.WriteLine();

            // Сортируем книги по названию
            library.SortByTitle();
            Console.WriteLine("Книги, отсортированные по названию:");
            library.DisplayBooks();
            Console.WriteLine();

            // Сортируем книги по автору
            library.SortByAuthor();
            Console.WriteLine("Книги, отсортированные по автору:");
            library.DisplayBooks();
            Console.WriteLine();

            // Сортируем книги по дате выхода
            library.SortByReleaseDate();
            Console.WriteLine("Книги, отсортированные по дате выхода:");
            library.DisplayBooks();
            Console.WriteLine();

            // Удаляем книгу
            library.RemoveBook("Преступление и наказание");
            Console.WriteLine("После удаления книги:");
            library.DisplayBooks();

            #endregion

            #region #4 задача

            // Фабричный метод с покупками, Electronic, Furniture, Clothes.
            // Сделать фабрику, которая создавала бы все эти типы. А эти 3 типа наследовать
            // от абстрактного класса Product в нем название, количество, цена, методы:
            // PrintInfo, где все поля выводятся с учетом скидки.
            // И countDiscount расчет скидки на основе чего-либо (придумать самому условие),
            // реализовать эти методы в трех типах

            // Создание фабрик
            ProductFactory electronicFactory = new ElectronicFactory();
            ProductFactory furnitureFactory = new FurnitureFactory();
            ProductFactory clothesFactory = new ClothesFactory();

            // Создание продуктов через фабрики
            Product laptop = electronicFactory.CreateProduct("Ноутбук", 6, 50000);
            Product sofa = furnitureFactory.CreateProduct("Диван", 3, 20000);
            Product shirt = clothesFactory.CreateProduct("Рубашка", 12, 1500);

            // Вывод информации о продуктах
            Console.WriteLine("Информация о продукте (с учетом скидки):");
            laptop.PrintInfo();
            sofa.PrintInfo();
            shirt.PrintInfo();

            #endregion

            #region #5 задача

            // Класс матрица целых чисел
            // Конструктор количество строк и столбцов и заполняются
            // рандомными числами от 1 до 100
            // Переопределить операцию сложения и умножения на число
            // Методы транспонирование матрицы и вывода на экран

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

            #endregion
        }
    }
}
