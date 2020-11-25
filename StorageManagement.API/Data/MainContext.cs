using Microsoft.EntityFrameworkCore;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageManagement.API.Data
{
    public class MainContext : DbContext
    {
        public DbSet<WarehouseModel> Warehouses { get; set; }
        public DbSet<StorageRackModel> StorageRacks { get; set; }
        public DbSet<ShelfModel> Shelves { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ContractorModel> Contractors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseModel>().HasMany(w => w.StorageRacks).WithOne(sr => sr.Warehouse);
            modelBuilder.Entity<StorageRackModel>().HasMany(sr => sr.Shelves).WithOne(s => s.Rack);
            modelBuilder.Entity<ContractorModel>().HasMany(c => c.Racks).WithOne(sr => sr.Contractor);
            modelBuilder.Entity<ProductModel>().HasOne(p => p.Shelf).WithOne(s => s.Product).HasForeignKey<ShelfModel>(s => s.ProductId);
        
        }
    }
}
