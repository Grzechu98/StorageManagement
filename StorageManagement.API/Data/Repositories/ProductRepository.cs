using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Data.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<ProductModel>> GetProducts(Func<ProductModel, bool> condition);
        Task<ProductModel> GetProduct(int id);
        Task<ProductModel> GetProduct(Func<ProductModel, bool> condition);
        Task AddProduct(ProductModel Product);
        Task UpdateProduct(ProductModel Product);
        Task DeleteProduct(ProductModel Product);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly MainContext _context;
        public ProductRepository(MainContext context)
        {
            _context = context;
        }
        public async Task AddProduct(ProductModel Product)
        {
            try
            {
                await _context.Products.AddAsync(Product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteProduct(ProductModel Product)
        {
            try
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ProductModel> GetProduct(Func<ProductModel, bool> condition)
        {
            return _context.Products.Where(condition).FirstOrDefault();
        }

        public async Task<ICollection<ProductModel>> GetProducts(Func<ProductModel, bool> condition)
        {
            return await Task.FromResult(_context.Products.Include(p => p.Shelf).ThenInclude(s => s.Rack).Where(condition).ToList());
        }

        public async Task UpdateProduct(ProductModel Product)
        {
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();
        }
    }
}
