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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StorageManagmentTest");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShelfModel>().ToTable("Shelves");
            modelBuilder.Entity<WarehouseModel>().HasMany(w => w.StorageRacks).WithOne(sr => sr.Warehouse);
            modelBuilder.Entity<StorageRackModel>().HasMany(sr => sr.Shelves).WithOne(s => s.Rack);
            modelBuilder.Entity<ContractorModel>().HasMany(c => c.Racks).WithOne(sr => sr.Contractor);
            modelBuilder.Entity<ProductModel>().HasOne(p => p.Shelf).WithOne(s => s.Product).HasForeignKey<ShelfModel>(s => s.ProductId);

            modelBuilder.Entity<WarehouseModel>().HasData(
               new WarehouseModel { Id = 1, Name = "Magazyn 1", City = "Rzeszów", Street = "Eugeniusza Kwiatkowskiego", PostCode = "35-311", UnitNumber = "45" }
               );
            modelBuilder.Entity<StorageRackModel>().HasData(
                new StorageRackModel { Id = 1, RackNumber = "A1", IsTaken = false, WarehouseId = 1 },
                new StorageRackModel { Id = 2, RackNumber = "A2", IsTaken = false, WarehouseId = 1 },
                new StorageRackModel { Id = 3, RackNumber = "B1", IsTaken = false, WarehouseId = 1 },
                new StorageRackModel { Id = 4, RackNumber = "B2", IsTaken = false, WarehouseId = 1 },
                new StorageRackModel { Id = 5, RackNumber = "C1", IsTaken = false, WarehouseId = 1 }
                );
            modelBuilder.Entity<ShelfModel>().HasData(
                new ShelfModel { Id = 1, ShelfNumber = "1", Quantity = 0, RackId = 1 },
                new ShelfModel { Id = 2, ShelfNumber = "2", Quantity = 0, RackId = 1 },
                new ShelfModel { Id = 3, ShelfNumber = "3", Quantity = 0, RackId = 1 },
                new ShelfModel { Id = 4, ShelfNumber = "4", Quantity = 0, RackId = 1 },

                new ShelfModel { Id = 5, ShelfNumber = "1", Quantity = 0, RackId = 2 },
                new ShelfModel { Id = 6, ShelfNumber = "2", Quantity = 0, RackId = 2 },
                new ShelfModel { Id = 7, ShelfNumber = "3", Quantity = 0, RackId = 2 },
                new ShelfModel { Id = 8, ShelfNumber = "4", Quantity = 0, RackId = 2 },

                new ShelfModel { Id = 9, ShelfNumber = "1", Quantity = 0, RackId = 3 },
                new ShelfModel { Id = 10, ShelfNumber = "2", Quantity = 0, RackId = 3 },
                new ShelfModel { Id = 11, ShelfNumber = "3", Quantity = 0, RackId = 3 },
                new ShelfModel { Id = 12, ShelfNumber = "4", Quantity = 0, RackId = 3 },

                new ShelfModel { Id = 13, ShelfNumber = "1", Quantity = 0, RackId = 4 },
                new ShelfModel { Id = 14, ShelfNumber = "2", Quantity = 0, RackId = 4 },
                new ShelfModel { Id = 15, ShelfNumber = "3", Quantity = 0, RackId = 4 },
                new ShelfModel { Id = 16, ShelfNumber = "4", Quantity = 0, RackId = 4 },

                new ShelfModel { Id = 17, ShelfNumber = "1", Quantity = 0, RackId = 5 },
                new ShelfModel { Id = 18, ShelfNumber = "2", Quantity = 0, RackId = 5 },
                new ShelfModel { Id = 19, ShelfNumber = "3", Quantity = 0, RackId = 5 },
                new ShelfModel { Id = 20, ShelfNumber = "4", Quantity = 0, RackId = 5 }
                );
        }
    }

}

