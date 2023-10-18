using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using Microsoft.EntityFrameworkCore;
using MyShop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MyShop.DAL;
namespace MyShop.Controllers;

public class BookingController : Controller
{
    private readonly ItemDbContext _itemDbContext;

    public BookingController(ItemDbContext itemDbContext)
    {
        _itemDbContext = itemDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<Booking> bookings = await _itemDbContext.Bookings.ToListAsync();
        return View(bookings);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateBooking(Booking booking)
    {
        try
        {
            var newItem = await _itemDbContext.Items.FindAsync(booking.ItemId);

            if (newItem == null)
            {
                return BadRequest("Item not found.");
            }

            var newBooking = new Booking
            {
                BookingDate = booking.BookingDate,
                ItemId = booking.ItemId,
                Item = newItem,
            };

            _itemDbContext.Bookings.Add(newBooking);
            await _itemDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
        catch
        {
            return BadRequest("BookingItem creation failed.");
        }
    }
}
