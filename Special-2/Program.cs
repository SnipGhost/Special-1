using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Special_2
{
    class Program
    {

        struct Date // d - день, m - месяц, y - год  
        {
            public int d; // День
            public int m; // Месяц
            public int y; // Год

            public static bool operator ==(Date d1, Date d2) // Перегрузка оператора эквивалентности (чтобы не мучаться потом)
            {
                if (d1.d == d2.d && d1.m == d2.m && d1.y == d2.y) return true;
                else return false;
            }
            public static bool operator !=(Date d1, Date d2) // Перегрузка оператора в пару к ==
            {
                if (d1.d != d2.d || d1.m != d2.m || d1.y != d2.y) return true;
                else return false;
            }
            public static bool operator >(Date d1, Date d2) // Перегрузка оператора сравнения >
            {
                if ((d1.y > d2.y) || (d1.y == d2.y && d1.m > d2.m) || (d1.y == d2.y && d1.m == d2.m && d1.d > d2.d))
                    return true;
                else return false;
            }
            public static bool operator <(Date d1, Date d2) // Перегрузка оператора в пару к >
            {
                if ((d1.y < d2.y) || (d1.y == d2.y && d1.m < d2.m) || (d1.y == d2.y && d1.m == d2.m && d1.d < d2.d))
                    return true;
                else return false;
            }
            public static implicit operator string(Date dt) // Перегрузка оператора неявного приведения к типу string
            {
                string day = dt.d + ".";
                string mon = dt.m + ".";
                string year = "" + dt.y;
                if (day.Length < 3) day = "0" + day;
                if (mon.Length < 3) mon = "0" + mon;
                while (year.Length < 4) year = "0" + year;
                return (day + mon + year);
            }
            public void FromString(string input) // А это метод приведения ИЗ string В Date
            {
                string[] dt = input.Split(new char[] { '.' });
                if (!int.TryParse(dt[0], out this.d)) this.d = 0;
                if (!int.TryParse(dt[1], out this.m)) this.m = 0;
                if (!int.TryParse(dt[2], out this.y)) this.y = 0;
            }
        }

        class Student
        {
            public int id;         // № записи-элемента (ID)
            public string name;    // ФИО студента
            public Date bith;      // Дата рождения 
            public string group;   // Группа
            public string stage;   // Курс
            public float amark;    // Средний балл
            public string univer;  // Институт/Университет

            public Student(string[] input) // Конструктор класса
            {
                // дабы не присваивать каждому полю потом значение, а просто ввести строку
                int.TryParse(input[0], out this.id);
                this.name = input[1] + " " + input[2] + " " + input[3];
                this.bith.FromString(input[4]);
                this.group = input[5];
                this.stage = input[6];
                float.TryParse(input[7], out this.amark);
                this.univer = input[8];
                int c = 9;
                while (c < input.Length)
                {
                    this.univer += " " + input[c];
                    c++;
                }
                Console.WriteLine("Студент №" + id + " [" + name + "]\t- создан успешно.");
            }
        }

        class DataBase
        {
            public List<Student> stu;

            public delegate int CMP(Student s1, Student s2);

            public void loadDataFromFile(string path) // Метод загрузки из файла
            {
                try
                {
                    TextReader tr = new StreamReader(path);
                    stu = new List<Student>();
                    string str = tr.ReadLine();
                    int i = 1;
                    while (str != null && str.Length > 0)
                    {
                        string[] input = str.Split(new char[] { ' ' });
                        int num = i;
                        int.TryParse(input[0], out num);
                        /* // На всякий случай: создавать дефолтные записи вместо недостающих
                        while (i < num)
                        {             
                            Student nul = new Student(new string[] { i+"", "Дефолтов", "Дефолт", "Дефолтович", "0.0.0", "Группа", "K", "0", "NULL" });
                            stu.Add(nul);
                            i++;
                        }
                        */
                        if (i % 2 == 1) Console.ForegroundColor = ConsoleColor.DarkGray;
                        Student s = new Student(input);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        stu.Add(s);
                        str = tr.ReadLine();
                        i++;
                    }
                    tr.Close();
                    Console.WriteLine("Загрузка данных завершена успешно");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось открыть файл \"" + path + "\"\n\nПричина: " + e.ToString());
                    //Environment.Exit(0); // Если супер-критично
                }     
            }

            public void saveDataToFile(string path) // Метод записи в файл
            {
                TextWriter sw = new StreamWriter(path);
                for (int i = 0; i < stu.Count; ++i)
                {
                    sw.Write(stu[i].id + " " + stu[i].name + " " + stu[i].bith + " " + stu[i].group);
                    sw.Write(" " + stu[i].stage + " " + stu[i].amark + " " + stu[i].univer + "\r\n");
                }
                sw.Close();
            }

            public void print() // Метод красивой печати
            {
                for (int i = -1; i < stu.Count; ++i) // -1 - это вывод шапки таблицы
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("{0, 2} ", (i == -1) ? "##" : stu[i].id.ToString());
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("{0, -26} ", (i == -1) ? "Фамилия Имя Отчество" : stu[i].name);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("{0, 10} ", (i == -1) ? "dd.mm.yyyy" : stu[i].bith);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0, -20} ", (i == -1) ? "Название ВУЗа" : stu[i].univer);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0, 6} ", (i == -1) ? "Группа" : stu[i].group);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("({0, 1}) ", (i == -1) ? "K" : stu[i].stage);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("{0, 4}\n", (i == -1) ? "Ср." : stu[i].amark.ToString());
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            public void printFields()
            {
                Console.WriteLine(" 1 - № записи-элемента (ID)");
                Console.WriteLine(" 2 - ФИО студента");
                Console.WriteLine(" 3 - Дата рождения");
                Console.WriteLine(" 4 - Группа");
                Console.WriteLine(" 5 - Курс");
                Console.WriteLine(" 6 - Средний балл");
                Console.WriteLine(" 7 - Институт/Университет");
            }

            public int FindId(int id)
            {
                int i;
                for (i = 0; i < stu.Count; ++i)
                    if (stu[i].id == id) break;
                if (i == stu.Count) i = -1;
                return i;
            }

            public void changeField(int id, int field, string input)
            {
                int i = FindId(id);
                if (i == -1)
                {
                    Console.WriteLine("Нет такой записи");
                    return;
                }
                switch (field)
                {
                    case 1:
                        int nid = 0;
                        if (!int.TryParse(input, out nid))
                            Console.WriteLine("Неверный формат ID");
                        bool jack = true;
                        for (int j = 0; (j < stu.Count) && jack; ++j)
                            if (stu[j].id == nid)
                                jack = false;
                        if (!jack) Console.WriteLine("Этот ID уже используется");
                        else stu[i].id = nid;
                        break;
                    case 2:
                        stu[i].name = input;
                        break;
                    case 3:
                        stu[i].bith.FromString(input);
                        break;
                    case 4:
                        stu[i].group = input;
                        break;
                    case 5:
                        stu[i].stage = input;
                        break;
                    case 6:
                        float.TryParse(input, out stu[i].amark);
                        break;
                    case 7:
                        stu[i].univer = input;
                        break;
                    default:
                        Console.WriteLine("Нет такого поля");
                        break;
                }
            }

            public void deleteStudent(int id)
            {
                int i = FindId(id);
                if (i != -1)
                {
                    stu.RemoveAt(i);
                }
                else
                {
                    Console.WriteLine("Нет такой записи");
                }
            }

            public int CmpId(Student s1, Student s2)   // Реализует сравнение по ID 
            {
                return (s1.id - s2.id);
            }
            public int CmpName(Student s1, Student s2) // Реализует сравнение по имени 
            {
                return string.Compare(s1.name, s2.name);
            }
            public int CmpDate(Student s1, Student s2) // Реализует сравнение по дате 
            {
                if (s1.bith > s2.bith) return 1;
                else if (s1.bith < s2.bith) return -1;
                else return 0;
            }

            public void sort(int field, bool type) // type = true - обратная сортировка 
            {
                CMP Comparate = null;
                switch (field)
                {
                    case 1:
                        Comparate = CmpId;
                        break;

                    case 2:
                        Comparate = CmpName;
                        break;

                    case 3:
                        Comparate = CmpDate;
                        break;

                    default:
                        Console.WriteLine("Для этого поля недоступна сортировка");
                        return;
                }
                for (int i = 0; i < stu.Count; ++i)
                {
                    Student temp;
                    for (int j = i + 1; j < stu.Count; ++j)
                    {
                        int cp = Comparate(stu[j], stu[i]);
                        if ((type) ? cp > 0 : cp < 0)
                        {
                            temp = stu[i];
                            stu[i] = stu[j];
                            stu[j] = temp;
                        }
                    }
                }
            }

            public void findStudent(string s)
            {
            
            }

            public void AnalyzeMarks()
            {
                float s = stu[0].amark, max = s, min = s;
                int max_i = 0, min_i = 0;
                for (int i = 1; i < stu.Count; ++i)
                {
                    s += stu[i].amark;
                    if (stu[i].amark > max)
                    {
                        max = stu[i].amark;
                        max_i = i;
                    }
                    else if (stu[i].amark < min)
                    {
                        min = stu[i].amark;
                        min_i = i;
                    }
                }
                Console.WriteLine("Средний балл:      " + (s/stu.Count).ToString() + " для " + stu.Count + " студентов");
                Console.WriteLine("Максимальный балл: " + max.ToString() + "\t [" + stu[max_i].name + "]");
                Console.WriteLine("Минимальный балл:  " + min.ToString() + "\t [" + stu[min_i].name + "]");
            }
        }

        static int Menu()
        {
            Console.Clear();
            Console.WriteLine("0. Печать БД на экран");
            Console.WriteLine("1. Загрузить БД из файла");
            Console.WriteLine("2. Сохранить БД в файл");
            Console.WriteLine("3. Изменить запись по ID");
            Console.WriteLine("4. Удалить запись по ID");
            Console.WriteLine("5. Сортировка БД");
            Console.WriteLine("6. Поиск элемента");
            Console.WriteLine("7. Анализ средних баллов");
            Console.WriteLine("8. Завершить программу");
            Console.Write("Введите 0/1/2/3/4/5/6/7/8: ");
            int a;
            int.TryParse(Console.ReadLine(), out a);
            Console.Clear();
            return a;
        }

        static void WaitAnyKey() // Перекочевала из предыдущего проекта 
        {
            Console.WriteLine("\nДля продолжения нажмите любую клавишу ...");
            Console.ReadKey(); // Ожидание нажатия любой клавиши
            Console.Clear();   // Очистка консоли
        }

        static void Main(string[] args)
        {
            DataBase db = null; // Собственно, экземпляр класса нашей БД
            string path;        // Вспомогательная переменная для ввода имен файлов

            while (true)
            {
                switch (Menu())
                {
                    case 0: // Печать БД
                        if (db != null)
                            db.print();
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 1: // Загрузить БД из файла
                        db = new DataBase();
                        Console.Write("Введите имя входного файла: ");
                        path = Console.ReadLine();
                        if (path.Length == 0) path = "DataBase.txt";
                        db.loadDataFromFile(path);
                        break;

                    case 2: // Сохранить БД в файл
                        if (db != null)
                        {
                            Console.Write("Введите имя выходного файла: ");
                            path = Console.ReadLine();
                            if (path.Length == 0) path = "DataBase-out.txt";
                            db.saveDataToFile(path);
                        }
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 3: // Изменить запись по ID
                        if (db != null)
                        {
                            int id, field;
                            Console.Write("Введите ID: ");
                            if (!int.TryParse(Console.ReadLine(), out id)) id = -1;
                            db.printFields();
                            Console.Write("Введите номер поля: ");
                            if (!int.TryParse(Console.ReadLine(), out field)) field = -1;
                            Console.Write("Введите значение: ");
                            string input = Console.ReadLine();
                            db.changeField(id, field, input);
                        }
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 4: // Удалить запись по ID
                        if (db != null)
                        {
                            int id;
                            Console.Write("Введите ID: ");
                            if (!int.TryParse(Console.ReadLine(), out id)) id = -1;
                            db.deleteStudent(id);
                        }
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 5: // Сортировка БД
                        if (db != null)
                        {
                            db.printFields();
                            int field, type;
                            Console.Write("Введите номер поля: ");
                            if (!int.TryParse(Console.ReadLine(), out field)) field = -1;
                            Console.Write("Введите 1 для прямой сортировки и 0 для обратной: ");
                            if (!int.TryParse(Console.ReadLine(), out type)) type = 0;
                            db.sort(field, (type == 0));
                        }
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 6: // Поиск элемента по значению и номеру поля
                        if (db != null)
                        {
                            Console.WriteLine("Не готово еще");
                        }
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 7: // Анализ средних баллов (???)
                        if (db != null) 
                        {
                            db.AnalyzeMarks();
                        }
                        else Console.WriteLine("БД пуста!");
                        break;

                    case 8:
                        Environment.Exit(0); // Досвидули
                        break;

                    default:
                        Console.WriteLine("Нет такой операции! Повторите ввод.");
                        break;
                }
                WaitAnyKey();
            }
        }
    }
}
