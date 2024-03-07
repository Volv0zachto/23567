﻿using _666.Models;
using Microsoft.EntityFrameworkCore;

namespace _666.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSet для каждой сущности в вашей базе данных
    public DbSet<Product> Products { get; set; }
}