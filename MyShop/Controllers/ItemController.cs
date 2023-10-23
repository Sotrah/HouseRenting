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
            if (model.ImageUpload != null && model.ImageUpload.Length > 0)
            {
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", model.ImageUpload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageUpload.CopyToAsync(stream);
                }

                // Convert model to Item type
                var item = new Item
                {
                    // Assign all other properties from model to item...
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    Address = model.Address,
                    Phone = model.Phone,
                    Rooms = model.Rooms,
                    Beds = model.Beds,
                    Guests = model.Guests,
                    Baths = model.Baths,
                    ImageUrl = "/images/" + model.ImageUpload.FileName // ImageUrl gets the path of uploaded image
                };

                bool returnOk = await _itemRepository.Create(item);
                if (returnOk)
                    return RedirectToAction(nameof(Table));

                return View(model);
            }
            else
            {
                ModelState.AddModelError("ImageUpload", "Please upload an image.");
                return View(model);
            }
        }
        return View(model);  // Dette vil fange opp alle andre scenarier
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
    public async Task<IActionResult> Update(Item item, IFormFile ImageUpload)
    {
        if (ModelState.IsValid)
        {
            var uploadedImage = Request.Form.Files.FirstOrDefault();

            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", uploadedImage.FileName);

                // Sjekker om filen allerede eksisterer og legger til en unik identifikator for å unngå overskriving
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uploadedImage.FileName);
                var fileExtension = Path.GetExtension(uploadedImage.FileName);
                var counter = 1;

                while (System.IO.File.Exists(filePath))
                {
                    var tempFileName = $"{fileNameWithoutExtension}_{counter}{fileExtension}";
                    filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", tempFileName);
                    counter++;
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(stream);
                }

                item.ImageUrl = "/images/" + Path.GetFileName(filePath);
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
}
