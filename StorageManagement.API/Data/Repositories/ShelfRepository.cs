using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Data.Repositories
{
    public interface IShelfRepository
    {
        Task<ICollection<ShelfModel>> GetShelves(Func<ShelfModel, bool> condition);
        Task<ShelfModel> GetShelf(int id);
        Task<ShelfModel> GetShelf(Func<ShelfModel, bool> condition);
        Task AddShelf(ShelfModel shelf);
        Task UpdateShelf(ShelfModel shelf);
        Task DeleteShelf(ShelfModel shelf);
    }
    public class ShelfRepository : IShelfRepository
    {
        private readonly MainContext _context;
        public ShelfRepository(MainContext context)
        {
            _context = context;
        }
        public async Task AddShelf(ShelfModel shelf)
        {
            try
            {
                 _context.Shelves.Add(shelf);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteShelf(ShelfModel shelf)
        {
            try
            {
                _context.Shelves.Remove(shelf);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<ShelfModel> GetShelf(int id)
        {
           return await _context.Shelves.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ShelfModel> GetShelf(Func<ShelfModel, bool> condition)
        {
            return await _context.Shelves.Where(condition).AsQueryable().FirstOrDefaultAsync();
        }

        public async Task<ICollection<ShelfModel>> GetShelves(Func<ShelfModel, bool> condition)
        {
            return await _context.Shelves.Include(s => s.Product).Where(condition).AsQueryable().ToListAsync();
        }

        public async Task UpdateShelf(ShelfModel shelf)
        {
            _context.Shelves.Update(shelf);
            await _context.SaveChangesAsync();
        }
    }
}
