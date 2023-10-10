using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyShop.Models;

public class OrderItem
{
    [BindNever]
    public int OrderItemId { get; set; }
	public int ItemId { get; set; }
	//navigation property
	public virtual Item Item { get; set; } = default!;

    [Range(0.01, double.MaxValue, ErrorMessage = "The Quantity must be greater than 0.")]
    public int Quantity { get; set; }

	public int OrderId { get; set; }
    //navigation property
    public virtual Order Order { get; set; } = default!;

    [BindNever]
	public decimal OrderItemPrice { get; set; }
}

