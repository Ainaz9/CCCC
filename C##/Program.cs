

namespace C_Sharp
{
    #region Билет 1. Задание 2 
    class EventGenerator
    {
        public event Action<string> OnEvent; // Событие с текстовым аргументом
        private string name; // Текстовое поле с названием объекта

        public EventGenerator(string name)
        {
            this.name = name;
        }

        public void GenerateEvent()
        {
            OnEvent?.Invoke(name); // Генерация события с передачей имени объекта
        }
    }

    class EventHandlerClass
    {
        public void HandleEvent(string message)
        {
            Console.WriteLine($"Событие получено от: {message}");
        }
    }
    #endregion
    #region Билет 1. Задание 3 
    abstract class Document
    {
        public abstract void GetInfo();
        public abstract void Open();
        public abstract void Close();
    }
    
    // Конкретный класс для DOCX
    class DocxDocument : Document
    {
        public override void GetInfo() => Console.WriteLine("Это документ DOCX.");
        public override void Open() => Console.WriteLine("Открытие DOCX файла...");
        public override void Close() => Console.WriteLine("Закрытие DOCX файла...");
    }

    // Конкретный класс для PDF
    class PdfDocument : Document
    {
        public override void GetInfo() => Console.WriteLine("Это документ PDF.");
        public override void Open() => Console.WriteLine("Открытие PDF файла...");
        public override void Close() => Console.WriteLine("Закрытие PDF файла...");
    }

    // Конкретный класс для XML
    class XmlDocument : Document
    {
        public override void GetInfo() => Console.WriteLine("Это документ XML.");
        public override void Open() => Console.WriteLine("Открытие XML файла...");
        public override void Close() => Console.WriteLine("Закрытие XML файла...");
    }

    // Абстрактная фабрика
    abstract class DocumentCreator
    {
        public abstract Document CreateDocument();
    }

    // Конкретные фабрики
    class DocxCreator : DocumentCreator
    {
        public override Document CreateDocument() => new DocxDocument();
    }

    class PdfCreator : DocumentCreator
    {
        public override Document CreateDocument() => new PdfDocument();
    }

    class XmlCreator : DocumentCreator
    {
        public override Document CreateDocument() => new XmlDocument();
    }
    #endregion
    #region Билет 2. Задание 2 
    class TextArray
    {
        private string[] texts;

        public TextArray(string[] initialTexts)
        {
            texts = initialTexts;
        }

        // Одномерный индексатор для работы с элементами массива
        public string this[int index]
        {
            get
            {
                index = NormalizeIndex(index, texts.Length);
                return texts[index];
            }
            set
            {
                index = NormalizeIndex(index, texts.Length);
                texts[index] = value;
            }
        }

        // Двумерный индексатор для работы с символами в строках
        public char this[int row, int col]
        {
            get
            {
                row = NormalizeIndex(row, texts.Length);
                col = NormalizeIndex(col, texts[row].Length);
                return texts[row][col];
            }
        }

        // Метод нормализации индексов (циклическая перестановка)
        private int NormalizeIndex(int index, int length)
        {
            return (index % length + length) % length; // Работает для отрицательных индексов
        }

        // Вывод массива для удобства
        public void PrintArray()
        {
            foreach (var text in texts)
            {
                Console.WriteLine(text);
            }
        }
    }
    #endregion
    #region Билет 2. Задание 3 
    class Exam
    {
        public string Name { get; set; }  // Название экзамена
        public int Grade { get; set; }    // Оценка

        public Exam(string name, int grade)
        {
            Name = name;
            Grade = grade;
        }

        public override string ToString() => $"{Name}: {Grade}"; // Удобный вывод
    }
    class Student
    {
        public string FullName { get; set; } // ФИО студента
        public int Age { get; set; }         // Возраст
        public List<Exam> Exams { get; set; } // Список экзаменов

        public Student(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
            Exams = new List<Exam>();
        }

        // Метод для добавления экзамена
        public void AddExam(string name, int grade)
        {
            Exams.Add(new Exam(name, grade));
        }

        // Метод для изменения оценки по экзамену
        public void UpdateExam(string name, int newGrade)
        {
            foreach (var exam in Exams)
            {
                if (exam.Name == name)
                {
                    exam.Grade = newGrade;
                    return;
                }
            }
            Console.WriteLine($"Экзамен '{name}' не найден!");
        }

        // Метод для сортировки экзаменов (по убыванию или возрастанию)
        public void SortExams(bool ascending = true)
        {
            Exams = MergeSort(Exams, ascending);
        }

