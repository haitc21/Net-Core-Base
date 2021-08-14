using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF
{
    //  [Table("Products")]
    public class Product
    {
        //   [Key]
        //   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        //    [Required]
        // [StringLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CateID { get; set; }

        // Referent Navigation
        // Khi muon lay dl trwong nay khi da co doi tuong thi dung 
        // context.Entry(doituong).Referent(doituong).Load();

        // Co the dung lazzyload de no tu load . Them tu khoa virtual trong property
        // [ForeignKey("CateID")]
        public virtual Category Categories { get; set; }
    }
}