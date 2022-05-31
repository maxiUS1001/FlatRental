using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlatRental.DataModel
{
    public partial class User
    {
        public User()
        {
            Leases = new HashSet<Lease>();
        }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Email | Введите ваш Email\n")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Email | Формат неверный. \n")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Пароль | Введите пароль \n")]
        [RegularExpression(@"^([a-zA-Z0-9]*)$", ErrorMessage = "Пароль | Формат неверный. \n")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Пароль | Минимальная длина 4 символа, максимальная 15 \n")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Имя | Введите ваше Имя\n")]
        [RegularExpression(@"^([a-zA-Zа-яА-ЯёЁ]{2,15})$", ErrorMessage = "Имя | Формат неверный. \n")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Телефон | Введите номер телефона \n")]
        [RegularExpression(@"^\+375(29|33|44|25)[0-9]{7}$", ErrorMessage = "Телефон | Формат неверный.\n")]
        public string? PhoneNumber { get; set; }

        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
    }
}
