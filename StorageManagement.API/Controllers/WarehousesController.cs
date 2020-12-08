using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StorageManagement.API.Data;
using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using StorageManagement.API.Services;

namespace StorageManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {

        private readonly IWarehouseRepository _repository;
        private readonly IStockService _stockeService;

        public WarehousesController(IWarehouseRepository repository, IStockService stockeService)
        {
            _repository = repository;
            _stockeService = stockeService;
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseModel>>> GetWarehouses()
        {
            return Ok(await _repository.GetWarehouses());
        }


        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseModel>> GetWarehouseModel(int id)
        {
            var warehouseModel = await _repository.GetWarehouse(id);

            if (warehouseModel == null)
            {
                return NotFound();
            }

            return warehouseModel;
        }

        // GET: api/Warehouses/5/stockstatus
        [HttpGet("{id}/stockstatus")]
        public async Task<ActionResult<string>> GetStockStatus(int id)
        {
            return Ok(await _stockeService.GetStockStatus(id));
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseModel(int id, WarehouseModel warehouseModel)
        {
            if (id != warehouseModel.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateWarehouse(warehouseModel);
            }
            catch (DbUpdateConcurrencyException)
            {                
                    return NotFound();
            }

            return NoContent();
        }

        // POST: api/Warehouses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WarehouseModel>> PostWarehouseModel(WarehouseModel warehouseModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.AddWarehouse(warehouseModel);

            return CreatedAtAction("GetWarehouseModel", new { id = warehouseModel.Id }, warehouseModel);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WarehouseModel>> DeleteWarehouseModel(int id)
        {
            var warehouseModel = await _repository.GetWarehouse(id);
            if (warehouseModel == null)
            {
                return NotFound();
            }
            await _repository.DeleteWarehouse(warehouseModel);

            return warehouseModel;
        }
    }
}
