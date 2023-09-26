using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double previous = 0;  //Переменная сохраняюще предыдущие значение

            do
            {
                Console.Write("");
                Console.Clear();
                if (previous != 0 && previous != double.PositiveInfinity)  // Проверка предыдущего значения
                {
                    Console.WriteLine(previous);
                }
                else
                {
                    try   // Исключение на случай ошибки при конвертации строки в double
                    {
                        Console.WriteLine("Введите число");
                        previous = Convert.ToDouble(Console.ReadLine());  //Получение первичного значения
                    }
                    catch
                    {
                        previous = 0; // Обнуление результата ||| На всякий случай
                        continue;
                    }
                }
                Console.WriteLine("Выберите операцию написав её");
                Console.WriteLine("- : минус");
                Console.WriteLine("+ : плюс");
                Console.WriteLine("/ : деление");
                Console.WriteLine("* : умножение");
                Console.WriteLine("^ : возвести в степень");
                Console.WriteLine("3 : квадратный корень"); // Как более правильно писать корень?
                operation calculation = new operation();
                switch (Console.ReadLine())  //Выбор оператора
                {
                    case "-":
                        previous = calculation.minus(previous);
                        break;
                    case "+":
                        previous = calculation.plus(previous);
                        break;
                    case "/":
                        previous = calculation.divide(previous);
                        break;
                    case "*":
                        previous = calculation.multiplicate(previous);
                        break;
                    case "^":
                        previous = calculation.exponentiation(previous);
                        break;
                    case "3":
                        previous = calculation.square_root(previous);
                        break;
                    default:
                        Console.WriteLine("Неизвестный оператор");
                        Thread.Sleep(2000);        //Ожидание на 2 сек.
                        break;
                }
            }
            while (true);
        }
    }
    class operation // Вынесение операций для последующей возможности расширения списка операций
    {
        private void Clear(string operation) //Спорно нужно ли так делать
                                             //или лучше оставлять в каждой операции
        {
            Console.Clear();
            Console.Write(operation);
        }
        public double minus(double previous)
        {
            Clear(previous + " - ");
            double current = Convert.ToDouble(Console.ReadLine());
            return previous - current;
        }
        public double plus(double previous)
        {
            Clear(previous + " + ");
            double current = Convert.ToDouble(Console.ReadLine());
            return previous + current;
        }
        public double divide(double previous)
        {
            Clear(previous + " / ");
            double current = Convert.ToDouble(Console.ReadLine());
            return previous / current;
        }
        public double multiplicate(double previous)
        {
            Clear(previous + " * ");
            double current = Convert.ToDouble(Console.ReadLine());
            return previous * current;
        }
        public double exponentiation(double previous)
        {
            Clear(previous + " ^ ");
            double current = Convert.ToDouble(Console.ReadLine());
            for (int i = 0; i < current; i++)
                previous *= previous;
            if (previous == double.PositiveInfinity)
            {
                Console.WriteLine("Число приблизилось к бесконечности");
                Thread.Sleep(2000);
            }
            return previous;
        }

        // Квадратный корень немного странно работает

        public double square_root(double previous)
        {
            Console.Clear();
            if (previous < 0)
            {
                Console.WriteLine("Нельзя найти корень из отицательного числа");
                Thread.Sleep(2000);
                return 0;
            }
            if (previous == 0 || previous == 1)
                return previous;
            double mid = (1 + previous / 2) / 2;   // Первичное приблизительное число

            while (true)
            {
                double next = (mid + (previous / mid)) / 2;  // Формула нахождения корней
                if (next > mid)   //Проще было поставить модуль при вычитании, но тут всё без Math
                {
                    if (next - mid < 0.00000000000005)
                        break;
                    else
                        mid = next;
                }
                else
                {
                    if (mid - next < 0.00000000000005)
                        break;
                    else
                        mid = next;
                }
            }
            return mid;
        }
    }
}
