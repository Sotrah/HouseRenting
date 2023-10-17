using System;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public class Booking
{
	public int BookingId { get; set; }
	public DateTime BookingDate {get; set;}
    // Reference navigation property for the booked item
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
}
		


