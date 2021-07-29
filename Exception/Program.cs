using System;

namespace Exception
{
    class Program
    {
        public static void DangKy(string name, int age)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.Exception("Ten khong hop le");
            }
            Console.WriteLine($"Name: {name} Age: {age}");
        }
        static void Main(string[] args)
        {
            while (true)
            {
                // string n = Console.ReadLine();
                // int a = Int32.Parse(Console.ReadLine());
                // DangKy(n, a);
                               Console.WriteLine("ASDSADSA");
            }
        }
    }
}
