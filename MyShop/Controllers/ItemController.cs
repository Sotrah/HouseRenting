using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.ViewModels;

namespace MyShop.Controllers;

public class ItemController : Controller
{
    public IActionResult Table()
    {
        var items = GetItems();
        var itemListViewModel = new ItemListViewModel(items, "Table");
        return View(itemListViewModel);
    }

    public IActionResult Grid()
    {
        var items = GetItems();
        var itemListViewModel = new ItemListViewModel(items, "Grid");
        return View(itemListViewModel);
    }

    public IActionResult Details(int id)
    {
        var items = GetItems();
        var item = items.FirstOrDefault(i => i.ItemId == id);
        if (item == null)
            return NotFound();
        return View(item);
    }

    //public IActionResult Table()
    //{
    //    var items = GetItems();
    //    ViewBag.CurrentViewName = "Table";
    //    return View(items);
    //}

    //public IActionResult Grid()
    //{
    //    var items = GetItems();
    //    ViewBag.CurrentViewName = "Grid";
    //    return View(items);
    //}

    public List<Item> GetItems()
    {
        var items = new List<Item>();
        var item1 = new Item
        {
            ItemId = 1,
            Name = "Hus1",
            Price = 150,
            Description = "Hus1.",
            ImageUrl = "/images/hus1.jpg",
            Rooms = 1,
            Address = "Address1",
            Telephone = 1111111
        };

        var item2 = new Item
        {
            ItemId = 2,
            Name = "Hus 2",
            Price = 20,
            Description = "Hus2.",
            ImageUrl = "/images/hus2.jpg",
            Rooms = 2,
            Address = "Address2",
            Telephone = 2222222
        };

        var item3 = new Item
        {
            ItemId = 3,
            Name = "Hus3",
            Price = 50,
            Description = "Hus3",
            ImageUrl = "/images/hus3.jpg",
            Rooms = 3,
            Address = "Address3",
            Telephone = 3333333
        };

        var item4 = new Item
        {
            ItemId = 4,
            Name = "Hus4",
            Price = 250,
            Description = "Hus4",
            ImageUrl = "/images/hus4.jpg",
            Rooms = 4,
            Address = "Address4",
            Telephone = 44444444
        };

        var item5 = new Item
        {
            ItemId = 5,
            Name = "Hus5",
            Price = 150,
            Description = "Hus5",
            ImageUrl = "/images/hus5.jpg",
            Rooms = 5,
            Address = "Address5",
            Telephone = 5555555
        };

        var item6 = new Item
        {
            ItemId = 6,
            Name = "Hus6",
            Price = 180,
            Description = "Hus6",
            ImageUrl = "/images/hus6.jpg",
            Rooms = 6,
            Address = "Address6",
            Telephone = 6666666
        };


        items.Add(item1);
        items.Add(item2);
        items.Add(item3);
        items.Add(item4);
        items.Add(item5);
        items.Add(item6);
        return items;
    }
}