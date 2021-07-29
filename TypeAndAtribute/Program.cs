using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace TypeAndAtribute
{
    class Program
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
        class MoTaAttribute : Attribute
        {
            public MoTaAttribute(string d)
            {
                Des = d;
            }
            public string Des { get; set; }
        }

        [MoTa("Lop chua thong tin nguoi dung")]
        class User
        {
            [MoTa("Ten nguoi dung")]
            [Required(ErrorMessage = "Ten khong duoc de trong")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Ten >= 3 va <= 50")]
            public string Name { get; set; }

            [MoTa("Tuoi nguoi dung")]
            [Range(18, 60)]
            public int Age { get; set; }
            [MoTa("TDia chi email")]
            [EmailAddress(ErrorMessage = "Email k dung dinh dang")]
            public string Email { get; set; }
            [MoTa("TSDT")]
            [Phone(ErrorMessage = "sdt K DUNG DINH DANG")]
            public string PhoneNumber { get; set; }

            [MoTa("Phuong thuc in thong tin nguoi dung")]
            public void Print()
            {
                Console.WriteLine($"Ten: {Name}");
                Console.WriteLine($"Tuoi: {Age}");
                Console.WriteLine($"Email: {Email}");
                Console.WriteLine($"SDT: {PhoneNumber}");
            }
        }
        static void Main(string[] args)
        {
            var u = new User()
            {
                Name = "Tr",
                Age = 1,
                Email = "Tran",
                PhoneNumber = "01234569a"
            };

           //var lstPro = u.GetType().GetProperties();
            // foreach (PropertyInfo p in lstPro)
            // {
            //     string name = p.Name;
            //     var value = p.GetValue(u);
            //     var attr = p.GetCustomAttributes(false);
            //     Console.WriteLine($"{name} : {value}");
            //     if (attr != null)
            //     {
            //         Console.WriteLine("Dnah sach attribute: ");
            //         foreach (var a in attr)
            //         {
            //             MoTaAttribute mota = a as MoTaAttribute;
            //             if (a != null)
            //                 Console.WriteLine("Mo ta: " + mota.Des);
            //         }
            //     }
            // }
            // GIONG KIEU Model.IsValid
            ValidationContext context = new ValidationContext(u);
            var rel = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(u, context, rel, true);
            if(isValid == false)
            {
                rel.ForEach(
                    r => Console.WriteLine($"Property: {r.MemberNames.First()} ErrMess: {r.ErrorMessage} ")
                );
            }

        }
    }
}
