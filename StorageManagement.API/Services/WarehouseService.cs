using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public interface IWarehouseService
    {
        Task<string> GetStockStatus(int id);
    }
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;
        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> GetStockStatus(int id)
        {
            var warehouse = await _repository.GetWarehouse(id);
            
            int total = CountWarehouseTotalStorageSpace(warehouse);
            int free = CountWarehouseFreeStorageSpace(warehouse);

            return free + "/" + total;
        }

        private int CountWarehouseTotalStorageSpace(WarehouseModel model)
        {
            int totalspace = 0;
            
            foreach (var item in model.StorageRacks)
            {
                totalspace += item.Shelves.Count;
            }
            
            return totalspace;
        }

        private int CountWarehouseFreeStorageSpace(WarehouseModel model)
        {
            int freespace = 0;
            
            var racks = model.StorageRacks.Where(e => e.IsTaken == false).AsEnumerable();
           
            foreach (var item in racks)
            {
                freespace += item.Shelves.Where(s => s.Product == null).ToList().Count;
            }
            
            return freespace;
        }
    }
}
