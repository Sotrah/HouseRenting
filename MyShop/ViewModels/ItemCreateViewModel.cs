namespace MyShop.ViewModels
{
    public class ItemCreateViewModel
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Rooms { get; set; }
        public string? Beds { get; set; }
        public string? Guests { get; set; }

        public string? Baths { get; set; }

        public IFormFile? ImageUpload { get; set; }  // For the image upload functionality
        public IFormFile? ImageUpload2 { get; set; }
        public IFormFile? ImageUpload3 { get; set; }
    }
}