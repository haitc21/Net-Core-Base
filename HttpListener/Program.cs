using System;

namespace HttpListener
{
    class Program
    {
        static void Main(string[] args)
        {
            if (HttpListener.IsSupported)
            {
                Console.WriteLine("Support HttpListener");
            }
        }
    }
}
