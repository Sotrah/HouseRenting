using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using Microsoft.EntityFrameworkCore;
using MyShop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MyShop.DAL;
using Microsoft.AspNetCore.Identity;

namespace MyShop.Controllers;

public class BookingController : Controller

{
    private readonly ItemDbContext _itemDbContext;
    private readonly UserManager<CustomerUser> _userManager; // Inject UserManager


    public BookingController(ItemDbContext itemDbContext, UserManager<CustomerUser> userManager)
    {
        _itemDbContext = itemDbContext;
        _userManager = userManager;
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


            var user = await _userManager.GetUserAsync(User);
            string userId = user.Id;

            var newBooking = new Booking
            {
                BookingDate = booking.BookingDate,
                ItemId = booking.ItemId,
                Item = newItem,
                UserId = userId,
                CustomerUser = user,
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
