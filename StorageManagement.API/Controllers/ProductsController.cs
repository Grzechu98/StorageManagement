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

namespace StorageManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
            var productModel = await _repository.GetProduct(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel);
        }

        // GET: api/Products/NAME
        [HttpGet("{Name}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductModel(string name)
        {
            var productModel = await _repository.GetProducts(e => e.Name == name);

            if (productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel);
        }
    }
}
