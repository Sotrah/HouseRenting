using System.ComponentModel.DataAnnotations;

namespace MyShop.ViewModels
{
    public class ItemUpdateViewModel
    {
        public int ItemId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ.' \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
        [Display(Name = "Item name")]
        public string Name { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        [Display(Name = "Price per night")]
        public int Price { get; set; }

        [StringLength(600)]
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

        [RegularExpression(@"^[0-9]{1,50}$", ErrorMessage = "Must be a number between 1 and 50")]
        public string? Guests { get; set; }

        [RegularExpression(@"^[0-9]{1,50}$", ErrorMessage = "Must be a number between 1 and 50")]
        public string? Baths { get; set; }

        public IFormFile? ImageUpload { get; set; }
        public IFormFile? ImageUpload2 { get; set; }
        public IFormFile? ImageUpload3 { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
    }
}
