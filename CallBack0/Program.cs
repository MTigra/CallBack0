using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Library;
/*
Мартиросян Т.О.
Гр. 176(1)
Программирование
Задача про растение(Callback).
Вариант 0
*/

namespace Martirosyan0
{
    class Program
    {
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            do
            {
                try
                {
                    int N = InputInt("Введите желаемое количество элементов:");
                    Plant[] plantsArr = new Plant[N];
                    plantsArr = Array.ConvertAll(plantsArr,
                        x => x = new Plant(GetRandomDouble(25, 100), GetRandomDouble(0, 100), GetRandomDouble(0, 80)));

                    ShowArrInfo(plantsArr);
                    // Сортировка по росту.
                    Array.Sort(plantsArr, delegate (Plant X, Plant Y) { return X.Growth.CompareTo(Y.Growth) * (-1); });
                    Console.WriteLine("После сортировки по росту по убыванию:\n");
                    ShowArrInfo(plantsArr);

                    // Сортировка по морозоустойчивости.
                    Array.Sort(plantsArr, (X, Y) => X.Frostresistance.CompareTo(Y.Frostresistance));
                    Console.WriteLine("После сортировки по морзоустойчивости по возрастанию:\n");
                    ShowArrInfo(plantsArr);

                    // Сортировка по четности. сначала четные.
                    Array.Sort(plantsArr, oddEvenComparator);
                    Console.WriteLine("После сортировки по четности:\n");
                    ShowArrInfo(plantsArr);

                    Console.WriteLine("\nПосле изменений");
                    // Делаем изменения.
                    int i = 0;
                    Array.ConvertAll(plantsArr,
                        x =>
                        {

                            if ((int)x.Frostresistance % 2 == 0 && x.Frostresistance - 10 < 0)
                            {
                                i++;
                            }
                            return ((int)x.Frostresistance) % 2 == 0
                                ? x.Frostresistance - 10 < 0
                                    ? x.Frostresistance
                                    : x.Frostresistance -= 10
                                : x.Frostresistance /= 2;
                        });
                    if (i > 0) Console.WriteLine($"(!!!) Некоторые растения невозможно изменить.\nИх кол-во == {i}\n");

                    ShowArrInfo(plantsArr);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                    
                }
                Console.WriteLine("Что угодно чтобы запустить программу заново. Чтобы выйти нажмите ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        // Я не знаю что такое четность у дробного числа, поэтому я сделаю вот так :)
        // и вообще я не уверен что такое понятие существует.
        private static int oddEvenComparator(Plant x, Plant y)
        {
            if ((int)x.Photosensitivity % 2 == 0 && (int)y.Photosensitivity % 2 != 0) return -1;
            if ((int)x.Photosensitivity % 2 != 0 && (int)y.Photosensitivity == 0) return 1;
            return 0;
        }

        private static int InputInt(string s)
        {
            int n;
            Console.WriteLine(s);
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.WriteLine("Допустимы лишь положительные целочисленные значения.");
                Console.WriteLine(s);
            }
            return n;
        }

        private static double GetRandomDouble(int minInc, int maxInc)
        {
            double number;
            number = rnd.Next(minInc, maxInc) + rnd.NextDouble();
            return number;
        }

        private static void ShowArrInfo(Plant[] arr)
        {
            Array.ForEach(arr, x => Console.WriteLine(x.ToString()));
        }
    }
}
