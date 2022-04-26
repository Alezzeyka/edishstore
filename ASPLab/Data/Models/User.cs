using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASPLab.Data.Models
{
    public class User
    {
        public Guid ID { get; set; }
        
        
        [Display(Name = "Введите имя")]
        [StringLength(25)]
        [Required(ErrorMessage = "Укажите имя")]
        public string Name { get; set; }
        
        
        [Display(Name = "Введите логин")]
        [StringLength(25)]
        [Required(ErrorMessage = "Укажите логин")]
        public string Login { get; set; }
        
        
        [Display(Name = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(16)]
        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }
        
        
        [Display(Name = "Введите адрес почты")]
        [DataType(DataType.EmailAddress)]
        [StringLength(25)]
        [Required(ErrorMessage = "Укажите адрес почты")]
        public string Email { get; set; }
        
        
        [Display(Name = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12)]
        [Required(ErrorMessage = "Укажите номер телефона")]
        public string PhoneNumber { get; set; }
    }
}
