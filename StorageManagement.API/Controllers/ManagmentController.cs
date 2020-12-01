using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("PlaceProduct/{Name}")]
        public async Task PlaceProduct(string Name, [FromBody] int warehouseId) {
            await _managmentService.AllocateProductToStoragePlace(await _communicationService.GetProduct(Name), warehouseId);
        }

        [HttpPost("PlaceContractorProduct")]
        public async Task PlaceProduct([FromBody] string NIP, [FromBody] string Name, [FromBody] int warehouseId)
        {
            await _managmentService.AllocateProductToStoragePlace(await _communicationService.GetProduct(Name),await _communicationService.GetContractor(NIP), warehouseId);
        }
        [HttpPost("ContractorRack")]
        public async Task AssignContractorRack([FromBody] string NIP, [FromBody] int warehouseId)
        {
            await _managmentService.AssingRackToContractor(await _communicationService.GetContractor(NIP), warehouseId);
        }
    }
}
