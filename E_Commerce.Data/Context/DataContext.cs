﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Model;

namespace E_Commerce.Data.Context
{
    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DataContext() { }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Admin>().ToTable("Admins");
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<OrderDetails>().HasKey(OrderDetails=>new { OrderDetails.OrderId, OrderDetails.ProductId });
            builder.Entity<Order>().Property(o => o.TotalPrice).HasColumnType("money");
            builder.Entity<OrderDetails>().Property(od => od.UnitPrice).HasColumnType("money");
            builder.Entity<Product>().Property(p => p.UnitPrice).HasColumnType("money");
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=OWAGEH-LT-11120\\SQLEXPRESS;Initial Catalog=ECommerce_123;Integrated Security=True;TrustServerCertificate=True");
            }
        }
    }
}
