using ChefNDish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefNDish.Controllers;


public class DishController : Controller
{
private ChefNDish.Models.MyContext db;         
    // Here we can "inject" our context service into the constructor 
    public DishController(ChefNDish.Models.MyContext context)    
    {         
        // When our DishController is instantiated, it will fill in db with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        db = context;    
    }       

[HttpGet("/dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.AllChefs = db.Chefs.ToList();
        return View("DishNew");
    }

[HttpPost("/dish/create")]
public IActionResult CreateDish(Dish newDish)
    {
        string messages = string.Join("; ", ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage));         
            Console.WriteLine(messages);
            //line 58 61 to catch extra errs


        if (!ModelState.IsValid)
        {
            return View("DishNew");
        }
        db.Dishes.Add(newDish);
        //the .add is maping over your data table in mysql bench

        db.SaveChanges();
        
        Console.WriteLine(newDish.DishId);
        return RedirectToAction("AllDishes");
    }

    [SessionCheck]
    [HttpGet("/dishes")]
    public IActionResult AllDishes()
    {
        List<Dish> allDishes = db.Dishes.Include(dish => dish.Creator).ToList();
        // List<Dish> allDishes = db.Dishes.ToList();


        return View("All", allDishes);
    }

    // [HttpGet("/dish/{DishesId}")]
    // public IActionResult DetailsDish(int DishId)
    // {
    //     Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == DishId);
    //     if (dish == null)
    //     {
    //         return RedirectToAction("All");
    //     }
    //     return View("Details", dish);
    // }
}
