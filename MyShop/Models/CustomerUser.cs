using Microsoft.AspNetCore.Identity;
namespace MyShop.Models;


public class CustomerUser : IdentityUser
{
    public virtual ICollection<Booking>? Bookings { get; set; }

    public virtual ICollection<Item>? Items { get; set; }
}


