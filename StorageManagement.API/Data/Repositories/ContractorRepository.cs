using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Data.Repositories
{
    public interface IContractorRepository
    {
        Task<ICollection<ContractorModel>> GetContractos(Func<ContractorModel, bool> condition);
        Task<ICollection<ContractorModel>> GetContractos();
        Task<ContractorModel> GetContractor(int id);
        Task<ContractorModel> GetContractor(Func<ContractorModel, bool> condition);
        Task AddContractor(ContractorModel contractor);
        Task UpdateContractor(ContractorModel contractor);
        Task DeleteContractor(ContractorModel contractor);
    }
    public class ContractorRepository : IContractorRepository
    {
        private readonly MainContext _context;
        public ContractorRepository(MainContext context)
        {
            _context = context;
        }
        public async Task AddContractor(ContractorModel contractor)
        {
            try
            {
                await _context.Contractors.AddAsync(contractor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task DeleteContractor(ContractorModel contractor)
        {
            try
            {
                _context.Contractors.Remove(contractor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<ContractorModel> GetContractor(int id)
        {
            return await _context.Contractors.Include(c => c.Racks).ThenInclude(r => r.Shelves).ThenInclude(s => s.Product).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ContractorModel> GetContractor(Func<ContractorModel, bool> condition)
        {
            return _context.Contractors.Include(c => c.Racks).ThenInclude(r => r.Shelves).ThenInclude(s => s.Product).Where(condition).FirstOrDefault();
        }

        public async Task<ICollection<ContractorModel>> GetContractos(Func<ContractorModel, bool> condition)
        {
            return await _context.Contractors.Where(condition).AsQueryable().ToListAsync();
        }
        public async Task<ICollection<ContractorModel>> GetContractos()
        {
            return await _context.Contractors.ToListAsync();
        }
        public async Task UpdateContractor(ContractorModel contractor)
        {
            _context.Contractors.Update(contractor);
            await _context.SaveChangesAsync();
        }
    }
}
