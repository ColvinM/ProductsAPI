﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
        {
        }

        public DbSet<Product> TodoItems { get; set; } = null!;
    }
}