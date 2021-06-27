using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Entities.Orders
{
    public class Order:NamedEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        [MaxLength(200)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    }
}
