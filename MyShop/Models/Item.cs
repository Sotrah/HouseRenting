using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyShop.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
        [Display(Name = "Item name")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        [Display(Name = "Price per night")]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "The Phone number must be between 8 and 15 digits")]
        [Display(Name = "Phone number")]
        public string? Phone { get; set; }

        [RegularExpression(@"^[0-9]{1,50}$", ErrorMessage = "Must be a number between 1 and 50")]
        public string? Rooms { get; set; }

        [RegularExpression(@"^[0-9]{1,50}$", ErrorMessage = "Must be a number between 1 and 50")]
        public string? Beds { get; set; }

        public string? ImageUrl { get; set; }
        // navigation property
        public virtual List<OrderItem>? OrderItems { get; set; }
    }
}

