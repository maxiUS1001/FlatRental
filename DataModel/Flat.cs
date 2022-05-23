using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlatRental.DataModel
{
    public partial class Flat
    {
        public Flat()
        {
            Leases = new HashSet<Lease>();
        }

        public int FlatId { get; set; }

        [Required(ErrorMessage = "Выберите станцию метро")]
        public string? Metro { get; set; }

        [Required(ErrorMessage = "Выберите район")]
        public string? District { get; set; }

        [Required(ErrorMessage = "Выберите микрорайон")]
        public string? Мicrodistrict { get; set; }

        [Required(ErrorMessage = "Введите количество комнат")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Кол-во комнат | Формат неверный. \n")]
        public int? NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Выберите тип аренды")]
        public string? RentalType { get; set; }

        [Required(ErrorMessage = "Введите площадь")]
        [RegularExpression(@"^\d+([.,]\d{1,2})?$", ErrorMessage = "Площадь | Формат неверный. \n")]
        public decimal? Area { get; set; }

        [Required(ErrorMessage = "Заполните поле 'Санузел'")]
        public string? Toilet { get; set; }

        [Required(ErrorMessage = "Выберите балкон")]
        public string? Balcony { get; set; }

        [Required(ErrorMessage = "Введите этаж")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Этаж | Формат неверный. \n")]
        public int? Floor { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [RegularExpression(@"^\d+([.,]\d{1,2})?$", ErrorMessage = "Цена | Формат неверный. \n")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Напишите описание")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Вставьте ссылку на изображение")]
        public string? Image { get; set; }

        public virtual ICollection<Lease> Leases { get; set; }
    }
}
