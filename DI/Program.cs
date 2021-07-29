using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DI
{
    interface IClassA
    {
        public void ActionA();
    }
    interface IClassB
    {
        public void ActionB();
    }
    interface IClassC
    {
        public void ActionC();
    }

    class ClassC : IClassC
    {
        public ClassC() => Console.WriteLine("ClassC is created");
        public void ActionC() => Console.WriteLine("Action in ClassC");
    }
    class ClassC1 : IClassC
    {
        public ClassC1() => Console.WriteLine("ClassC1 is created");
        public void ActionC() => Console.WriteLine("Action in ClassC1");
    }


    class ClassB : IClassB
    {
        IClassC c_dependency;
        public ClassB(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB");
            c_dependency.ActionC();
        }
    }

    class ClassB1 : IClassB
    {
        IClassC c1_dependency;
        public ClassB1(IClassC class1)
        {
            c1_dependency = class1;
            Console.WriteLine("ClassB1 is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB1");
            c1_dependency.ActionC();
        }
    }

    class ClassB2 : IClassB
    {
        IClassC c_dependency;
        public string Mess { get; set; }

        public ClassB2(IClassC classC, string meg)
        {
            c_dependency = classC;
            Mess = meg;
            Console.WriteLine("ClassB2 is created");
        }
        public void ActionB()
        {
            Console.WriteLine(Mess);
            c_dependency.ActionC();
        }
    }
    class ClassA : IClassA
    {
        IClassB b_dependency;
        public ClassA(IClassB classb)
        {
            b_dependency = classb;
            Console.WriteLine("ClassA is created");
        }
        public void ActionA()
        {
            Console.WriteLine("Action in ClassA");
            b_dependency.ActionB();
        }
    }
    class MyService
    {
        public MyService(IOptions<MyServiceOptions> option)
        {
            var _option = option.Value;
            Data1 = _option.Data1;
            Data2 = _option.Data2;
        }
        public string Data1 { get; set; }
        public int Data2 { get; set; }

        public void Print()
        {
            Console.WriteLine($"{Data1} - {Data2}");
        }
    }
    class MyServiceOptions
    {
        public string Data1 { get; set; }
        public int Data2 { get; set; }
    }
    class Program
    {
        // Factory nhận tham số là IServiceProvider và trả về đối tượng địch vụ cần tạo
        public static ClassB2 CreateB2Factory(IServiceProvider serviceprovider)
        {
            var service_c = serviceprovider.GetService<IClassC>();
            var sv = new ClassB2(service_c, "Thực hiện trong ClassB2");
            return sv;
        }
        static void Main(string[] args)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("cauhinh.json");
            IConfigurationRoot configurationRoot = configBuilder.Build();
            var myServiceOptions = configurationRoot.GetSection("MyServiceOptions");

            var service = new ServiceCollection();

            // Neu khai bao DI Cung 1 interface thi no se ghi de cai sau len
            // AddSingleton khoi tao doi tuong 1 lan duy nhat
            // service.AddSingleton<IClassC, ClassC>();

            // AddTransient moi lan goi se  tao ra doi tuong moi
            //  service.AddTransient<IClassC, ClassC>();

            // AddScoped tao moi doi tuong khi thay doi scope
            // service.AddScoped<IClassC, ClassC>();

            // var provider = service.BuildServiceProvider();

            // for (int i = 0; i < 3; i++)
            // {
            //     var classC = provider.GetService<IClassC>();
            //     Console.WriteLine((classC.GetHashCode()));
            // }
            // using (var scope = provider.CreateScope())
            // {
            //     var provider1 = scope.ServiceProvider;
            //     for (int i = 0; i < 3; i++)
            //     {
            //         var classC = provider1.GetService<IClassC>();
            //         Console.WriteLine((classC.GetHashCode()));
            //     }
            // }
            // using (var scope = provider.CreateScope())
            // {
            //     var provider1 = scope.ServiceProvider;
            //     for (int i = 0; i < 3; i++)
            //     {
            //         var classC = provider1.GetService<IClassC>();
            //         Console.WriteLine((classC.GetHashCode()));
            //     }
            // }

            // service.AddTransient<IClassB, ClassB2>(provider =>
            // {
            //     var b2 = new ClassB2(
            //             provider.GetService<IClassC>(),
            //             "Thuc hien trong class b2"
            //             );
            //     return b2;
            // });

            // service.AddSingleton<IClassB>(CreateB2Factory);
            // service.AddTransient<IClassC, ClassC1>();
            // service.AddTransient<IClassA, ClassA>();

            service.AddTransient<MyService>();
            // service.Configure<MyServiceOptions>(
            //     option =>
            //     {
            //         option.Data1 = "Hello data1";
            //         option.Data2 = 123456;
            //     }
            // );
            service.Configure<MyServiceOptions>(myServiceOptions);


            var provider = service.BuildServiceProvider();

            // IClassA a = provider.GetService<IClassA>();
            // a.ActionA();

            MyService sv = provider.GetService<MyService>();
            sv.Print();

        }
    }
}
