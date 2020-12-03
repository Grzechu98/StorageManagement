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
        Task<ProductModel> GetProductFromProductsModule(int Id);
        Task<ContractorModel> GetContractor(string NIP);
        Task<ProductModel> GetProduct(int Id);

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

        public async Task<ProductModel> GetProduct(int Id)
        {
            return await GetProductFromProductsModule(Id);
        }

        public async Task<ProductModel> GetProductFromProductsModule(int Id)
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
