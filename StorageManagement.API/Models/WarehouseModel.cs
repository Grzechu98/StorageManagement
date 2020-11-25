using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Models
{
    public class WarehouseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string UnitNumber { get; set; }
        [Required]
        public string PostCode { get; set; }

        public ICollection<StorageRackModel> StorageRacks { get; set; }
    }
}
