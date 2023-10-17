using Microsoft.EntityFrameworkCore;
using MyShop.Models;

namespace MyShop.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "House",
                    Price = 150,
                    Description = "Nice house",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus1.jpg"
                },
                new Item
                {
                    Name = "Castle",
                    Price = 20,
                    Description = "Beautiful fairytale castle",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus2.jpg"
                },
                new Item
                {
                    Name = "Housy house",
                    Price = 50,
                    Description = "Very housy house",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus3.jpg"
                },
                new Item
                {
                    Name = "The Elk Mountain Lodge",
                    Price = 250,
                    Description = "Located on 2 acres of the gorgeous hillside, you’ll have plenty of space for sprawling out and enjoying the fresh mountain air in privacy. Get ready to take in clear night skies full of brilliant constellations from the back-patio hot tub— whether it’s a cold winter night or a fresh summer evening",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus4.jpg"
                },
                new Item
                {
                    Name = "Vivenda Vida Boa",
                    Price = 150,
                    Description = "After a year-long restoration project, we are thrilled to present Vivenda Vida Boa. Translating to The Good Life House in Portuguese, every inch of this property has been lovingly and thoughtfully restored to present an experience of upscale casual elegance.",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus5.jpg"
                },
                new Item
                {
                    Name = "The Pearl in Gulf Shores",
                    Price = 180,
                    Description = "Mix a cocktail at the wet bar and toast to the paradise that is beach life. With plenty of seating throughout, everyone can spend time together and find time to themselves. ",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus6.jpg"
                },
                new Item
                {
                    Name = "Heavenly Sunset",
                    Price = 50,
                    Description = "A welcoming, open floor plan makes it easy for your group to spend time together AND find quiet moments to yourself. Professional interior design and all-new appliances and furnishings make Heavenly Sunset the perfect setting for your next family vacation, corporate retreat, or romantic couples’ getaway.",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus7.jpg"
                },
                new Item
                {
                    Name = "Houseboat",
                    Price = 30,
                    Description = "“There is still a small amount of wave action and you’ll feel a slight rock from time-to-time, mostly from boats passing by who don’t respect the no wake zone (feel free to yell at them & shake your fist while doing so).”",
                    Address = "Gate 1, Oslo",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/hus8.jpg"
                },
            };
            context.AddRange(items);
            context.SaveChanges();
        }
        if (!context.Bookings.Any())
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    ItemId = 1,
                    BookingDate = new DateTime(2023, 10, 25),  // Example booked date
                },
                new Booking
                {
                    ItemId = 2,
                    BookingDate = new DateTime(2023, 10, 30)   // Another example booked date
                },
            };
            context.AddRange(bookings);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice Hansen", Address = "Osloveien 1"},
                new Customer { Name = "Bob Johansen", Address = "Oslomet gata 2"},
            };
            context.AddRange(customers);
            context.SaveChanges();
        }

        if (!context.Orders.Any())
        {
            var orders = new List<Order>
            {
                new Order {OrderDate = DateTime.Today.ToString("dd-MM-yyyy"), CustomerId = 1,},
                new Order {OrderDate = DateTime.Today.AddDays(-1).ToString("dd-MM-yyyy"), CustomerId = 2,},
            };
            context.AddRange(orders);
            context.SaveChanges();
        }

        if (!context.OrderItems.Any())
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ItemId = 1, Quantity = 2, OrderId = 1},
                new OrderItem { ItemId = 2, Quantity = 1, OrderId = 1},
                new OrderItem { ItemId = 3, Quantity = 4, OrderId = 2},
            };

            foreach (var orderItem in orderItems)
            {
                var item = context.Items.Find(orderItem.ItemId);
                orderItem.OrderItemPrice = orderItem.Quantity * item?.Price ?? 0;
            }

            context.AddRange(orderItems);
            context.SaveChanges();
        }

        var ordersToUpdate = context.Orders.Include(o => o.OrderItems);
        foreach (var order in ordersToUpdate)
        {
            order.TotalPrice = order.OrderItems?.Sum(oi => oi.OrderItemPrice) ?? 0;
        }
        context.SaveChanges();
    }
}
