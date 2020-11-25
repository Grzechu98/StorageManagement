using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Models
{
    public class ContractorModel
    {
        public int Id { get; set; }
        public string NIP { get; set; }
        public string Name { get; set; }
        public ICollection<StorageRackModel> Racks { get; set; }
    }
}
