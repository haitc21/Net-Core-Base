using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF
{
    //  [Table("Categories")]
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public decimal Description { get; set; }

        // Collect Nagigation
        // cai nay chi dieu huong quan he den doi tuong khac hien
        // K co cha sao
        // Khi muon lay dl trwong nay khi da co doi tuong thi dung 
        // context.Entry(doituong).Collection(doituong).Load();
        // Co the dung lazzyload de no tu load . Them tu khoa virtual trong property
        public virtual List<Product> Products { get; set; }
    }
}