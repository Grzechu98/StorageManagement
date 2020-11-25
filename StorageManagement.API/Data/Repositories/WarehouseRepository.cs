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
        Task DeleteWarehouse(int id);
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

        public async Task DeleteWarehouse(int id)
        {
            try
            {
                _context.Warehouses.Remove(_context.Warehouses.FirstOrDefault(w => w.Id == id));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<WarehouseModel> GetWarehouse(int id)
        {
            return await _context.Warehouses.FirstOrDefaultAsync(w => w.Id == id);
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
