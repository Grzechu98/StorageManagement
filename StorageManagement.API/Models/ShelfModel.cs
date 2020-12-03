using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Models
{
    [Table("Shelves")]
    public class ShelfModel
    {
        public int Id { get; set; }
        public string ShelfNumber { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("RackId")]
        public StorageRackModel Rack { get; set; }
        [Required]
        public int RackId { get; set; }
        public int? ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
