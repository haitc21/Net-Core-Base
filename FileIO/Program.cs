using System;
using System.IO;
using System.Text;

namespace FileIO
{
    class Program
    {
        /// <summary>
        /// In các thông tin ổ đĩa trong máy
        /// </summary>
        public static void GetDrivesInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);
                    Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);
                    Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                }
            }
        }
        /// <summary>
        /// Lay danh sach tat ca file va thu muc
        /// </summary>
        /// <param name="path"></param>
        static void ListFileDirectory(string path)
        {
            String[] directories = System.IO.Directory.GetDirectories(path);
            String[] files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
                ListFileDirectory(directory); // Đệ quy
            }
        }
        public class Product
        {
            public Product()
            {
                this.Id = Id;
                this.Name = Name;
                this.Price = Price;
            }
            public Product(int id, double price, string name)
            {
                Id = id;
                Price = price;
                Name = name;
            }
            public int Id { get; set; }
            public Double Price { get; set; }
            public string Name { get; set; }

            public void Save(Stream stream)
            {
                var byte_id = BitConverter.GetBytes(Id);
                stream.Write(byte_id, 0, 4); // id kieu int nen luu bang 4 byte
                var byte_price = BitConverter.GetBytes(Price);
                stream.Write(byte_price, 0, 8); // price kieu double nen luu bang 8 byte

                var byte_name = Encoding.UTF8.GetBytes(Name);
                var byte_name_len = BitConverter.GetBytes(byte_name.Length);
                stream.Write(byte_name_len, 0, 4); // luu lai do dai cua name
                stream.Write(byte_name, 0, byte_name.Length);
            }
            public static Product Restore(Stream stream)
            {
                var rel = new Product();
                var byte_id = new Byte[4];
                stream.Read(byte_id, 0, 4);
                rel.Id = BitConverter.ToInt32(byte_id);

                var byte_price = new Byte[8];
                stream.Read(byte_price, 0, 8);
                rel.Price = BitConverter.ToDouble(byte_price);

                var byte_name_len = new Byte[4];
                stream.Read(byte_name_len, 0, 4);
                int name_len = BitConverter.ToInt32(byte_name_len);

                var byte_name = new Byte[name_len];
                stream.Read(byte_name, 0, name_len);
                rel.Name = Encoding.UTF8.GetString(byte_name);
                return rel;
            }
        }
        static void Main(string[] args)
        {
            // Lay thong tin o dia tren may
            Console.WriteLine("Thong tin o dia:");
            GetDrivesInfo();
            Console.WriteLine("=========================================");
            // Lay duong dan den file dac biet cua he thong
            var path_mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Console.WriteLine($"Duong dan den mydocument: {path_mydoc}");
            Console.WriteLine("=========================================");

            // lam viec voi filestream
            string path = "data.dat";
            if (!File.Exists(path))
            {
                Console.WriteLine("Tao file..");
                var sp = new Product(1, 1.99, "Pho");
                using (var stream = new FileStream(path: path, FileMode.Create))
                {
                    sp.Save(stream);
                }
                Console.WriteLine("=========================================");
            }

            using (var stream = new FileStream(path: path, FileMode.Open))
            {
                Console.WriteLine("In thong tin tu file...");
                var spInData = Product.Restore(stream);
                Console.WriteLine($"Id: {spInData.Id} Nam: {spInData.Name} Price: {spInData.Price}");
            }
            Console.WriteLine("=========================================");
            // lay danh sach tat ca folder va file trong thu muc hien tai
            Console.WriteLine("Danh sach folder va file o thu muc hien tai:");
            string pathCur = Directory.GetCurrentDirectory();
            ListFileDirectory(pathCur);
            Console.WriteLine("=========================================");

        }
    }
}
