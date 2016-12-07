using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Special_2
{

    struct Date // d - день, m - месяц, y - год  
    {
        public int d; // День
        public int m; // Месяц
        public int y; // Год

        public static bool operator ==(Date d1, Date d2)
        {
            if (d1.d == d2.d && d1.m == d2.m && d1.y == d2.y) return true;
            else return false;
        }
        public static bool operator !=(Date d1, Date d2)
        {
            if (d1.d != d2.d || d1.m != d2.m || d1.y != d2.y) return true;
            else return false;
        }
        public static bool operator >(Date d1, Date d2)
        {
            if ((d1.y > d2.y) || (d1.y == d2.y && d1.m > d2.m) || (d1.y == d2.y && d1.m == d2.m && d1.d > d2.d))
                return true;
            else return false;
        }
        public static bool operator <(Date d1, Date d2)
        {
            if ((d1.y < d2.y) || (d1.y == d2.y && d1.m < d2.m) || (d1.y == d2.y && d1.m == d2.m && d1.d < d2.d))
                return true;
            else return false;
        }
        public static implicit operator string(Date dt)
        {
            string day = dt.d + ".";
            string mon = dt.m + ".";
            string year = "" + dt.y;
            if (day.Length < 3) day = "0" + day;
            if (mon.Length < 3) mon = "0" + mon;
            while (year.Length < 4) year = "0" + year;
            return (day + mon + year);
        }
    }

    class Student
    {
        public int id;         // № записи-элемента (ID)
        public string name;    // ФИО студента
        public Date bith;      // Дата рождения 
        public string group;   // Группа
        public string stage;   // Курс
        public float  amark;   // Средний балл
        public string univer;  // Институт/Университет

        public Student(string[] input)
        {
            int.TryParse(input[0], out this.id);
            this.name = input[1] + " " + input[2] + " " + input[3];
            string[] dt = input[4].Split(new char[] { '.' });
            int.TryParse(dt[0], out this.bith.d);
            int.TryParse(dt[1], out this.bith.m);
            int.TryParse(dt[2], out this.bith.y);    
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

        public void loadDataFromFile(string path)
        {
            stu = new List<Student>();
            TextReader tr = new StreamReader(path);
            string str = tr.ReadLine();
            while (str != null && str.Length > 0) 
            {
                string[] input = str.Split(new char[] { ' ' });              
                Student s = new Student(input);
                stu.Add(s);
                str = tr.ReadLine();
            }
            Console.WriteLine("Загрузка данных завершена успешно");
        }

        public void print()
        {
            Console.WriteLine("{0, -2} {1, -25} {2, 10} {3, -20} {4, 6} ({5, 1}) ~{6, 4}", 
                "##", "Фамилия Имя Очество", "dd.mm.yyyy", "Название ВУЗа", "Группа", "К", "Ср.");
            for (int i = 0; i < stu.Count; ++i)
            {
                Console.WriteLine("{0, -2} {1, -25} {2, 10} {3, -20} {4, 6} ({5, 1}) ~{6, 4}", 
                    stu[i].id, stu[i].name, stu[i].bith + "", stu[i].univer, stu[i].group, stu[i].stage, stu[i].amark);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DataBase db = new DataBase();
            Console.Write("Введите имя файла: ");
            string path = Console.ReadLine();
            if (path.Length == 0) path = "DataBase.txt";
            db.loadDataFromFile(path);
            Console.WriteLine();
            db.print();
        }
    }
}
