﻿using Humanizer;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;

namespace MyShop.DAL;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Sorcerer's Spire",
                    Price = 1500,
                    Description = "Step into the Sorcerer's Spire, a penthouse loft towering above the city lights. This luxurious dwelling offers panoramic views of the urban skyline, making guests feel as though they're floating above the bustling metropolis below. The interior seamlessly melds contemporary design with mystical touches: crystal chandeliers, enchanted mirrors, and subtle spells in every nook. Centrally located yet feeling like a citadel in the clouds, it's the ideal escape for those seeking both luxury and magic at the city's heart.",
                    Address = "Magic Avenue 77, 17th Floor\r\nMetropolis, MageRealm 1010",
                    Phone = "64578324",
                    Rooms = "12",
                    Beds = "6",
                    ImageUrl = "/images/dnd26.png",
                    ImageUrl2 = "/images/Image2.4.png",
                    ImageUrl3 = "/images/Image2.0.png"
                },
                new Item
                {
                    Name = "Druid's Dell",
                    Price = 2000,
                    Description = "Druid's Dell: Urban Oasis with Nature's Touch\r\nAmidst the city's heartbeat, Druid's Dell emerges as a verdant retreat. This cozy apartment marries urban convenience with whispers of the wild: botanical prints, wooden finishes, and nature-inspired decor. Windows frame cityscapes, while indoors, tranquility reigns. Perfect for those who seek the city's vibrancy but cherish a natural sanctuary to recharge.",
                    Address = "60 Nature's Nectar Road\r\nWildgrove, Greenward 8555",
                    Phone = "68578324",
                    Rooms = "3",
                    Beds = "6",
                    ImageUrl = "/images/dnd23.png"
                },
                new Item
                {
                    Name = "Elf's Eyrie",
                    Price = 5000,
                    Description = "Ascend to Elf's Eyrie, a sophisticated penthouse poised above the shimmering skyline of Silver City. Crafted with elven elegance, its airy ambiance is complemented by floor-to-ceiling windows revealing sweeping urban vistas. The interiors fuse sleek, modern amenities with ethereal Elvish artistry: silver-leafed furnishings, moonlit balconies, and the subtle glow of fae lights. Perfectly situated for city endeavors yet offering an otherworldly sanctuary, Elf's Eyrie is a haven for those touched by both urban allure and timeless enchantment. ",
                    Address = "44 Silverleaf Lane\r\nMoonshadow Glade, Faelund 1023",
                    Phone = "64578324",
                    Rooms = "4",
                    Beds = "5",
                    ImageUrl = "/images/dnd24.png"
                },
                new Item
                {
                    Name = "Dwarf's Den",
                    Price = 2500,
                    Description = "Dwarf's Den: Enchanting Woodland Retreat\r\nDiscover a hidden gem: our stone cottage deep in Stonemount's woods. Crafted by dwarven artisans, it blends robust stonework with rustic charm. Sleeps 4 with a master suite, cozy loft, fireplace-warmed lounge, and rustic kitchenette. Minutes from Dwarvenhold's market. Enjoy woodland walks, local excursions, and complimentary Dwarven Ale. An idyllic escape for those seeking fantasy-inspired serenity.",
                    Address = "333 Mithril Mine Road \r\nStonemount, Dwarvenhold 4587",
                    Phone = "64578324",
                    Rooms = "5",
                    Beds = "4",
                    ImageUrl = "/images/dnd27.png"
                },
                new Item
                {
                    Name = "Wizard's Walkway",
                    Price = 1500,
                    Description = "Wizard's Walkway: Mystical Urban Nook\r\nTucked between the city's pulse, discover Wizard's Walkway: a magical townhouse oasis. Its enchanted interiors transport guests from the urban hustle to realms of fantasy. Though compact, its clever design maximizes space and charm. Spiral staircases, arcane art, and hidden nooks await. Steps from city attractions yet a world apart. Dive into urban adventures with a spellbinding twist. ",
                    Address = "19 Spellbook Street\r\nArcanum City, Magica 6342",
                    Phone = "64578324",
                    Rooms = "3",
                    Beds = "2",
                    ImageUrl = "/images/dnd21.png"
                },
                new Item
                {
                    Name = "Nymph's Nook",
                    Price = 1800,
                    Description = "Nymph's Nook: Lakeside Enchantment Cabin\r\nFind serenity at Nymph's Nook, a cabin cradled by the water's edge. Reflecting the delicate dance of nature, its wooden charm seamlessly melds with shimmering lake views. Awaken to gentle ripples, spend days canoeing or simply bask on the deck, enchanted by twilight's glow. Inside, cozy comforts meet ethereal elegance. The perfect sanctuary for water sprites and weary travelers alike.",
                    Address = "202 Waterfall Court\r\nEnchanted Forest, Sylvanis 1405",
                    Phone = "34578324",
                    Rooms = "12",
                    Beds = "10",
                    ImageUrl = "/images/dnd22.png"
                },
                new Item
                {
                    Name = "Bard's Boulevard",
                    Price = 5000,
                    Description = "Bard's Boulevard: Urban Elegance with a Fantasy Twist\r\nDive into a blend of modern city living and enchanting lore in our loft apartment at Bard's Boulevard. Sleek design meets lyrical charm: open-concept living, panoramic city views, and artistic touches inspired by age-old tales. Centrally located, steps from vibrant city spots. Ideal for urban explorers and lovers of unique ambiance. Experience the rhythm of the city with a magical undertone.",
                    Address = "88 Melody Manor\r\nHarmonytown, Lyrical 5678",
                    Phone = "64578324",
                    Rooms = "7",
                    Beds = "6",
                    ImageUrl = "/images/dnd28.png"
                },
                new Item
                {
                    Name = "Ranger's Roost",
                    Price = 3000,
                    Description = "Ranger's Roost: Majestic Mountain Hideaway\r\nElevate your escape at Ranger's Roost, a cabin perched in the heart of the mountains. This rustic refuge, crafted with nature's touch, offers breathtaking vistas, serene trails, and solitude. Inside, warm woods meet modern comforts, ensuring a blend of authenticity and luxury. Steps from pristine alpine adventures. Perfect for those seeking a serene retreat or the call of the wild. ",
                    Address = "50 Wilderness Way\r\nTracker's Trail, Frontiera 9900",
                    Phone = "56578324",
                    Rooms = "5",
                    Beds = "6",
                    ImageUrl = "/images/dnd29.png"
                },
                new Item
                {
                    Name = "Cleric's Cloister",
                    Price = 3000,
                    Description = "Cleric's Cloister: Divine Stone Manor Retreat\r\nImmerse in tranquility at Cleric's Cloister, a grand stone house standing regally amidst lush green expanses. Its pointed roof pierces the sky, while the interiors exude an aura of sanctity and comfort. Ornate stained glass, vaulted ceilings, and sacred motifs adorn every corner. Outside, the verdant lawn invites relaxation and meditation. A celestial haven just moments from life's conveniences. ",
                    Address = "90 Wilderness Way\r\nTracker's Trail, Frontiera 3400",
                    Phone = "23578324",
                    Rooms = "30",
                    Beds = "15",
                    ImageUrl = "/images/dnd32.png"
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
