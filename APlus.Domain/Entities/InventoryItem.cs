using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.Domain.Entities
{
    public class InventoryItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? LastUpdated { get; set; }

        public string AddedBy { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
