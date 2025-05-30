﻿using JoaquinProducts.Models;
using Microsoft.EntityFrameworkCore;

namespace JoaquinProducts.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}