using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Special_1
{
    class Program
    {

        static int Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Одномерный массив");
            Console.WriteLine("2. Двумерный массив");
            Console.WriteLine("3. Ступенчатый массив");
            Console.WriteLine("4. Завершить программу");
            Console.Write("Введите 1/2/3/4: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Clear();
            return a;
        }

        static int SubMenu1()
        {
            Console.WriteLine("1. Найти максимум и минимум");
            Console.WriteLine("2. Отсортировать по возрастанию");
            Console.WriteLine("3. Отсортировать по убыванию");
            Console.WriteLine("4. Сформировать и вывести новый массив");
            Console.WriteLine("5. Вывести массив");
            Console.WriteLine("6. Вернуться в основное меню");
            Console.Write("Введите 1/2/3/4/5/6: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Clear();
            return a;
        }

        static int SubMenu2()
        {
            Console.WriteLine("1. Найти максимум и минимум");
            Console.WriteLine("2. Провести операции над массивами");
            Console.WriteLine("3. Вывести массив");
            Console.WriteLine("4. Вернуться в основное меню");
            Console.Write("Введите 1/2/3/4: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Clear();
            return a;
        }

        static int SubMenu3()
        {
            Console.WriteLine("1. Найти максимум и минимум");
            Console.WriteLine("2. Вывести массив");
            Console.WriteLine("3. Вернуться в основное меню");
            Console.Write("Введите 1/2/3: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Clear();
            return a;
        }

        static string[][] InputNumberLine(bool isFile, string path) // Ввод строки и деление ее на подстроки-числа
        {
            string input = "";
            if (!isFile)
            {
                string s = Console.ReadLine();
                while (s.Length > 0)
                {
                    input += s;
                    s = Console.ReadLine();
                    input += '\n';
                }
            }
            else
            {
                if (path.Length == 0) path = "in.txt";
                try
                {
                    TextReader tr = new StreamReader(path);
                    input = tr.ReadToEnd();
                    tr.Close();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Не удалось открыть файл \"" + path + "\"");
                    WaitAnyKey();
                    return null;
                    //Environment.Exit(0); // Если супер-критично
                }
            }
            string[] strarr = input.Split(new Char[] { '\n' });
            string[][] sa = new string[strarr.Length-1][];
            for (int i = 0; i < strarr.Length-1; i++)
            {
                //Console.WriteLine("#" + strarr[i] + "#"); // Для отладки
                while (strarr[i][strarr[i].Length - 1] == ' ' || strarr[i][strarr[i].Length - 1] == 13)
                {
                    strarr[i] = strarr[i].Substring(0, strarr[i].Length - 1);
                }
                sa[i] = strarr[i].Split(new Char[] { ' ' });
            }
            //DisplayValues(sa); // Для отладки
            return sa;
        }

        // Шаблон функции FuncName<Type>() - принимает на вход массив любого типа (шаблон) и вида (перегружена трижды)
        static void DisplayValues<Type>(Type[] arr) // Вывод одномерного массива
        {
            Console.WriteLine();
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0); i++)
            {
                Console.WriteLine(" [{0}]: {1}", i, arr[i]);
            }
            Console.WriteLine();
        }

        static void DisplayValues<Type>(Type[,] arr) // Вывод двумерного массива
        {
            Console.WriteLine();
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0); i++)
            {
                for (int j = arr.GetLowerBound(1); j <= arr.GetUpperBound(1); j++)
                {
                    Console.WriteLine(" [{0}, {1}]: {2}", i, j, arr[i, j]);
                }
            }
            Console.WriteLine();
        }

        static void DisplayValues<Type>(Type[][] arr) // Вывод ступенчатого массива
        {
            Console.WriteLine();
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0); i++)
            {
                for (int j = arr[i].GetLowerBound(0); j <= arr[i].GetUpperBound(0); j++)
                {
                    Console.WriteLine(" [{0}][{1}]: {2}", i, j, arr[i][j]);
                }
            }
            Console.WriteLine();
        }

        static int[] CreateArr1(string[][] strarr) // Генерирует одномерный массив из введенных кусочков строки
        {
            int size = strarr[0].Length; // В данном случае мы лишь генерируем массив из первой (нулевой) строки
            int[] arr = new int[size];   // Так что нет смысла задействовать никакие строки, кроме первой (нулевой)
            for (int i = 0; i < size; ++i)
            {
                if (strarr[0][i].Length > 0) // Исключает ошибки при лишних пробелах
                {
                    int.TryParse(strarr[0][i], out arr[i]);
                }
            }
            return arr;
        }

        static int[,] CreateArr2(string[][] strarr) // Генерирует двумерный массив из введенных кусочков строки
        {
            int m = strarr.Length, n = strarr[0].Length;
            int[,] arr = new int[m, n];
            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (j >= strarr[i].Length)
                    {
                        Console.WriteLine("Некорректный ввод!");
                        WaitAnyKey();
                        return null;
                    }
                    if (strarr[i][j].Length > 0) // Исключает ошибки при лишних пробелах
                    {
                        int.TryParse(strarr[i][j], out arr[i, j]);
                    }
                }
            }
            return arr;
        }

        static int[][] CreateArr3(string[][] strarr) // Генерирует ступечнатый массив из введенных кусочков строки
        {
            int[][] arr = null;
            arr = new int[strarr.Length][];
            for (int i = 0; i < strarr.Length; i++)
            {
                arr[i] = new int[strarr[i].Length];
                for (int j = 0; j < strarr[i].Length; j++)
                {
                    if (strarr[i][j].Length > 0) // Исключает ошибки при лишних пробелах
                    {
                        int.TryParse(strarr[i][j], out arr[i][j]);
                    }
                }
            }
            return arr;
        }

        static void MySort(int[] arr, bool type) // Сортировка массива вставками
        {
            int size = arr.Length;
            for (int i = 0; i < size; ++i)
            {
                for (int j = i + 1; j < size; ++j)
                {
                    if ((type) ? arr[i] < arr[j] : arr[i] > arr[j])
                    {
                        int buf = arr[i];
                        arr[i] = arr[j];
                        arr[j] = buf;
                    }
                }
            }
        }

        static int FindM(int[] arr, bool type) // Поиск индекса максимального/минимального элемента
        {
            int size = arr.Length, m = 0;
            for (int i = 1; i < size; ++i)
            {
                if ((type) ? arr[m] < arr[i] : arr[m] > arr[i])
                {
                    m = i;
                }
            }
            return m;
        }

        static int[] FindM(int[,] arr, bool type) // Поиск индекса максимального/минимального элемента
        {
            int m = 0, n = 0;
            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(1); ++j)
                {
                    if ((type) ? arr[m, n] < arr[i, j] : arr[m, n] > arr[i, j])
                    {
                        m = i;
                        n = j;
                    }
                }
            }
            return (new int[2] {m, n});
        }

        static int[] FindM(int[][] arr, bool type) // Поиск индекса максимального/минимального элемента
        {
            int m = 0, n = 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                for (int j = 0; j < arr[i].Length; ++j)
                {
                    if ((type) ? arr[m][n] < arr[i][j] : arr[m][n] > arr[i][j])
                    {
                        m = i;
                        n = j;
                    }
                }
            }
            return (new int[2] { m, n });
        }

        static int[] GenerateEvenArr(int[] arr) // Сгенерировать массив четных элементов
        {
            int size = arr.Length, count = 0;
            for (int i = 0; i < size; ++i)
            {
                count += arr[i] % 2; // Считаем кол-во нечетных
            }
            int[] res = new int[size-count];
            count = 0;
            for (int i = 0; i < size; ++i)
            {
                if (arr[i] % 2 == 0)
                {
                    res[count] = arr[i];
                    count++;
                }
            }
            return res;
        }

        static void WaitAnyKey()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу ...");
            Console.ReadKey(); // Ожидание нажатия любой клавиши
            Console.Clear();   // Очистка консоли
        }

        static void TestArr1()
        {
            Console.WriteLine("1. Работа с одномерным массивом.");
            Console.Write("Ввод из файла (1) или с клавиатуры (0)? ");
            int mode;         // Вспомогательная переменная режима
            bool ret = true;  // Получена команда завершения подменю
            int[] arr = null; // Массив с которым мы работаем
            string path = ""; // Путь до файла, если необходим
            if (!int.TryParse(Console.ReadLine(), out mode))
            {
                Console.WriteLine("Некорректный ввод!");
                WaitAnyKey();
                return;
            }
            bool isFile = Convert.ToBoolean(mode);
            if (!isFile)
            {
                Console.Write("\nВведите числа (через пробелы): ");
            }
            else
            {
                Console.Write("Введите имя входного файла: ");
                path = Console.ReadLine();
            }
            string[][] inp = InputNumberLine(isFile, path); // Читаем строку (из файла или консоли)
            if (inp != null) // Возможно только если файл был прочитан
            {
                arr = CreateArr1(inp); // Создаем (генерируем) массив из строки
                DisplayValues(arr);   // Выводим массив
                WaitAnyKey();
                ret = false;          // Разрешаем продолжить исполнение цикла
            }
            while (!ret)
            {
                switch (SubMenu1())
                {
                    case 1:
                        //-------------------------------------
                        // Поиск максимума/минимума (2 способа)
                        //-------------------------------------
                        int max = FindM(arr, true), min = FindM(arr, false);
                        // Возвращает индекс максимального/минимального
                        Console.WriteLine("MAX: " + arr[max] + " индекс: " + max);
                        Console.WriteLine("MIN: " + arr[min] + " индекс: " + min);
                        Console.WriteLine("arr.Max(): " + arr.Max());
                        Console.WriteLine("arr.Min(): " + arr.Min());
                        //-------------------------------------
                        break;

                    case 2:
                        //-------------------------------------
                        // Сортировка (по возрастанию)
                        //-------------------------------------
                        Console.Write("Введите режим сортировки (1/0): ");
                        int.TryParse(Console.ReadLine(), out mode);
                        if (mode == 1)
                        {
                            MySort(arr, false);
                            // true - по убыванию, false - по возрастанию
                        }
                        else
                        {
                            Array.Sort(arr);
                        }
                        DisplayValues(arr);
                        //-------------------------------------
                        break;

                    case 3:
                        //-------------------------------------
                        // Сортировка (по убыванию)
                        //-------------------------------------
                        Console.Write("Введите режим сортировки (1/0): ");
                        int.TryParse(Console.ReadLine(), out mode);
                        if (mode == 1)
                        {
                            MySort(arr, true);
                            // true - по убыванию, false - по возрастанию
                        }
                        else
                        {
                            Array.Sort(arr);
                            Array.Reverse(arr);
                        }
                        DisplayValues(arr);
                        //-------------------------------------
                        break;

                    case 4:
                        //-------------------------------------
                        // Формирование нового массива (четн)
                        //-------------------------------------
                        Console.WriteLine("Массив, сформированный только из четных элементов исходного:");
                        int[] eve = GenerateEvenArr(arr);
                        DisplayValues(eve);
                        //-------------------------------------
                        break;

                    case 5:
                        DisplayValues(arr);
                        break;

                    case 6:
                        ret = true; // Подать сигнал к завершению цикла
                        break;

                    default:
                        Console.WriteLine("Нет такой операции! Повторите ввод.");
                        break;
                }
                WaitAnyKey();
            }
        }        

        static void TestArr2()
        {
            Console.WriteLine("2. Работа с двумерным массивом.");
            Console.Write("Ввод из файла (1) или с клавиатуры (0)? ");
            int mode;         // Вспомогательная переменная режима
            bool ret = true;  // Получена команда завершения подменю
            string path = ""; // Путь до файла, если необходим
            if (!int.TryParse(Console.ReadLine(), out mode))
            {
                Console.WriteLine("Некорректный ввод!");
                WaitAnyKey();
                return;
            }
            bool isFile = Convert.ToBoolean(mode);
            int[,] arr = null; // Массив с которым мы работаем
            if (!isFile)
            {
                Console.WriteLine("\nВведите числа (через пробелы) строками (через '\\n'): ");
            }
            else
            {
                Console.Write("Введите имя входного файла: ");
                path = Console.ReadLine();
            }
            string[][] inp = InputNumberLine(isFile, path); // Читаем строку (из файла или консоли)
            if (inp != null) // Возможно только если файл был прочитан
            {
                arr = CreateArr2(inp); // Создаем (генерируем) массив из строки
                if (arr != null)
                {
                    DisplayValues(arr);   // Выводим массив
                    WaitAnyKey();
                    ret = false;          // Разрешаем продолжить исполнение цикла
                }
            }
            while (!ret)
            {
                switch (SubMenu2())
                {
                    case 1:
                        //-------------------------------------
                        // Поиск максимума/минимума
                        //-------------------------------------
                        int[] max = FindM(arr, true);
                        int[] min = FindM(arr, false);
                        // Возвращает индекс максимального/минимального
                        Console.WriteLine("MAX: " + arr[max[0], max[1]] + " индекс: [" + max[0] + ", " + max[1] + "]");
                        Console.WriteLine("MIN: " + arr[min[0], min[1]] + " индекс: [" + min[0] + ", " + min[1] + "]");
                        //-------------------------------------
                        break;

                    case 2:
                        //-------------------------------------
                        // Операции над массивами (матрицами)
                        //-------------------------------------
                        int[,] dop = null; // Второй временный массив с которым мы работаем
                        if (!isFile)
                        {
                            Console.WriteLine("\nВведите числа (через пробелы) строками (через '\\n'): ");
                        }
                        else
                        {
                            Console.Write("Введите имя входного файла: ");
                            path = Console.ReadLine();
                        }
                        inp = InputNumberLine(isFile, path); // Читаем строку (из файла или консоли)
                        if (inp != null) // Возможно только если файл был прочитан
                        {
                            dop = CreateArr2(inp); // Создаем (генерируем) массив из строки
                            if (dop != null && dop.GetLength(0) == arr.GetLength(0) && dop.GetLength(1) == arr.GetLength(1))
                            {
                                DisplayValues(dop);   // Выводим массив
                                int[,] res = new int[arr.GetLength(0), arr.GetLength(1)];
                                Console.WriteLine("\t(+)");
                                for (int i = 0; i < arr.GetLength(0); i++)
                                {
                                    for (int j = 0; j < arr.GetLength(1); j++)
                                    {
                                        res[i, j] = arr[i, j] + dop[i, j];
                                    }
                                }
                                DisplayValues(res);
                                Console.WriteLine("\t(-)");
                                for (int i = 0; i < arr.GetLength(0); i++)
                                {
                                    for (int j = 0; j < arr.GetLength(1); j++)
                                    {
                                        res[i, j] = arr[i, j] - dop[i, j];
                                    }
                                }
                                DisplayValues(res);
                                Console.WriteLine("\t(*)");
                                for (int i = 0; i < arr.GetLength(0); i++)
                                {
                                    for (int j = 0; j < arr.GetLength(1); j++)
                                    {
                                        res[i, j] = arr[i, j] * dop[i, j];
                                    }
                                }
                                DisplayValues(res);
                                break;
                            }
                        }
                        Console.WriteLine("Что-то пошло не так!");
                        //-------------------------------------
                        break;

                    case 3:
                        DisplayValues(arr);
                        break;

                    case 4:
                        ret = true; // Подать сигнал к завершению цикла
                        break;

                    default:
                        Console.WriteLine("Нет такой операции! Повторите ввод.");
                        break;
                }
                WaitAnyKey();
            }
        }

        static void TestArr3()
        {
            Console.WriteLine("3. Работа со ступенчатым массивом.");
            Console.Write("Ввод из файла (1) или с клавиатуры (0)? ");
            int mode;         // Вспомогательная переменная режима
            bool ret = true;  // Получена команда завершения подменю
            string path = ""; // Путь до файла, если необходим
            if (!int.TryParse(Console.ReadLine(), out mode))
            {
                Console.WriteLine("Некорректный ввод!");
                WaitAnyKey();
                return;
            }
            bool isFile = Convert.ToBoolean(mode);
            int[][] arr = null; // Массив с которым мы работаем
            if (!isFile)
            {
                Console.WriteLine("\nВведите числа (через пробелы) строками (через '\\n'): ");
            }
            else
            {
                Console.Write("Введите имя входного файла: ");
                path = Console.ReadLine();
            }
            string[][] inp = InputNumberLine(isFile, path); // Читаем строку (из файла или консоли)
            if (inp != null) // Возможно только если файл был прочитан
            {
                arr = CreateArr3(inp); // Создаем (генерируем) массив из строки
                if (arr != null)
                {
                    DisplayValues(arr);   // Выводим массив
                    WaitAnyKey();
                    ret = false;          // Разрешаем продолжить исполнение цикла
                }
            }
            while (!ret)
            {
                switch (SubMenu3())
                {
                    case 1:
                        //-------------------------------------
                        // Поиск максимума/минимума
                        //-------------------------------------
                        int[] max = FindM(arr, true);
                        int[] min = FindM(arr, false);
                        // Возвращает индекс максимального/минимального
                        Console.WriteLine("MAX: " + arr[max[0]][max[1]] + " индекс: [" + max[0] + ", " + max[1] + "]");
                        Console.WriteLine("MIN: " + arr[min[0]][min[1]] + " индекс: [" + min[0] + ", " + min[1] + "]");
                        //-------------------------------------
                        break;

                    case 2:
                        DisplayValues(arr);
                        break;

                    case 3:
                        ret = true; // Подать сигнал к завершению цикла
                        break;

                    default:
                        Console.WriteLine("Нет такой операции! Повторите ввод.");
                        break;
                }
                WaitAnyKey();
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                switch (Menu())
                {
                    case 1:
                        TestArr1(); // Одномерный
                        break;

                    case 2:
                        TestArr2(); // Двумерный
                        break;

                    case 3:
                        TestArr3(); // Ступенчатый
                        break;

                    case 4:
                        Environment.Exit(0); // Досвидули
                        break;

                    default:
                        Console.WriteLine("Нет такой операции! Повторите ввод.");
                        WaitAnyKey();
                        break;
                }
            }
        }

    }
}
