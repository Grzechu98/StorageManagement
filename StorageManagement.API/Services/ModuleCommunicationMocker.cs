using Newtonsoft.Json.Linq;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public interface IModuleCommunicationMocker
    {
        Task<ProductModel> MockProduct();
        Task<ContractorModel> MockContractor();
    }
    public class ModuleCommunicationMocker : IModuleCommunicationMocker
    {
        public async Task<ContractorModel> MockContractor()
        {
            ContractorModel contractor = null;
            JObject jobject = JObject.Parse(File.ReadAllText(@"contractor.json"));
            contractor = new ContractorModel { Name = jobject["name"].ToString(), NIP = jobject["NIP"].ToString(), Racks = new List<StorageRackModel>() };
            return contractor;
        }

        public async Task<ProductModel> MockProduct()
        {
            ProductModel product = null;
            JObject jobject = JObject.Parse(File.ReadAllText(@"product.json"));
            product = new ProductModel { Name = jobject["name"].ToString(), Value = Decimal.Parse(jobject["value"].ToString()) };
            return product;
        }
    }
}
