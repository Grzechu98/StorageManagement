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
    public class StorageRacksController : ControllerBase
    {
        private readonly IStorageRackRepository _repository;
        private readonly IValidator _validator;
        private readonly IDeleteService _deleteService;

        public StorageRacksController(IStorageRackRepository repository, IValidator validator, IDeleteService deleteService)
        {
            _repository = repository;
            _validator = validator;
            _deleteService = deleteService;
        }
        // GET: api/StorageRacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageRackModel>> GetStorageRackModel(int id)
        {
            var storageRackModel = await _repository.GetStorageRack(id);

            if (storageRackModel == null)
            {
                return NotFound();
            }

            return storageRackModel;
        }

        // PUT: api/StorageRacks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageRackModel(int id, StorageRackModel storageRackModel)
        {
            if (id != storageRackModel.Id || !ModelState.IsValid || await _validator.IsRackNumberInDb(storageRackModel))
            {
                return BadRequest();
            }
            try
            {
                await _repository.UpdateStorageRack(storageRackModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/StorageRacks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StorageRackModel>> PostStorageRackModel(StorageRackModel storageRackModel)
        {
            if (!ModelState.IsValid || await _validator.IsRackNumberInDb(storageRackModel))
            {
                return BadRequest();
            }

            await _repository.AddStorageRack(storageRackModel);
            return CreatedAtAction("GetStorageRackModel", new { id = storageRackModel.Id }, storageRackModel);
        }

        // DELETE: api/StorageRacks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StorageRackModel>> DeleteStorageRackModel(int id)
        {
            var storageRackModel = await _repository.GetStorageRack(id);
            if (storageRackModel == null)
            {
                return NotFound();
            }
            if (await _deleteService.CanDeleteRack(storageRackModel))
            {
                await _repository.DeleteStorageRack(storageRackModel);

                return storageRackModel;
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
