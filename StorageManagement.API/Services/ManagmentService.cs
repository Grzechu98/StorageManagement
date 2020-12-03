using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public interface IManagmentService
    {
        Task<ProductModel> AllocateProductToStoragePlace(ProductModel product,ContractorModel contractor, int warehouseId);
        Task<ProductModel> AllocateProductToStoragePlace(ProductModel product, int warehouseId);
        Task<ShelfModel> FindEmptySpace(int warehouseId);
        Task<ShelfModel> FindEmptySpace(int warehouseId,ContractorModel contractor);
        Task AssingRackToContractor(ContractorModel contractor, int warehouseId);
        Task<StorageRackModel> FindEmptyRack(int warehouseId);
    }
    public class ManagmentService : IManagmentService
    {
        private readonly IProductRepository _productRepository;
        private readonly IContractorRepository _contractorRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        public ManagmentService(IProductRepository productRepository, IContractorRepository contractorRepository, IWarehouseRepository warehouseRepository)
        {
            _productRepository = productRepository;
            _contractorRepository = contractorRepository;
            _warehouseRepository = warehouseRepository;
        }
        public async Task<ProductModel> AllocateProductToStoragePlace(ProductModel product, ContractorModel contractor, int warehouseId)
        {
            product.Shelf = await FindEmptySpace(warehouseId, contractor);
            product.Shelf.Quantity++;
            await _productRepository.UpdateProduct(product);
            return product;
        }

        public async Task<ProductModel> AllocateProductToStoragePlace(ProductModel product, int warehouseId)
        {
            product.Shelf = await FindEmptySpace(warehouseId);
            product.Shelf.Quantity=1;
            await _productRepository.UpdateProduct(product);
            return product;
        }

        public async Task AssingRackToContractor(ContractorModel contractor, int warehouseId)
        {
            contractor.Racks.Add(await FindEmptyRack(warehouseId));
            await _contractorRepository.UpdateContractor(contractor);   
        }

        public async Task<StorageRackModel> FindEmptyRack(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetWarehouse(warehouseId);

            foreach (var item in warehouse.StorageRacks.Where(s => s.Contractor == null && s.IsTaken == false))
            {
                foreach (var i in item.Shelves)
                {
                    if (i.Product != null) return null;
                }
                return item;
            }
            return null;
        }

        public async Task<ShelfModel> FindEmptySpace(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetWarehouse(warehouseId);
            
            foreach (var item in warehouse.StorageRacks.Where(s=>s.Contractor == null && s.IsTaken == false))
            {
                foreach (var i in item.Shelves)
                {
                    if (i.Product == null)
                    {
                        item.CheckIfHasSpace();
                        return i;
                    }
                }
            }
            return null;
        }

        public async Task<ShelfModel> FindEmptySpace(int warehouseId, ContractorModel contractor)
        {
            var warehouse = await _warehouseRepository.GetWarehouse(warehouseId);

            foreach (var item in warehouse.StorageRacks.Where(s => s.Contractor == contractor && s.IsTaken == false))
            {
                foreach (var i in item.Shelves)
                {
                    if (i.Product == null) {
                        item.CheckIfHasSpace();
                        return i; 
                    }
                    
                }
            }
            return null;
        }
    }
}
