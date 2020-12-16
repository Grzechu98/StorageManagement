using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;

namespace StorageManagement.API.Services
{
    public interface IDeleteService
    {
        Task<bool> CanDeleteWarhouse(WarehouseModel warehouse);
        Task<bool> CanDeleteRack(StorageRackModel storageRack);
        Task<bool> CanDeleteShelf(ShelfModel shelf);
    }
    public class DeleteService : IDeleteService
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public DeleteService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<bool> CanDeleteRack(StorageRackModel storageRack)
        {
            return await Task.FromResult(!storageRack.Shelves.Where(e => e.HasProduct() == true).Any());
        }

        public async Task<bool> CanDeleteShelf(ShelfModel shelf)
        {
            return await Task.FromResult(!shelf.HasProduct());
        }

        public async Task<bool> CanDeleteWarhouse(WarehouseModel warehouse)
        {
            foreach (var item in warehouse.StorageRacks)
            {
                if(!(await CanDeleteRack(item))){
                    return false;
                }
            }return true;
        }
    }
}
