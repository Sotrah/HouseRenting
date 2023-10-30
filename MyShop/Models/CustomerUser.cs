using Microsoft.AspNetCore.Identity;
namespace MyShop.Models;


public class CustomerUser : IdentityUser
{
    // Links to bookings and items, extends everything else from the normal IdentityUser
    public virtual ICollection<Booking>? Bookings { get; set; }

    public virtual ICollection<Item>? Items { get; set; }
}


