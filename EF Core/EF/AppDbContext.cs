using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace EF
{
    public class AppDbContext : DbContext
    {

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
         {
             builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
             // builder.AddFilter(DbLoggerCategory.Database.Name, LogLevel.Information);
             builder.AddConsole();
         });
        private const string connectionString = @"
                                                Server = localhost,1433;
                                                Database = AppDb;
                                                UID = sa;
                                                PWD =  Password123";
        /// <summary>
        /// Cai nay hay goi trong file startup.cs
        /// </summary>
        /// <param name="optionBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            Console.WriteLine("OnConfiguring");
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseLoggerFactory(loggerFactory); // moi lan truy van DB thi no log lai
            optionBuilder.UseSqlServer(connectionString);
            // Microsoft.EntityFrameWorkCore.Proxies
            // k nen dung cai LazyLoad nay vi no rat nang
            // optionBuilder.UseLazyLoadingProxies(); // tu dong load cac Referent,Collection Navigation (may cai doi tuong khoa ngoai)
        }
        /// <summary>
        /// thuc thi sau khi OnConfiguring 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.ProductID);
                // danh indexer
                entity.HasIndex(p => new { p.Name, p.Price })
                    .HasDatabaseName("IDX_NamePrice");
                // Khoa 1 - n
                entity.HasOne(p => p.Categories)
                    .WithMany(c => c.Products) // CHI RA PHAN NHIEU BEN BANG CHA
                    .HasForeignKey(p => p.CateID) // Dat ten truong tao ra HasForeignKey
                    .OnDelete(DeleteBehavior.Cascade) // khi xoa bang cha 1 thi xoa het bang con n
                    .HasConstraintName("FK_P_C") // DAT TEN CAI QUAN HE
                    ;
                    
            // Cau hinh properties
            entity.Property(p => p.Name)
                    .HasColumnName("Productname")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasDefaultValue("San pham khong ro ten");
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.CategoryID);
            });

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}