using System;

namespace Func
{
    class Program
    {
        public static int Hieu(int a, int b)
        {
            return a - b;
        }
        public static int Tong(int a, int b)
        {
            return a + b;
        }
        public static int TinhToan(int x, int y, Func<int, int, int> phepTinh)
        {
            int kq = phepTinh.Invoke(x, y);
            return kq;
        }
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Info: " +message);
            Console.ResetColor();
        }
        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + message);
            Console.ResetColor();
        }
        public static void Print(string s, Action<string> action)
        {
            action.Invoke(s);
        }
        static void Main(string[] args)
        {
            int a = 5;
            int b = 10;
            int tong = TinhToan(a, b, Tong);
            int hieu = TinhToan(a, b, Hieu);

            // Console.WriteLine($"Tong: {tong}");
            // Console.WriteLine($"Hieu: {hieu}");
            Print($"Tong: {tong}",Info);
            Print($"Hieu: {hieu}",Warning);
        }
    }
}
