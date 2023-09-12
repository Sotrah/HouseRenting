using System;
using HouseRenting.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HouseRenting.Models;

public class ItemDbContext : DbContext
{
    public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Item> Items { get; set; }
}
