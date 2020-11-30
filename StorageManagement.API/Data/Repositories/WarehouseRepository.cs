using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Data.Repositories
{
    public interface IWarehouseRepository
    {
        Task<ICollection<WarehouseModel>> GetWarehouses();
        Task<WarehouseModel> GetWarehouse(int id);
        Task AddWarehouse(WarehouseModel warehouse);
        Task UpdateWarehouse(WarehouseModel warehouse);
        Task DeleteWarehouse(WarehouseModel warehouse);
    }
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly MainContext _context;

        public WarehouseRepository(MainContext context)
        {
            _context = context;
        }

        public async Task AddWarehouse(WarehouseModel warehouse)
        {
            try
            {
                await _context.Warehouses.AddAsync(warehouse);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteWarehouse(WarehouseModel warehouse)
        {
            try
            {
                _context.Warehouses.Remove(warehouse);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<WarehouseModel> GetWarehouse(int id)
        {
            return await _context.Warehouses.Include(e =>e.StorageRacks).ThenInclude(sr => sr.Shelves).ThenInclude(s => s.Product).Include(sr => sr.StorageRacks).ThenInclude(sr => sr.Contractor).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<ICollection<WarehouseModel>> GetWarehouses()
        {
            return await _context.Warehouses.ToListAsync();
        }

        public async Task UpdateWarehouse(WarehouseModel warehouse)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}
