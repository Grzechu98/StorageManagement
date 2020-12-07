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
        Task<ContractorModel> AssingRackToContractor(ContractorModel contractor, int warehouseId);
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
            var shelf = await FindEmptySpace(warehouseId, contractor);
            if (shelf != null)
            {
                product.Shelf = shelf;
                product.Shelf.Quantity = 1;
                product.Shelf.Rack.CheckIfHasSpace();
                await _productRepository.UpdateProduct(product);
            }
            return product;
        }

        public async Task<ProductModel> AllocateProductToStoragePlace(ProductModel product, int warehouseId)
        {
            var shelf = await FindEmptySpace(warehouseId);
            if (shelf != null)
            {
                product.Shelf = shelf;
                product.Shelf.Quantity = 1;
                product.Shelf.Rack.CheckIfHasSpace();
                await _productRepository.UpdateProduct(product);
            }
            return product;
        }

        public async Task<ContractorModel> AssingRackToContractor(ContractorModel contractor, int warehouseId)
        {
            var rack = await FindEmptyRack(warehouseId);
            if (rack != null)
            {
                contractor.Racks.Add(rack);
                await _contractorRepository.UpdateContractor(contractor);
            }
            return contractor;
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
