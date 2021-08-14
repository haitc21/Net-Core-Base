using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace È_DB_First.Data
{
    [Index(nameof(Productname), nameof(Price), Name = "IDX_NamePrice")]
    [Index(nameof(CateId), Name = "IX_Products_CateID")]
    public partial class Product
    {
        [Key]
        [Column("ProductID")]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string Productname { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column("CateID")]
        public int CateId { get; set; }

        [ForeignKey(nameof(CateId))]
        [InverseProperty(nameof(Category.Products))]
        public virtual Category Cate { get; set; }
    }
}
