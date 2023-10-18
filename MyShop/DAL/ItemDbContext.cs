﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
namespace MyShop.DAL;

public class ItemDbContext : IdentityDbContext  
{
    public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    // Connect the items and bookings with a one to many relationship.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Item)
            .WithMany(i => i.Bookings)
            .HasForeignKey(b => b.ItemId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}
