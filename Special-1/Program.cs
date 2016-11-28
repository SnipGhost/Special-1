using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Special_1
{
    class Program
    {

        public static int Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Одномерный массив");
            Console.WriteLine("2. Двумерный массив");
            Console.WriteLine("3. Ступенчатый массив");
            Console.WriteLine("4. Завершить программу");
            Console.Write("Введите 1/2/3/4: ");
            int a = int.Parse(Console.ReadLine());
            Console.Clear();
            return a;
        }

        public static string[] InputNumberLine() // Ввод строки и деление ее на подстроки-числа
        {
            string input = Console.ReadLine();
            string[] strarr = input.Split(new Char[] { ' ' });
            return strarr;
        }

        public static void DisplayValues(int[] arr) // Вывод одномерного массива
        {
            Console.WriteLine();
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0); i++)
            {
                Console.WriteLine(" [{0}]: {1}", i, arr[i]);
            }
            Console.WriteLine();
        }

        public static int[] CreateArr(string[] strarr) // Генерирует массив из введенных кусочков строки
        {
            int size = strarr.Length;
            int[] arr = new int[size];
            for (int i = 0; i < size; ++i)
            {
                if (strarr[i].Length > 0) // Исключает ошибки при лишних пробелах
                {
                    arr[i] = int.Parse(strarr[i]);
                }
            }
            return arr;
        }

        public static void MySort(int[] arr, bool type) // Сортировка массива вставками
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

        public static int FindM(int[] arr, bool type)
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

        public static int[] GenerateEvenArr(int[] arr)
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

        static void Main(string[] args)
        {
            while (true)
            {
                switch (Menu())
                {
                    case 1:
                        Console.Write("1. Работа с одномерным массивом.\nВведите числа (через пробелы): ");
                        int[] arr = CreateArr(InputNumberLine());
                        DisplayValues(arr);
                        //-------------------------------------
                        // Поиск максимума/минимума (2 способа)
                        //-------------------------------------
                        int max = FindM(arr, true), min = FindM(arr, false);
                        // Возвращает индекс максимального/минимального
                        Console.WriteLine("MAX: " + arr[max] + " индекс: " + max);
                        Console.WriteLine("MIN: " + arr[min] + " индекс: " + min);
                        Console.WriteLine("MAX: " + arr.Max());
                        Console.WriteLine("MIN: " + arr.Min());
                        //-------------------------------------
                        // Сортировка (2 способа по 2 типа)
                        //-------------------------------------
                        MySort(arr, true); 
                        // true - по убыванию, false - по возрастанию
                        DisplayValues(arr);
                        Array.Sort(arr);
                        //Array.Reverse(arr);
                        // Отразит массив (если он отсротирован - даст сортировку в другую сторону)
                        //arr.Reverse(); // Или так, тоже можно
                        DisplayValues(arr);
                        //-------------------------------------
                        // Формирование нового массива (четн)
                        //-------------------------------------
                        int[] eve = GenerateEvenArr(arr);
                        DisplayValues(eve);
                        //-------------------------------------
                        break;

                    case 2:
                        Console.WriteLine("2. Работа с двумерным массивом.");

                        break;

                    case 3:
                        Console.WriteLine("3. Работа со ступенчатым массивом.");

                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Нет такой операции! Повторите ввод.");
                        break;
                }
                Console.ReadKey();
            }
        }

    }
}
