using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IConfiguration _config;
        private readonly IModuleCommunicationMocker _mocker;

        public ModuleCommunicationService(IModuleCommunicationMocker mocker,IConfiguration config,IContractorRepository contractorRepository, IProductRepository productrRepository)
        {
            _contractorRepository = contractorRepository;
            _productrRepository = productrRepository;
            _config = config;
            _mocker = mocker;
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
            if (_config["ContractorsAPI"].Equals("")) contractor = await _mocker.MockContractor();
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_config["ContractorsAPI"] + "/" + NIP);

                if (response.IsSuccessStatusCode)
                {
                    var jobject = await response.Content.ReadAsAsync<ContractorModel>();
                     contractor = new ContractorModel { Name = jobject.Name, NIP = jobject.NIP, Racks = new List<StorageRackModel>() };
                    await _contractorRepository.AddContractor(contractor);
                }
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
            string test = _config["ProductsAPI"];
            if (test.Equals("")) product = await _mocker.MockProduct();
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(_config["ProductsAPI"]+"/"+Id);

                if (response.IsSuccessStatusCode)
                {
                    var jobject = await response.Content.ReadAsAsync<ProductModel>();
                    product = new ProductModel { Name = jobject.Name, Value = jobject.Value };
                   await _productrRepository.AddProduct(product);
                }
            }
            
            return product;
        }
    }
}
