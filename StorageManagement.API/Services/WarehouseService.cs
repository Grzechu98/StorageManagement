﻿using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public class StockHelper {
        public int Quantity { get; set; }
        public decimal Value { get; set; }

        public StockHelper(int Quantity, decimal SingleProductValue)
        {
            this.Quantity = Quantity;
            this.Value = Quantity * SingleProductValue;
        }
        public void CountValue(decimal value)
        {
            Value = Quantity * value;
        }
    }


    public interface IWarehouseService
    {
        Task<string> GetStockStatus(int id);
        Task<string> GetStockStatus(int id,ContractorModel model);
        Task<ICollection<StockHelper>> GetContractorStockDetails(int id,ContractorModel model);
        
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
            
            int total = CountWarehouseTotalStorageSpace(warehouse.StorageRacks);
            int free = CountWarehouseFreeStorageSpace(warehouse.StorageRacks);

            return free + "/" + total;
        }
        public async Task<string> GetStockStatus(int id, ContractorModel model)
        {
            var warehouse = await _repository.GetWarehouse(id);

            int total = CountWarehouseTotalStorageSpace(warehouse.StorageRacks.Where(w => w.Contractor == model).AsEnumerable());
            int free = CountWarehouseFreeStorageSpace(warehouse.StorageRacks.Where(w => w.Contractor == model).AsEnumerable());

            return free + "/" + total;
        }

        private int CountWarehouseTotalStorageSpace(IEnumerable<StorageRackModel> model)
        {
            int totalspace = 0;
            
            foreach (var item in model)
            {
                totalspace += item.Shelves.Count;
            }
            
            return totalspace;
        }

        private int CountWarehouseFreeStorageSpace(IEnumerable<StorageRackModel> model)
        {
            int freespace = 0;
            
            var racks = model.Where(e => e.IsTaken == false).AsEnumerable();
           
            foreach (var item in racks)
            {
                freespace += item.Shelves.Where(s => s.Product == null).ToList().Count;
            }
            
            return freespace;
        }

        public async Task<IDictionary<string, StockHelper>> GetContractorStockDetails(int id, ContractorModel model)
        {
            var warehouse = await _repository.GetWarehouse(id);

            IDictionary<string, StockHelper> result = new Dictionary<string, StockHelper>();

            foreach (var item in warehouse.StorageRacks)
            {
                foreach (var i in item.Shelves)
                {
                    if (result.ContainsKey(i.Product.Name))
                    {
                        result[i.Product.Name].Quantity++;
                        result[i.Product.Name].CountValue(i.Product.Value);
                    }
                    else
                    {
                        result.Add(i.Product.Name, new StockHelper(i.Quantity, i.Product.Value));
                    }
                }
            }
            return result;


        }
    }
}
