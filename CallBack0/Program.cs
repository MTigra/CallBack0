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
                    //второй параметр - компаратор. Метод CompareTo это сишарповский метод
                    // он возвращает такие значения, будто мы сортируем эти элементы по возрастанию. Поэтому я умножаю
                    //на минус один чтоб получить по убыванию
                    Array.Sort(plantsArr, delegate (Plant X, Plant Y) { return X.Growth.CompareTo(Y.Growth) * (-1); });
                    Console.WriteLine("После сортировки по росту по убыванию:\n");
                    ShowArrInfo(plantsArr);

                    // Сортировка по морозоустойчивости.
                    //здесь все просто. используем комппейр-ту чтоб не писать свою длинную хуйню
                    Array.Sort(plantsArr, (X, Y) => X.Frostresistance.CompareTo(Y.Frostresistance));
                    Console.WriteLine("После сортировки по морзоустойчивости по возрастанию:\n");
                    ShowArrInfo(plantsArr);

                    // Сортировка по четности. сначала четные.
                    // здесь сортируем по четности используя свой собственный метод который будет компаратором.
                    // то естьс оответствует делегату который принимет два параметра и возвращает инт.
                    Array.Sort(plantsArr, oddEvenComparator);
                    Console.WriteLine("После сортировки по четности:\n");
                    ShowArrInfo(plantsArr);

                    Console.WriteLine("\nПосле изменений");
                    // Делаем изменения.
                    int i = 0;// эта хуйня нужна чтобы отслдить сколько записей мы не смогли и зменить
                    
                    Array.ConvertAll(plantsArr,
                        x =>
                        {
                            //тут проверяю смогу ли сделать изменение (проверяю типа если вычту то будет ли меньше нуля)
                            if ((int)x.Frostresistance % 2 == 0 && x.Frostresistance - 10 < 0)
                            {
                                i++;
                            }
                            // тернарный оператор с тернанрным оператором.
                            //если словами, то так. Если четное, то проверим станет ли отрицательным если вычесть 10.
                            // Если станет, то не меняем. Если останется положительным то поменяем
                            //ну а если нечетное то делим на два, согласно условию задачи
                            return ((int)x.Frostresistance) % 2 == 0
                                ? x.Frostresistance - 10 < 0
                                    ? x.Frostresistance
                                    : x.Frostresistance -= 10
                                : x.Frostresistance /= 2;
                        });
                    //информируем юзера что что-то мы не смогли поменять
                    if (i > 0) Console.WriteLine($"(!!!) Некоторые растения невозможно изменить.\nИх кол-во == {i}\n");
                    //снова отображаем массив
                    ShowArrInfo(plantsArr);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    //ловим эксепшн выводя инфу.
                    // на самом деле этот прикол выше с тем что я проверяю станет ли элемент орицательный
                    // это наверное не совсем верно, а может и верно
                    // я просто не хотел чтобы изза того что один элемент низя изменить, не менять все.
                    //вообще наверное это де-то в обработчике исключения надо делать
                    Console.WriteLine(ex.Message);
                    
                }
                Console.WriteLine("Что угодно чтобы запустить программу заново. Чтобы выйти нажмите ESC");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        // Я не знаю что такое четность у дробного числа, поэтому я сделаю вот так :)
        // и вообще я не уверен что такое понятие существует.
        private static int oddEvenComparator(Plant x, Plant y)
        {
            //ну тут все просто
            if ((int)x.Photosensitivity % 2 == 0 && (int)y.Photosensitivity % 2 != 0) return -1;
            if ((int)x.Photosensitivity % 2 != 0 && (int)y.Photosensitivity == 0) return 1;
            return 0;
        }

        private static int InputInt(string s)
        {
            //здесь сделано так чтобы понравилось МК.
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
