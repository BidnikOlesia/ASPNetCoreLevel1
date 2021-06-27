using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewsModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов")]
        [RegularExpression(@"([А-ЯЁ][ф-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат имени")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указана")]
        [StringLength(50, ErrorMessage = "Длина отчества должна быть от 2 до 50 символов")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть до 50 символов")]
        public string MiddleName { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Возраст должен быть от 18 до 80 лет")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата трудоустройства")]
        public DateTime EmploymentDate { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }
    }
}
