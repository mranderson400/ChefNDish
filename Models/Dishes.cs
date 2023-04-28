#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefNDish.Models;
public class Dish
{    
    [Key]    

public int DishId { get; set; }
    
[Required(ErrorMessage = "is required")] 
[Display(Name  =" DishName")]
public string DishName { get; set; }
    
       
public int Tastiness {get; set;}

public int Calories {get; set;}    
    
public DateTime CreatedAt {get;set;} = DateTime.Now;   
public DateTime UpdatedAt {get;set;} = DateTime.Now;

public int ChefId {get; set;} // ChefId needs to match with your key in Chefs.cs file
public Chef? Creator {get; set;} // 1 chef related to each dish posted. Also line 27 uses navigate props?
    // public object AllDishesMade { get; internal set; }
}