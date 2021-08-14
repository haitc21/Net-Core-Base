using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace È_DB_First.Data
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("CategoryID")]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Description { get; set; }

        [InverseProperty(nameof(Product.Cate))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
