using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Models
{
    public class ShelfModel
    {
        public int Id { get; set; }
        public string ShelfNumber { get; set; }
        public int Quantity { get; set; }
        [Required]
        public StorageRackModel Rack { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
