using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public async Task PlaceProduct(int Id, [FromBody] JObject data) {
            
            await _managmentService.AllocateProductToStoragePlace(await _communicationService.GetProduct(Id), Int32.Parse(data["warehouseId"].ToString()));
        }

        [HttpPost("PlaceContractorProduct")]
        public async Task PlaceProduct([FromBody] JObject data)
        {
            await _managmentService.AllocateProductToStoragePlace(await _communicationService.GetProduct(Int32.Parse(data["Id"].ToString())),await _communicationService.GetContractor(data["NIP"].ToString()), Int32.Parse(data["warehouseId"].ToString()));
        }
        [HttpPost("ContractorRack")]
        public async Task AssignContractorRack([FromBody] JObject data)
        {
            await _managmentService.AssingRackToContractor(await _communicationService.GetContractor(data["NIP"].ToString()), Int32.Parse(data["warehouseId"].ToString()));
        }
    }
}
