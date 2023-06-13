﻿using Microsoft.EntityFrameworkCore;

namespace API.entityFramework
{
    public class RestaurantDbContext: DbContext
    {
        private readonly string _connectionUrl = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<TestApiClassEntityFramework> Type { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestApiClassEntityFramework>()
                .Property(n => n.Name).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Dish>()
                .Property(n => n.Name).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Address>()
                .Property(n => n.City).HasMaxLength(50);
           
            modelBuilder.Entity<Address>()
                .Property(n => n.Street).HasMaxLength(50);

        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionUrl);
        }
    }
}
