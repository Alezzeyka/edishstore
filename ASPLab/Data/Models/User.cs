using System;
using System.ComponentModel.DataAnnotations;

namespace ASPLab.Data.Models
{
    public class User
    {
        public Guid ID { get; set; }
        [Display(Name = "Введите имя")]
        public string Name { get; set; }
        [Display(Name = "Введите логин")]
        public string Login { get; set; }
        [Display(Name = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Введите адрес почты")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
