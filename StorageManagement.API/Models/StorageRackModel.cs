using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Models
{
    public class StorageRackModel
    {
        public int Id { get; set; }
        public string RackNumber { get; set; }
        public bool IsTaken { get; set; }
        public ContractorModel Contractor { get; set; }
        [Required]
        public WarehouseModel Warehouse { get; set; }
        public ICollection<ShelfModel> Shelves { get; set; }
    }
}
