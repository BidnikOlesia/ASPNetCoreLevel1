using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class Employee:Entity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string MiddleName { get; set; }

        public int Age { get; set; }

        public DateTime EmploymentDate { get; set; }

        public string Position { get; set; }

        public override string ToString()
        {
            return $"(id: {Id}) {LastName} {FirstName} {MiddleName}";
        }
    }
}
