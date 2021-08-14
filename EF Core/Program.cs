using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EF_Core
{
    class Program
    {
        public static async Task CreatDb()
        {
            using var dbContext = new AppDbContext();
            string dbName = dbContext.Database.GetDbConnection().Database;
            var result = await dbContext.Database.EnsureCreatedAsync();
            if (result)
                Console.WriteLine($"Da tao Db: {dbName}");
            else
                Console.WriteLine("Khong tao duoc DB");
        }
        public static async Task DeleteDb()
        {
            using var dbContext = new AppDbContext();
            string dbName = dbContext.Database.GetDbConnection().Database;
            var result = await dbContext.Database.EnsureDeletedAsync();
            if (result)
                Console.WriteLine($"Da xoa Db: {dbName}");
            else
                Console.WriteLine("Khong xoa duoc DB");
        }
        public static async Task InsertProduct(Product product)
        {
            using var context = new AppDbContext();
            await context.Products.AddAsync(product);

            // EntityEntry<Product> entry = context.Entry(product); // doi tuong nay theo doi su thay doi cua entity 
            // entry.State = EntityState.Detached; // no se khong theo doi su thay doi cua product nua  => savechange k duoc gi
            // var sp1 = new Product();
            // sp1.Name = "San pham 5";
            // sp1.Provider = "Nha cung cap 5";
            // await context.Products.AddAsync(sp1); // cai entry chi theo doi doi tuong product
            // // San pham 5 van luu thanh cong

            int result = await context.SaveChangesAsync(); // tra ve so ban ghi bi tac dong

            if (result > 0)
                Console.WriteLine($"So ban ghi bi tac dong: {result}");
            else
                Console.WriteLine("Them san pham that bai!");

        }
        public static async Task<List<Product>> GetAllProducts()
        {
            using var context = new AppDbContext();
            return context.Products.ToList();
        }
        static async Task Main(string[] args)
        {
             await DeleteDb();
             await CreatDb();
        }
    }
}