        // Реализация сортировки слиянием (Merge Sort)
        private List<Exam> MergeSort(List<Exam> list, bool ascending)
        {
            if (list.Count <= 1) return list;

            int mid = list.Count / 2;
            List<Exam> left = list.GetRange(0, mid);
            List<Exam> right = list.GetRange(mid, list.Count - mid);

            return Merge(MergeSort(left, ascending), MergeSort(right, ascending), ascending);
        }

        private List<Exam> Merge(List<Exam> left, List<Exam> right, bool ascending)
        {
            List<Exam> result = new List<Exam>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if ((ascending && left[i].Grade <= right[j].Grade) ||
                    (!ascending && left[i].Grade >= right[j].Grade))
                {
                    result.Add(left[i++]);
                }
                else
                {
                    result.Add(right[j++]);
                }
            }

            while (i < left.Count) result.Add(left[i++]);
            while (j < right.Count) result.Add(right[j++]);

            return result;
        }

        // Метод для вывода информации о студенте и его экзаменах
        public void PrintInfo()
        {
            Console.WriteLine($"\nСтудент: {FullName}, Возраст: {Age}");
            Console.WriteLine("Экзамены:");
            foreach (var exam in Exams)
            {
                Console.WriteLine($"  {exam}");
            }
        }
    }
    #endregion
    #region Билет 3. Задание 2
    class NumberWrapper
    {
        public int Value { get; set; } // Целочисленное поле

        public NumberWrapper(int value)
        {
            Value = value;
        }

        // Перегрузка оператора true
        public static bool operator true(NumberWrapper obj)
        {
            return obj.Value == 2 || obj.Value == 3 || obj.Value == 5 || obj.Value == 7;
        }

        // Перегрузка оператора false
        public static bool operator false(NumberWrapper obj)
        {
            return obj.Value < 1 || obj.Value > 10;
        }

        // Перегрузка оператора &
        public static NumberWrapper operator &(NumberWrapper a, NumberWrapper b)
        {
            return new NumberWrapper(a.Value & b.Value); // Побитовое И
        }

        // Перегрузка оператора |
        public static NumberWrapper operator |(NumberWrapper a, NumberWrapper b)
        {
            return new NumberWrapper(a.Value | b.Value); // Побитовое ИЛИ
        }

        public override string ToString() => $"[{Value}]";
    }
    #endregion

    class Program
    {
        static void Main()
        {
            #region Билет 1. Задание 2

            /* Напишите программу, в которой есть класс с событием. Событие обрабатывается методами, имеющими
            текстовый аргумент и не возвращающими результат. У класса должно быть текстовое поле, в которое при
            создании объекта класса записывается название объекта. В классе должен быть описан метод для
            генерирования события, который вызывается без аргументов. При генерировании события аргументом
            передается значение текстового поля объекта, генерирующего событие. Еще один класс, описанный в
            программе, должен содержать метод с текстовым аргументом, не возвращающий результат. При вызове
            метод отображает значение своего текстового аргумента. В главном методе программы необходимо создать
            два объекта первого класса и один объект второго класса.Для событий объектов первого класса
            обработчиком регистрируется метод объекта второго класса(получается, что метод одного и того же
            объекта зарегистрирован обработчиком для событий двух объектов).Для каждого из объектов первого
            класса необходимо сгенерировать событие. При этом метод, зарегистрированный обработчиком, должен
            выводить название объекта, сгенерировавшего событие. */

            EventGenerator obj1 = new EventGenerator("Объект 1");
            EventGenerator obj2 = new EventGenerator("Объект 2");
            EventHandlerClass handler = new EventHandlerClass();

            // Регистрация обработчика для событий обоих объектов
            obj1.OnEvent += handler.HandleEvent;
            obj2.OnEvent += handler.HandleEvent;

            // Генерация событий
            obj1.GenerateEvent();
            obj2.GenerateEvent();

            Console.WriteLine();

            #endregion
            #region Билет 1. Задание 3 

            /* Необходимо написать реализацию паттерна "Фабричный метод". Необходимо реализовать работу с
            различными типами файлов. Есть файлы: docx, pdf, xml. Необходимо создать классы, которые будут
            создавать документы этих типов. Каждый документ имеет по три метода: вывод информации о типе этого
            документа на экран, вывод информации о размере файла и метод для сохранениям файла в данном
            формате. */

            DocumentCreator docxCreator = new DocxCreator();
            DocumentCreator pdfCreator = new PdfCreator();
            DocumentCreator xmlCreator = new XmlCreator();

            // Создаем документы
            Document docx = docxCreator.CreateDocument();
            Document pdf = pdfCreator.CreateDocument();
            Document xml = xmlCreator.CreateDocument();

            // Работаем с документами
            docx.GetInfo();
            docx.Open();
            docx.Close();

            Console.WriteLine();

            pdf.GetInfo();
            pdf.Open();
            pdf.Close();

            Console.WriteLine();

            xml.GetInfo();
            xml.Open();
            xml.Close();

            Console.WriteLine();

            #endregion
            #region Билет 2. Задание 2

            /* Напишите программу с классом, в котором есть текстовый массив. Опишите в классе одномерный и
            двумерный индексаторы. Одномерный индексатор позволяет прочитать элемент текстового массива и
            присвоить новое значение элементу текстового массива. Двумерный индексатор позволяет прочитать
            символ в элементе текстового массива (первый индекс определяет элемент в текстовом массиве, а второй
            индекс определяет символ в тексте). Предусмотрите циклическую перестановку индексов в случае, если
            они выходят за верхнюю допустимую границу  */

            string[] words = { "Привет", "Мир", "C#" };
            TextArray textArray = new TextArray(words);

            // Тест одномерного индексатора
            Console.WriteLine("Одномерный индексатор:");
            Console.WriteLine(textArray[0]);  // "Привет"
            Console.WriteLine(textArray[1]);  // "Мир"
            Console.WriteLine(textArray[2]);  // "C#"
            Console.WriteLine(textArray[-1]); // "C#" (циклически)

            textArray[1] = "Вселенная"; // Изменение второго элемента
            Console.WriteLine(textArray[1]);  // "Вселенная"

            Console.WriteLine("\nДвумерный индексатор:");
            Console.WriteLine(textArray[0, 0]); // 'П'
            Console.WriteLine(textArray[1, 2]); // 'л'
            Console.WriteLine(textArray[2, 1]); // '#' 
            Console.WriteLine(textArray[2, 10]); // 'C' (циклический индекс)

            Console.WriteLine("\nОбновленный массив:");
            textArray.PrintArray();

            Console.WriteLine();

            #endregion
            #region Билет 2. Задание 3 

            /* Необходимо создать класса студента. У студента определить ФИО, возраст, список экзаменов(экзамен и
            оценка), метод для сортировки экзаменов по убыванию и возврастанию(сортировка слиянием). Необходимо
            предусмотреть возможность изменять экзамены.  */

            // Создаем студента
            Student student = new Student("Иван Петров", 20);

            // Добавляем экзамены
            student.AddExam("Математика", 85);
            student.AddExam("Физика", 90);
            student.AddExam("История", 75);
            student.AddExam("Информатика", 95);

            Console.WriteLine("Исходный список экзаменов:");
            student.PrintInfo();

            // Сортируем по возрастанию
            student.SortExams(true);
            Console.WriteLine("\nСортировка по возрастанию:");
            student.PrintInfo();

            // Сортируем по убыванию
            student.SortExams(false);
            Console.WriteLine("\nСортировка по убыванию:");
            student.PrintInfo();

            // Обновляем оценку
            student.UpdateExam("Физика", 100);
            Console.WriteLine("\nПосле изменения оценки по Физике:");
            student.PrintInfo();

            Console.WriteLine();

            #endregion
            #region Билет 3. Задание 2

            /*  Напишите программу, в которой есть класс с целочисленным полем. Перегрузите операторы &, |, true и
            false так, чтобы с объектами класса можно было использовать операторы && и ||. Перегрузку следует
            реализовать так, чтобы объект считался "истинным", если значение его числового поля равно 2, 3, 5 или 7.
            Объект должен рассматриваться как "ложный", если значение его числового поля меньше 1 или больше 10. */

            NumberWrapper num1 = new NumberWrapper(3); // "Истинный"
            NumberWrapper num2 = new NumberWrapper(5); // "Истинный"
            NumberWrapper num3 = new NumberWrapper(8); // "Ложный"
            NumberWrapper num4 = new NumberWrapper(12); // "Ложный"

            // Проверка оператора true/false
            Console.WriteLine(num1 ? "num1 Истина" : "num1 Ложь");
            Console.WriteLine(num3 ? "num3 Истина" : "num3 Ложь");

            // Использование && и ||
            if (num1 && num2) Console.WriteLine("num1 && num2: Оба истинные");
            if (num1 || num3) Console.WriteLine("num1 || num3: Хотя бы один истинный");
            if (num3 && num4) Console.WriteLine("num3 && num4: Оба истинные (не выведется)");

            // Демонстрация работы & и |
            NumberWrapper result1 = num1 & num2;
            NumberWrapper result2 = num1 | num3;

            Console.WriteLine($"num1 & num2 = {result1}");
            Console.WriteLine($"num1 | num3 = {result2}");

            Console.WriteLine();

            #endregion

        }
    }
}
