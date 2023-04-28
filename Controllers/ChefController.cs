using ChefNDish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChefNDish.Controllers;

public class ChefController : Controller
{
private ChefNDish.Models.MyContext db;         
    // Here we can "inject" our context service into the constructor 
    public ChefController(ChefNDish.Models.MyContext context)    
    {         
        // When our ChefController is instantiated, it will fill in db with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        db = context;    
    }       


[HttpGet("/chefs/new")]
    public IActionResult NewChef()
    {
        return View("ChefNew");
    }

[HttpPost("/chefs/create")]
public IActionResult CreateChef(Chef newChef)
    {
        string messages = string.Join("; ", ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage));         
            Console.WriteLine(messages);
            //line 58 61 to catch extra errs


        if (!ModelState.IsValid)
        {
            return View("ChefNew");
        }
        db.Chefs.Add(newChef);
        //the .add is maping over your data table in mysql bench

        db.SaveChanges();
        
        Console.WriteLine(newChef.ChefId);
         
        return RedirectToAction("AllChefs");
    }

    // [HttpGet("")]
    [SessionCheck]
    [HttpGet("")]
    public IActionResult AllChefs()
    {
        List<Chef> allChefs = db.Chefs.Include(dish => dish.AllDishesMade).ToList();
        // List<Chef> allChefs = db.Chefs.ToList();
        ViewBag.AllChefs = db.Chefs.Include(dish => dish.AllDishesMade).ToList();
        

        

        return View("All", allChefs);
    }

    // [HttpGet("/chef/{ChefsId}")]
    // public IActionResult DetailsDish(int ChefId)
    // {
    //     Chef? chef = db.Chefs.FirstOrDefault(chef => chef.ChefId == ChefId);
    //     if (chef == null)
    //     {
    //         return RedirectToAction("All");
    //     }
    //     return View("Details", chef);
    // }
}