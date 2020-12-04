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
    public class ShelvesController : ControllerBase
    {
        private readonly IShelfRepository _repository;
        private readonly IValidator _validator;
        public ShelvesController(IShelfRepository repository, IValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        // GET: api/Shelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelfModel>> GetShelfModel(int id)
        {
            var shelfModel = await _repository.GetShelf(id);

            if (shelfModel == null)
            {
                return NotFound();
            }

            return shelfModel;
        }

        // PUT: api/Shelves/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelfModel(int id, ShelfModel shelfModel)
        {
            if (id != shelfModel.Id || !ModelState.IsValid || await _validator.IsShelfNumberInDb(shelfModel))
            {
                return BadRequest();
            }
            try
            {
                await _repository.UpdateShelf(shelfModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                 return NotFound();
            }

            return NoContent();
        }

        // POST: api/Shelves
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ShelfModel>> PostShelfModel(ShelfModel shelfModel)
        {
            if (!ModelState.IsValid || await _validator.IsShelfNumberInDb(shelfModel))
            {
                return BadRequest();
            }
            await _repository.AddShelf(shelfModel);

            return CreatedAtAction("GetShelfModel", new { id = shelfModel.Id }, shelfModel);
        }

        // DELETE: api/Shelves/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ShelfModel>> DeleteShelfModel(int id)
        {
            var shelfModel = await _repository.GetShelf(id);
            if (shelfModel == null)
            {
                return NotFound();
            }


            await _repository.DeleteShelf(shelfModel);

            return shelfModel;
        }
    }
}
