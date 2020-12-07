using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Data.Repositories
{
    public interface IStorageRackRepository
    {
        Task<ICollection<StorageRackModel>> GetStorageRacks(Func<StorageRackModel, bool> condition);
        Task<StorageRackModel> GetStorageRack(int id);
        Task<StorageRackModel> GetStorageRack(Func<StorageRackModel, bool> condition);
        Task AddStorageRack(StorageRackModel storageRack);
        Task UpdateStorageRack(StorageRackModel storageRack);
        Task DeleteStorageRack(StorageRackModel storageRack);
    }
    public class StorageRackRepository : IStorageRackRepository
    {
        private readonly MainContext _context;
        public StorageRackRepository(MainContext context)
        {
            _context = context;
        }
        public async Task AddStorageRack(StorageRackModel storageRack)
        {
            try
            {
                await _context.StorageRacks.AddAsync(storageRack);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteStorageRack(StorageRackModel storageRack)
        {
            try
            {
                _context.StorageRacks.Remove(storageRack);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<StorageRackModel> GetStorageRack(int id)
        {
            return await _context.StorageRacks.Include(sr => sr.Shelves).ThenInclude(s => s.Product).AsNoTracking().FirstOrDefaultAsync(sr => sr.Id == id);
        }

        public async Task<StorageRackModel> GetStorageRack(Func<StorageRackModel, bool> condition)
        {
            return _context.StorageRacks.Include(sr => sr.Shelves).ThenInclude(s =>s.Product).Where(condition).FirstOrDefault();
        }

        public async Task<ICollection<StorageRackModel>> GetStorageRacks(Func<StorageRackModel, bool> condition)
        {
            return await _context.StorageRacks.Include(sr => sr.Shelves).ThenInclude(s => s.Product).Include(sr => sr.Contractor).Where(condition).AsQueryable().ToListAsync();
        }

        public async Task UpdateStorageRack(StorageRackModel storageRack)
        {
            _context.StorageRacks.Update(storageRack);
            await _context.SaveChangesAsync();
        }
    }
}
