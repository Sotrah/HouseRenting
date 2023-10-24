using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.DAL;
using MyShop.Models;
using MyShop.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MyShop.Controllers;

public class ItemController : Controller
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemController> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger, IWebHostEnvironment hostEnvironment)
    {
        _itemRepository = itemRepository;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Table()
    {
        var items = await _itemRepository.GetAll();
        if (items == null)
        {
            _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
            return NotFound("Item list not found");
        }
        var itemListViewModel = new ItemListViewModel(items, "Table");
        return View(itemListViewModel);
    }

    public async Task<IActionResult> Grid()
    {
        var items = await _itemRepository.GetAll();
        if (items == null)
        {
            _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
            return NotFound("Item list not found");
        }
        var itemListViewModel = new ItemListViewModel(items, "Grid");
        return View(itemListViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
            return NotFound("Item not found for the ItemId");
        }
        return View(item);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(ItemCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var imageUrl = await UploadImage(model.ImageUpload);
            var imageUrl2 = await UploadImage(model.ImageUpload2);
            var imageUrl3 = await UploadImage(model.ImageUpload3);

            if (imageUrl == null)
            {
                ModelState.AddModelError("ImageUpload", "Please upload an image.");
                return View(model);
            }
            var item = new Item
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Address = model.Address,
                Phone = model.Phone,
                Rooms = model.Rooms,
                Beds = model.Beds,
                Guests = model.Guests,
                Baths = model.Baths,
                ImageUrl = imageUrl,
                ImageUrl2 = imageUrl2,
                ImageUrl3 = imageUrl3,
            };

            bool returnOk = await _itemRepository.Create(item);
            if (returnOk)
                return RedirectToAction(nameof(Table));

            return View(model);
        }
        return View(model); // This will catch all other scenarios
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            _logger.LogError("[ItemController] Item not found when updating the ItemId {ItemId:0000}", id);
            return BadRequest("Item not found for the ItemId");
        }
        return View(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Update(Item item, IFormFile ImageUpload, IFormFile ImageUpload2, IFormFile ImageUpload3)
    {
        if (ModelState.IsValid)
        {
            if (ImageUpload != null && ImageUpload.Length > 0)
            {
                item.ImageUrl = await UploadImage(ImageUpload);
            }

            if (ImageUpload2 != null && ImageUpload2.Length > 0)
            {
                item.ImageUrl2 = await UploadImage(ImageUpload2);
            }

            if (ImageUpload3 != null && ImageUpload3.Length > 0)
            {
                item.ImageUrl3 = await UploadImage(ImageUpload3);
            }

            bool returnOk = await _itemRepository.Update(item);
            if (returnOk)
                return RedirectToAction(nameof(Table));
        }

        _logger.LogWarning("[ItemController] Item update failed {@item}", item);
        return View(item);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            _logger.LogError("[ItemController] Item not found for the ItemId {ItemId:0000}", id);
            return BadRequest("Item not found for the ItemId");
        }
        return View(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool returnOk = await _itemRepository.Delete(id);
        if (!returnOk)
        {
            _logger.LogError("[ItemController] Item deletion failed for the ItemId {ItemId:0000}", id);
            return BadRequest("Item deletion failed");
        }
        return RedirectToAction(nameof(Table));
    }
    private async Task<string> UploadImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0) return null;

        var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", imageFile.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return "/images/" + imageFile.FileName;
    }
}
