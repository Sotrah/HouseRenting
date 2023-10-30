using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.DAL;
using MyShop.Models;

namespace MyShop.Controllers;

public class BookingController : Controller

{
    private readonly ItemDbContext _itemDbContext;
    private readonly UserManager<CustomerUser> _userManager; // Inject UserManager
    private readonly ILogger<BookingController> _logger;


    public BookingController(ItemDbContext itemDbContext, UserManager<CustomerUser> userManager,ILogger<BookingController> logger)
    {
        _itemDbContext = itemDbContext;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<IActionResult> Table()
    {
        List<Booking> bookings = await _itemDbContext.Bookings.ToListAsync();
        return View(bookings);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateBooking()
    {
        return RedirectToAction("Grid", "Item"); // If you try booking something without being logged in, you'll go to Item Grid after logging in
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
            return RedirectToAction("Table", "Booking"); //After creating a booking, reroutes you to a booking list.
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex, "Failed to create booking item");
            return BadRequest("BookingItem creation failed.");
        }
    }
}
