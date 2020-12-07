using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Data;
using StorageManagement.API.Data.Repositories;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Services
{
    public interface IValidator
    {
        Task<bool> IsRackNumberInDb(StorageRackModel model);
        Task<bool> IsShelfNumberInDb(ShelfModel model);
    }
    public class Validator : IValidator
    {
        private readonly MainContext _context;
        
        
        public Validator(MainContext context)
        {
            _context = context;
        }
        public async Task<bool> IsRackNumberInDb(StorageRackModel model)
        {
            var warehouse = await _context.Warehouses.AsNoTracking().Include(r => r.StorageRacks).FirstOrDefaultAsync(e => e.Id == model.WarehouseId);
            if (model.Id == 0) return warehouse.StorageRacks.Where(sr => sr.RackNumber == model.RackNumber).Any();
            return warehouse.StorageRacks.Where(sr => sr.RackNumber == model.RackNumber && sr.Id != model.Id).Any();
        }

        public async Task<bool> IsShelfNumberInDb(ShelfModel model)
        {
            var rack = await _context.StorageRacks.AsNoTracking().Include(sr => sr.Shelves).FirstOrDefaultAsync(e => e.Id == model.RackId);
            if(model.Id == 0) return rack.Shelves.Where(sr => sr.ShelfNumber == model.ShelfNumber).Any();
            return rack.Shelves.Where(sr => sr.ShelfNumber == model.ShelfNumber && sr.Id != model.Id).Any();
        }
    }
}
