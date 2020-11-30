using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Data;
using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using StorageManagement.API.Services;

namespace StorageManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly IContractorRepository _repository;
        private readonly IWarehouseService _warehouseService;

        public ContractorController(IContractorRepository repository, IWarehouseService warehouseService)
        {
            _repository = repository;
            _warehouseService = warehouseService;
        }

        // GET: api/Contractor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractorModel>>> GetContractors()
        {
            return Ok(await _repository.GetContractos());
        }

        // GET: api/Contractor/9090909090/TotalStock
        [HttpGet("{NIP}/TotalStock")]
        public async Task<ActionResult<ContractorModel>> GetContractorModel(string NIP)
        {
            var stock = await _warehouseService.GetContractorStockDetails(await _repository.GetContractor(e => e.NIP == NIP));
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        // GET: api/Contractor/9090909090/WarehouseStock/1
        [HttpGet("{NIP}/WarehouseStock/{WarehouseId}")]
        public async Task<ActionResult<IDictionary<string,StockHelper>>> GetWarehouseStockDetails(string NIP,int warehouseId)
        {
            var stock = await _warehouseService.GetContractorStockDetails(warehouseId, await _repository.GetContractor(e => e.NIP == NIP));
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }


        // GET: api/Contractor/9090909090/FreeStorageSpaceInWarehouse/1
        [HttpGet("{NIP}/FreeStorageSpaceInWarehouse/{WarehouseId}")]
        public async Task<ActionResult<string>> GetFreeStorageSpaceInWarehouse(string NIP, int warehouseId)
        {
            var stockStatus = await _warehouseService.GetStockStatus(warehouseId, await _repository.GetContractor(e => e.NIP == NIP));

            return stockStatus;
        }

    }
}
