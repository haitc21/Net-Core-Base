using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinQ
{
    public class Brand
    {
        public string Name { set; get; }
        public int ID { set; get; }
    }
    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }         // tên
        public double Price { set; get; }        // giá
        public string[] Colors { set; get; }     // cá màu
        public int Brand { set; get; }           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
           => $"{ID,3} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var brands = new List<Brand>() {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty DDD"},
                };

            var products = new List<Product>(){
                new Product(1, "Bàn trà 1",    250, new string[] {"Đen", "Vàng"},         3),
                new Product(1, "Bàn trà 2",    300, new string[] {"Đen", "Xanh"},         2),
                new Product(1, "Bàn trà 3",    400, new string[] {"Xám", "Xanh"},         3),
                new Product(1, "Bàn trà 4",    400, new string[] {"Đen", "Xanh"},         2),
                new Product(1, "Bàn trà 5",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(1, "Bàn trà 6",    100, new string[] {"Đen", "Xanh"},         2),
                new Product(1, "Bàn trà 7",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
                };
            var mangSo = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 9 };

            // Select
            // var kq = products.Select(p => new { Ten = p.Name, Gia = p.Price });
            // foreach(var p in kq)
            // {
            //     Console.WriteLine($"Ten: {p.Ten} Gia: {p.Gia}");
            // }

            //SelectMany
            // var kq = products.SelectMany(p => p.Colors);
            // foreach (var p in kq)
            // {
            //     Console.WriteLine(p);
            // }

            // Min, Max, Sum, Average
            // Console.WriteLine($"Min: {mangSo.Min()}");
            // Console.WriteLine($"Max: {mangSo.Max()}");
            // Console.WriteLine($"Sum: {mangSo.Sum()}");
            // Console.WriteLine($"Average: {mangSo.Average()}");
            //Console.WriteLine($"Tong gia cac sp: {products.Sum(p => p.Price)}");

            // Join
            // var kq = products.Join(brands, p => p.Brand, b => b.ID, (p, b) => new
            // {
            //     TenSanPhan = p.Name,
            //     NhanHieu = b.Name
            // });

            // foreach (var p in kq)
            // {
            //     Console.WriteLine(p);
            // }

            // GroupJoin
            // var kq = brands.GroupJoin(products, b => b.ID, p => p.Brand, (b, p) => new
            // {
            //     NhanHieu = b.Name,
            //     SanPham = p
            // });

            // foreach (var gr in kq)
            // {
            //     Console.WriteLine(gr.NhanHieu);
            //     foreach (var sp in gr.SanPham)
            //     {
            //         Console.WriteLine(sp);
            //     }
            // }

            // Group
            // var gruop = products.GroupBy(p => p.Brand);
            // foreach (var g in gruop)
            // {
            //     Console.WriteLine(g.Key);
            //     foreach (var p in g)
            //     {
            //         Console.WriteLine(p);
            //     }
            // }

            // Single, SingleOrDèault
            // var pro = products.Single(p => p.Price == 1000);
            // Console.WriteLine(pro);

            // products.Where(p => p.Price >= 100 && p.Price < 400)
            //             .OrderByDescending(p => p.Price)
            //             .Join(brands, p => p.Brand, b => b.ID, (pro, bra) =>
            //             new { TenSP = pro.Name, Gia = pro.Price, NhanHieu = bra.Name })
            //             .ToList()
            //             .ForEach(x => Console.WriteLine(x));

            // truy van LINQ
            // var query = from p in products
            //             select p.Name;
            // Console.WriteLine(string.Join(", ", query));

            // var query2 = from p in products
            //              from color in p.Colors
            //              where p.Price < 400 && color == "Xanh"
            //              orderby p.Price descending
            //              select new
            //              {
            //                  Ten = p.Name,
            //                  Gia = p.Price,
            //                  Mau = string.Join(", ", p.Colors)
            //              };
            // Console.WriteLine(string.Join("\n", query2));

            //Left join
            // var query3 = from p in products
            //             join b in brands on p.Brand equals b.ID into t
            //             from b2 in t.DefaultIfEmpty()
            //             select new 
            //             {
            //                 TenSP = p.Name,
            //                 Gia = p.Price,
            //                 NhanHieu = b2 != null ? b2.Name : "Khong co thuong hieu"
            //             };
            // query3.ToList().ForEach(x => Console.WriteLine(x));

            var query4 = from p in products
                         join b in brands on p.Brand equals b.ID into t
                         from b2 in t.DefaultIfEmpty()
                         group new { b2, p } by new
                         {
                             BrandName = (b2 != null) ? b2.Name : "Khong co thuong hieu",
                             p.Price,
                             p.Name
                         } into gr
                         select new
                         {
                             TenSP= gr.Key.Name,
                             TenNhanHieu = gr.Key.BrandName,
                             Gia = gr.Key.Price
                         };
            query4.ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
