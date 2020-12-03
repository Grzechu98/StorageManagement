using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("WarehouseId")]
        public WarehouseModel Warehouse { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        public ICollection<ShelfModel> Shelves { get; set; }

        public void CheckIfHasSpace()
        {
            IsTaken = !this.Shelves.Where(e => e.Product == null).Any();
        }
    }
}
