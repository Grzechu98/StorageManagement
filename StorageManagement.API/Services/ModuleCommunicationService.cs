using Newtonsoft.Json.Linq;
using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public interface IModuleCommunicationService
    {
        Task<ContractorModel> GetContractorFromContractorsModule(string NIP);
        Task<ProductModel> GetProductFromProductsModule(string Name);
        Task<ContractorModel> GetContractor(string NIP);
        Task<ProductModel> GetProduct(string Name);

    }
    public class ModuleCommunicationService : IModuleCommunicationService
    {
        private readonly IContractorRepository _contractorRepository;
        private readonly IProductRepository _productrRepository;

        public ModuleCommunicationService(IContractorRepository contractorRepository, IProductRepository productrRepository)
        {
            _contractorRepository = contractorRepository;
            _productrRepository = productrRepository;
        }

        public async Task<ContractorModel> GetContractor(string NIP)
        {
            var contractor = await _contractorRepository.GetContractor(c => c.NIP == NIP);
            if (contractor == null)
            {
                return await GetContractorFromContractorsModule(NIP);
            }
            return contractor;
        }

        public async Task<ContractorModel> GetContractorFromContractorsModule(string NIP)
        {
            ContractorModel contractor = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("");
            
            if (response.IsSuccessStatusCode)
            {
                var jobject = await response.Content.ReadAsAsync<JObject>();
                contractor = new ContractorModel { Name = jobject["name"].ToString(), NIP = jobject["NIP"].ToString() };
            }
            return contractor;

        }

        public async Task<ProductModel> GetProduct(string Name)
        {
            var product = await _productrRepository.GetProduct(p => p.Name == Name);
            if (product == null)
            {
                return await GetProductFromProductsModule(Name);
            }
            return product;
        }

        public async Task<ProductModel> GetProductFromProductsModule(string Name)
        {
            ProductModel product = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var jobject = await response.Content.ReadAsAsync<JObject>();
                product = new ProductModel { Name = jobject["name"].ToString(), Value = Decimal.Parse(jobject["value"].ToString()) };
            }
            return product;
        }
    }
}
