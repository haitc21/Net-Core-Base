using System;
using System.Threading;
using System.Threading.Tasks;

namespace DaLuongBatDongBo
{
    class Program
    {
        public static void DoSomething(int second, string mes, ConsoleColor color)
        {
            lock (Console.Out) // khoa doi tuong console lai de cac thread khac k su dung den khi het tac vu trong P{ }
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"Strat {mes,10}");
            }

            for (int i = 1; i <= second; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"Task : {mes,10} Second : {i,2}");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
            }
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"End {mes,10}");
                Console.WriteLine("===================================================");
                Console.ResetColor();
            }
        }
        public static async Task Task2()
        {
            Task t2 = new Task(
               () => DoSomething(5, "Task 2", ConsoleColor.Yellow)
           );
            t2.Start();
            await t2;
            Console.WriteLine("Task 2 end.........");
        }
        public static async Task Task3()
        {
            Task t3 = new Task(
                obj =>
                {
                    string t = (string)obj;
                    DoSomething(3, t, ConsoleColor.Green);
                }, "Task 3"
            );
            t3.Start();
            await t3;
            Console.WriteLine("Task 3 end.........");
        }
        public static async Task<string> Task4()
        {
            Task<string> t4 = new Task<string>(
               () =>
               {
                   DoSomething(5, "Task 4", ConsoleColor.Red);
                   return "Task 4 result";
               }
           );
            t4.Start();
             var rel = await t4;
            Console.WriteLine("Task 4 end.........");
            return rel;
        }
        static async Task Main(string[] args)
        {
            var t4 = await Task4();
            var t2 = Task2();
            Console.WriteLine(t4);
            var t3 = Task3();
            DoSomething(2, "Task 1", ConsoleColor.Blue);
            await t2;
            await t3;
            Console.WriteLine("Hello World!");
        }
    }
}
