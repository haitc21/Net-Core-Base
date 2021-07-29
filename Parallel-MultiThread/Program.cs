using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_MultiThread
{
    class Program
    {
        //In thông tin, Task ID và thread ID đang chạy
        public static void PintInfo(string info) =>
                Console.WriteLine($"{info,10}    task:{Task.CurrentId,3}    " +
                                  $"thread: {Thread.CurrentThread.ManagedThreadId}");
        // Phương thức phù hợp với Action<int>, được làm tham số action của Parallel.For
        public static async void RunTask(int i)
        {
            PintInfo($"Start {i,3}");
           // Task.Delay(1000).Wait();
            await Task.Delay(1000);              // Task dừng 1s - rồi mới chạy tiếp
            PintInfo($"Finish {i,3}");
        }

        public static void ParallelFor()
        {
            ParallelLoopResult result = Parallel.For(1, 10, RunTask);   // Vòng lặp tạo ra 20 lần chạy RunTask
            Console.WriteLine($"All task start and finish: {result.IsCompleted}");
        }
        static void Main(string[] args)
        {

            Stopwatch sw;
            sw = Stopwatch.StartNew();
            ParallelFor();
            Console.WriteLine($"Thoi gian: {sw.ElapsedMilliseconds}");
            sw.Stop();
            Console.WriteLine("Press any key ..."); Console.ReadKey();
        }
    }
}
