using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public interface IValidator
    {
        Task<bool> IsRackNumberInDb(StorageRackModel model);
        Task<bool> IsShelfNumberInDb(ShelfModel model);
    }
    public class Validator : IValidator
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IStorageRackRepository _storageRepository;
        public Validator(IWarehouseRepository warehouseRepository, IStorageRackRepository storageRepository)
        {
            _warehouseRepository = warehouseRepository;
            _storageRepository = storageRepository;
        }
        public async Task<bool> IsRackNumberInDb(StorageRackModel model)
        {
            var warehouse = await _warehouseRepository.GetWarehouse(model.WarehouseId);
            return warehouse.StorageRacks.Where(sr => sr.RackNumber == model.RackNumber).Any();
        }

        public async Task<bool> IsShelfNumberInDb(ShelfModel model)
        {
            var rack = await _storageRepository.GetStorageRack(model.RackId);
            return rack.Shelves.Where(sr => sr.ShelfNumber == model.ShelfNumber).Any();
        }
    }
}
