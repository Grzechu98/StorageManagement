using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using StorageManagement.API.Models;
using StorageManagement.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StorageManagement.API.Controllers
{
    [Route("api/Managment")]
    [ApiController]
    public class ManagmentController : ControllerBase
    {
        private readonly IModuleCommunicationService _communicationService;
        private readonly IManagmentService _managmentService;
        public ManagmentController(IModuleCommunicationService communicationService, IManagmentService managmentService)
        {
            _communicationService = communicationService; 
            _managmentService = managmentService;
        }

        [HttpPost("PlaceProduct/{Id}")]
        public async Task<ActionResult<ProductModel>> PlaceProduct(int Id, [FromBody] JObject data) {
            var res = await _managmentService.AllocateProductToStoragePlace(await _communicationService.GetProduct(Id), Int32.Parse(data["warehouseId"].ToString()));
            if (res.Shelf == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpPost("PlaceContractorProduct")]
        public async Task<ActionResult<ProductModel>> PlaceProduct([FromBody] JObject data)
        {
            var res = await _managmentService.AllocateProductToStoragePlace(await _communicationService.GetProduct(Int32.Parse(data["Id"].ToString())),
                await _communicationService.GetContractor(data["NIP"].ToString()), Int32.Parse(data["warehouseId"].ToString()));
            if (res.Shelf == null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        [HttpPost("ContractorRack")]
        public async Task<IActionResult> AssignContractorRack([FromBody] JObject data)
        {
            var contractor = await _communicationService.GetContractor(data["NIP"].ToString());
            if (contractor == null)
            {
                return BadRequest();
            }
            var rack = await _managmentService.AssingRackToContractor(contractor, Int32.Parse(data["warehouseId"].ToString()));
            return Ok(rack);
        }
    }
}
