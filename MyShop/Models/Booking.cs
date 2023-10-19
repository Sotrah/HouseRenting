using Microsoft.AspNetCore.Identity;

namespace MyShop.Models;

public class Booking
{
	public int BookingId { get; set; }
	public DateTime BookingDate {get; set;}

    // Reference navigation property for the booked item
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }

    // Foreign key to link to the user
    // Currently marked as optional with '?' while trying stuff
    public string? UserId { get; set; }
    public virtual CustomerUser? CustomerUser { get; set; }
}
		


