using Microsoft.AspNetCore.Http;

namespace MyShop.ViewModels
{
    public class ItemCreateViewModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Rooms { get; set; }
        public string? Beds { get; set; }

        public IFormFile ImageUpload { get; set; }  // For the image upload functionality
    }
}