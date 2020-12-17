using StorageManagement.API.Data.Repositories;
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
        public decimal SingleProductValue { get; set; }

        public StockHelper(int Quantity, decimal SingleProductValue)
        {
            this.Quantity = Quantity;
            this.Value = Quantity * SingleProductValue;
            this.SingleProductValue = SingleProductValue;
        }
        public void CountValue()
        {
            Value = Quantity * SingleProductValue;
        }
    }


    public interface IStockService
    {
        Task<IDictionary<string, int>> GetStockStatus(int id);
        Task<IDictionary<string, int>> GetStockStatus(int id,ContractorModel model);
        Task<IDictionary<string, StockHelper>> GetContractorStockDetails(int id,ContractorModel model);
        Task<IDictionary<string, StockHelper>> GetContractorStockDetails(ContractorModel model);

    }
    public class StockService : IStockService
    {
        private readonly IWarehouseRepository _repository;
        public StockService(IWarehouseRepository repository)
        {
            _repository = repository;
        }
        public async Task<IDictionary<string, int>> GetStockStatus(int id)
        {
            IDictionary<string, int> status = new Dictionary<string, int>();
            var warehouse = await _repository.GetWarehouse(id);
            
            int total = CountWarehouseTotalStorageSpace(warehouse.StorageRacks);
            int free = CountWarehouseFreeStorageSpace(warehouse.StorageRacks);
            status.Add("FreeSpace", free);
            status.Add("TakenSpace", (total - free));
            status.Add("TotalSpace", total);

            return status;
        }
        public async Task<IDictionary<string,int>> GetStockStatus(int id, ContractorModel model)
        {
            IDictionary<string, int> status = new Dictionary<string, int>();
            var warehouse = await _repository.GetWarehouse(id);
            var racks = warehouse.StorageRacks.Where(w => w.Contractor == model).AsEnumerable();
            int free = CountWarehouseFreeStorageSpace(racks);
            int total = CountWarehouseTotalStorageSpace(racks);
            status.Add("FreeSpace", free);
            status.Add("TakenSpace", (total-free));
            status.Add("TotalSpace", total);


            return status;
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
                var test = item.Shelves.Where(s => s.Product == null).ToList().Count;
                freespace += test;
            }
            
            return freespace;
        }

        public async Task<IDictionary<string, StockHelper>> GetContractorStockDetails(int id, ContractorModel model)
        {
            var warehouse = await _repository.GetWarehouse(id);

            IDictionary<string, StockHelper> result = new Dictionary<string, StockHelper>();
            var contractorRacks = warehouse.StorageRacks.Where(w => w.Contractor == model);
            foreach (var item in contractorRacks)
            {
                foreach (var i in item.Shelves)
                {
                    if (i.Product == null)
                    {

                    }
                    else
                    {
                        if (result.ContainsKey(i.Product.Name))
                        {
                            result[i.Product.Name].Quantity++;
                            result[i.Product.Name].CountValue();
                        }
                        else
                        {
                            result.Add(i.Product.Name, new StockHelper(i.Quantity, i.Product.Value));
                        }
                    }
                }
            }
            return result;
        }

        public async Task<IDictionary<string, StockHelper>> GetContractorStockDetails(ContractorModel model)
        {
            var warehouses = await _repository.GetWarehouses();

            IDictionary<string, StockHelper> result = new Dictionary<string, StockHelper>();

            foreach (var item in warehouses)
            {
                IDictionary<string, StockHelper> temp = await GetContractorStockDetails(item.Id, model);
                foreach (var i in temp)
                {
                    if (result.ContainsKey(i.Key))
                    {
                        result[i.Key].Quantity++;
                        result[i.Key].CountValue();
                    }
                    else
                    {
                        result.Add(i.Key, new StockHelper(i.Value.Quantity, i.Value.SingleProductValue));
                    }
                }
            }

            return result;
        }
    }
}
