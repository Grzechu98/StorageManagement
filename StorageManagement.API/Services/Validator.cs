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
            if (model.Id == 0) return await _context.StorageRacks.AsNoTracking().Where(sr => sr.RackNumber == model.RackNumber).AnyAsync();
            return await _context.StorageRacks.AsNoTracking().Where(sr => sr.RackNumber == model.RackNumber && sr.Id != model.Id).AnyAsync();
        }

        public async Task<bool> IsShelfNumberInDb(ShelfModel model)
        {
            if(model.Id == 0) return await _context.Shelves.AsNoTracking().Where(sr => sr.ShelfNumber == model.ShelfNumber).AnyAsync();
            return await _context.Shelves.AsNoTracking().Where(sr => sr.ShelfNumber == model.ShelfNumber && sr.Id != model.Id).AnyAsync();
        }
    }
}
